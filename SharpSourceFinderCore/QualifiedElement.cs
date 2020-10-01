using System;
using System.Collections.Generic;
using System.Text;

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
		public override QualifiedElement GetQualifiedName()
		{
			throw new NotImplementedException();
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
			throw new NotImplementedException();
		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
			throw new NotImplementedException();
		}

		public override bool IsPhysicallyEquivalentTo(IDiscriminatedElement other)
		{
			throw new NotImplementedException();
		}

		public IReadOnlyList<IIdentity> Identities => TypedChildren;
		public bool IsEquivalentTo(IQualified other)
		{
			throw new NotImplementedException();
		}
	}
}