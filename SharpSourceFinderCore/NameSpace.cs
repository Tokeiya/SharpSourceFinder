using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameSpace : NonTerminalElement<IDiscriminatedElement>
	{
		public NameSpace()
		{
#warning NameSpace_Is_NotImpl
			throw new NotImplementedException("NameSpace is not implemented");
		}
		public NameSpace(IPhysicalStorage physicalStorage)
		{
#warning NameSpace_Is_NotImpl
			throw new NotImplementedException("NameSpace is not implemented");
		}
		public NameSpace(IDiscriminatedElement parent):base(parent)
		{
#warning NameSpace_Is_NotImpl
			throw new NotImplementedException("NameSpace is not implemented");
		}
		QualifiedElement Identity
		{
			get
			{
#warning Identity_Is_NotImpl
				throw new NotImplementedException("Identity is not implemented");
			}
		}

		public override void RegisterChild(IDiscriminatedElement child)
		{
#warning RegisterChild_Is_NotImpl
			throw new NotImplementedException("RegisterChild is not implemented");
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

		public override IPhysicalStorage Storage
		{
			get
			{
#warning Storage_Is_NotImpl
				throw new NotImplementedException("Storage is not implemented");
			}
		}

		
	}
}