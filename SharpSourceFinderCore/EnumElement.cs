using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class EnumElement : TypeElement
	{
		public EnumElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial, bool isStatic) : base(parent, scope, isUnsafe, isPartial, isStatic)
		{
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