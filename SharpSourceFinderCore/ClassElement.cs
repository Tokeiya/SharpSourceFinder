using System.Collections.Generic;
using System.Diagnostics;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class ClassElement : TypeElement
	{
		public ClassElement(IDiscriminatedElement parent, ScopeCategories scope, bool isAbstract, bool isSealed,
			bool isUnsafe, bool isPartial, bool isStatic) : base(parent, scope, isAbstract, isSealed, isUnsafe,
			isPartial, isStatic)
		{
		}

		public override QualifiedElement GetQualifiedName()
		{
			var stack = StackPool.Get();

			try
			{

				foreach (var elem in AncestorsAndSelf())
				{
					elem.AggregateIdentities(stack);
				}

				var ret = new QualifiedElement();

				while (stack.Count != 0)
				{
					var (category, name) = stack.Pop();
					_ = new IdentityElement(ret, category, name);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(stack.Count == 0);
				StackPool.Return(stack);
			}
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
			foreach (var elem in Identity.Identities) accumulator.Push((elem.Category, elem.Name));
		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
			if (other is ClassElement elem)
				if (elem.IsBasementEquivalentTo(this))
					return true;

			return false;
		}
	}
}