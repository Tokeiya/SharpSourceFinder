using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class DiscriminatedElement : IDiscriminatedElement
	{
		protected DiscriminatedElement(IDiscriminatedElement parent)
		{
#warning DiscriminatedElement_Is_NotImpl
			throw new NotImplementedException("DiscriminatedElement is not implemented");
		}
		protected DiscriminatedElement()
		{
#warning DiscriminatedElement_Is_NotImpl
			throw new NotImplementedException("DiscriminatedElement is not implemented");
		}
		public IDiscriminatedElement Parent { get; }

		public virtual IPhysicalStorage Storage
		{
			get
			{
#warning Storage_Is_NotImpl
				throw new NotImplementedException("Storage is not implemented");
			}
		}
		public abstract void RegisterChild(IDiscriminatedElement child);
		public abstract void Describe(StringBuilder builder, string indent, int depth);

		public string Describe(string indent = "\t")
		{
#warning Describe_Is_NotImpl
			throw new NotImplementedException("Describe is not implemented");
		}

		public IEnumerable<IDiscriminatedElement> Ancestors()
		{
#warning Ancestors_Is_NotImpl
			throw new NotImplementedException("Ancestors is not implemented");
		}

		public IEnumerable<IDiscriminatedElement> AncestorsAndSelf()
		{
#warning AncestorsAndSelf_Is_NotImpl
			throw new NotImplementedException("AncestorsAndSelf is not implemented");
		}

		public abstract IEnumerable<IDiscriminatedElement> Children();

		public IEnumerable<IDiscriminatedElement> Descendants()
		{
#warning Descendants_Is_NotImpl
			throw new NotImplementedException("Descendants is not implemented");
		}

		public IEnumerable<IDiscriminatedElement> DescendantsAndSelf()
		{
#warning DescendantsAndSelf_Is_NotImpl
			throw new NotImplementedException("DescendantsAndSelf is not implemented");
		}

		public Qualified GetQualifiedName()
		{
#warning GetQualifiedName_Is_NotImpl
			throw new NotImplementedException("GetQualifiedName is not implemented");
		}

		public abstract void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator);

		public abstract bool IsEquivalentLogicallyTo(IDiscriminatedElement other);

		public bool IsEquivalentPhysicallyTo(IDiscriminatedElement other)
		{
#warning IsEquivalentPhysicallyTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentPhysicallyTo is not implemented");
		}
	}
}