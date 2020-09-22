using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class ImaginaryRoot : IDiscriminatedElement
	{
		private ImaginaryRoot()
		{
#warning ImaginaryRoot_Is_NotImpl
			throw new NotImplementedException("ImaginaryRoot is not implemented");
		}

		public static ImaginaryRoot Root
		{
			get
			{
#warning Root_Is_NotImpl
				throw new NotImplementedException("Root is not implemented");
			}
		}

		public IDiscriminatedElement Parent
		{
			get
			{
#warning Parent_Is_NotImpl
				throw new NotImplementedException("Parent is not implemented");
			}
		}

		public IPhysicalStorage Storage
		{
			get
			{
#warning Storage_Is_NotImpl
				throw new NotImplementedException("Storage is not implemented");
			}
		}

		public void RegisterChild(IDiscriminatedElement child)
		{
#warning RegisterChild_Is_NotImpl
			throw new NotImplementedException("RegisterChild is not implemented");
		}

		public void Describe(StringBuilder builder, string indent, int depth)
		{
#warning Describe_Is_NotImpl
			throw new NotImplementedException("Describe is not implemented");
		}

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

		public IEnumerable<IDiscriminatedElement> Children()
		{
#warning Children_Is_NotImpl
			throw new NotImplementedException("Children is not implemented");
		}

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

		public void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
#warning AggregateIdentities_Is_NotImpl
			throw new NotImplementedException("AggregateIdentities is not implemented");
		}

		public bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
#warning IsEquivalentLogicallyTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentLogicallyTo is not implemented");
		}

		public bool IsPhysicallyEquivalentTo(IDiscriminatedElement other)
		{
#warning IsEquivalentPhysicallyTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentPhysicallyTo is not implemented");
		}

		public static bool IsImaginaryRoot(IDiscriminatedElement element)
		{
#warning IsImaginaryRoot_Is_NotImpl
			throw new NotImplementedException("IsImaginaryRoot is not implemented");
		}
	}
}