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
			throw new NotImplementedException();
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
#warning AggregateIdentities_Is_NotImpl
			throw new NotImplementedException("AggregateIdentities is not implemented");
		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
#warning IsEquivalentLogicallyTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentLogicallyTo is not implemented");
		}

		public override bool IsPhysicallyEquivalentTo(IDiscriminatedElement other)
		{
			throw new NotImplementedException();
		}
	}
}