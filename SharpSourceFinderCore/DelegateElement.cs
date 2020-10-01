using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class DelegateElement : TypeElement
	{
		public DelegateElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe) : base(parent, scope,
			isUnsafe, false, false)
		{
#warning DelegateElement_Is_NotImpl
			throw new NotImplementedException("DelegateElement is not implemented");
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