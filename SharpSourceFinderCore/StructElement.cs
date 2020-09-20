using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class StructElement : TypeElement
	{
		public StructElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial) : base(parent, scope, isUnsafe, isPartial, false)
		{
#warning StructElement_Is_NotImpl
			throw new NotImplementedException("StructElement is not implemented");
		}
		public override void Describe(StringBuilder builder, string indent, int depth)
		{
#warning Describe_Is_NotImpl
			throw new NotImplementedException("Describe is not implemented");
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
#warning AggregateIdentities_Is_NotImpl
			throw new NotImplementedException("AggregateIdentities is not implemented");
		}

		public override bool IsEquivalentLogicallyTo(IDiscriminatedElement other)
		{
#warning IsEquivalentLogicallyTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentLogicallyTo is not implemented");
		}

		
	}
}