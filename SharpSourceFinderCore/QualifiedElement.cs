using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class QualifiedElement : NonTerminalElement<IdentityElement>, IQualified
	{
		public QualifiedElement()
		{
		}

		public QualifiedElement(IDiscriminatedElement parent) : base(parent)
		{
		}

		public IReadOnlyList<IIdentity> Identities => TypedChildren;

		public bool IsEquivalentTo(IQualified other)
		{
#warning IsEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentTo is not implemented");
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


		//Must coordinate the IdentityElement's Order property.
		public override void RegisterChild(IDiscriminatedElement child)
		{
#warning RegisterChild_Is_NotImpl
			throw new NotImplementedException("RegisterChild is not implemented");
			base.RegisterChild(child);
		}
	}
}