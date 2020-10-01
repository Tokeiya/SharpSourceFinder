using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class StructElement : TypeElement
	{
		public StructElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial) : base(
			parent, scope, isUnsafe, isPartial, false)
		{
#warning StructElement_Is_NotImpl
			throw new NotImplementedException("StructElement is not implemented");
		}


		public override QualifiedElement GetQualifiedName()
		{
#warning GetQualifiedName_Is_NotImpl
			throw new NotImplementedException("GetQualifiedName is not implemented");
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