using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityElement : TerminalElement, IIdentity
	{
		public IdentityElement(IDiscriminatedElement from, IdentityCategories category, string name) : base(from)
		{
#warning IdentityElement_Is_NotImpl
			throw new NotImplementedException("IdentityElement is not implemented");
		}
			
		public IdentityElement(IDiscriminatedElement parent, string name) : base(parent)
		{
#warning IdentityElement_Is_NotImpl
			throw new NotImplementedException("IdentityElement is not implemented");
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

		public IdentityCategories Category { get; }
		public string Name { get; }
		public IQualified From { get; }
		public bool IsEquivalentTo(IIdentity identity)
		{
			throw new NotImplementedException();
		}

		public bool IsPhysicallyEquivalentTo(IIdentity identity)
		{
			throw new NotImplementedException();
		}
	}
}