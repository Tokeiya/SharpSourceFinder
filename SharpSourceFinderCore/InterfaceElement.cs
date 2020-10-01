using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class InterfaceElement : TypeElement
	{
		public InterfaceElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial) :
			base(parent, scope, isUnsafe, isPartial, false)
		{
		}


		public override QualifiedElement GetQualifiedName()
		{
			throw new NotImplementedException();
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
#warning AggregateIdentities_Is_NotImpl
			throw new NotImplementedException("AggregateIdentities is not implemented");
		}

		public override bool IsEquivalentTo(IDiscriminatedElement other)
		{
#warning IsEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentTo is not implemented");
		}
	}
}