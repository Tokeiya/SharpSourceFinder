using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class DelegateElement : TypeElement
	{
		public DelegateElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe) : base(parent, scope,
			false, true,
			isUnsafe, false, false)
		{
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
					_ = new IdentityElement(ret,scope, category, name);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(stack.Count == 0);
				StackPool.Return(stack);
			}
		}

		public override void RegisterChild(IDiscriminatedElement child)
		{
			if (!(child is QualifiedElement))
				throw new ArgumentException($"{child.GetType().Name} is unexpected child type.");
			base.RegisterChild(child);
		}

		public override void AggregateIdentities(Stack<(ScopeCategories scope, IdentityCategories category, string identity)> accumulator)
		{
			foreach (var elem in Identity.Identities) accumulator.Push((elem.Scope,elem.Category, elem.Name));
		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
			if (other is DelegateElement elem) return IsBasementEquivalentTo(elem);

			return false;
		}
	}
}