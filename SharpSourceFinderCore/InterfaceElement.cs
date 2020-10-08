using System;
using System.Collections.Generic;

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
			throw new NotImplementedException();
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
#warning AggregateIdentities_Is_NotImpl
			throw new NotImplementedException("AggregateIdentities is not implemented");
		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
#warning IsLogicallyEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsLogicallyEquivalentTo is not implemented");
		}
	}
}