using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class ClassElement : TypeElement
	{
		public ClassElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial,
			bool isStatic) : base(parent, scope, isUnsafe, isPartial, isStatic)
		{
#warning ClassElement_Is_NotImpl
			throw new NotImplementedException("ClassElement is not implemented");
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

		public override bool IsEquivalentTo(IDiscriminatedElement other)
		{
#warning IsEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentTo is not implemented");
		}
	}
}