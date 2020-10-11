using System.Collections.Generic;
using System.Diagnostics;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class InterfaceElement : TypeElement
	{
		public InterfaceElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial) :
			base(parent, scope, true, false, isUnsafe, isPartial, false)
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
			if (other is InterfaceElement elem) return elem.IsBasementEquivalentTo(this);
			return true;
		}
	}
}