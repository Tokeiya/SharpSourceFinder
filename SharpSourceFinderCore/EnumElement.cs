using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class EnumElement : TypeElement
	{
		public EnumElement(IDiscriminatedElement parent, ScopeCategories scope) : base(parent, scope, false, true,
			false, false, false)
		{
		}

		public override void RegisterChild(IDiscriminatedElement child)
		{
			if (!ReferenceEquals(child.Parent, this))
				throw new ArgumentException($"{nameof(child)}'s parent is different.");

			if (child is IQualified id)
			{
				if (!(_identity is null)) throw new IdentityDuplicatedException();
				_identity = id;
			}
			else
			{
				throw new ArgumentException($"{nameof(EnumElement)} can't register any child.");
			}
		}

		public override QualifiedElement GetQualifiedName()
		{
			var stack = StackPool.Get();

			try
			{
				foreach (var elem in AncestorsAndSelf()) elem.AggregateIdentities(stack);
				var ret = new QualifiedElement();

				while (stack.Count != 0)
				{
					var (scope,category, name) = stack.Pop();
					_ = new IdentityElement(ret, scope,category, name);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(stack.Count == 0);
				StackPool.Return(stack);
			}
		}

		public override void AggregateIdentities(Stack<(ScopeCategories scope, IdentityCategories category, string identity)> accumulator)
		{
			foreach (var elem in Identity.Identities) accumulator.Push((elem.Scope,elem.Category, elem.Name));
		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
			if (other is EnumElement elem) return IsBasementEquivalentTo(elem);
			return false;
		}
	}
}