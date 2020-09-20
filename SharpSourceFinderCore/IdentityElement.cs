﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityElement : Identity, IDiscriminatedElement
	{
		public IdentityElement(IDiscriminatedElement parent, string name):base(name)
		{
#warning IdentityElement_Is_NotImpl
			throw new NotImplementedException("IdentityElement is not implemented");
		}
		public IDiscriminatedElement Parent { get; }
		public IPhysicalStorage Storage { get; }

		public override IdentityCategories Category { get; }

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

		public bool IsEquivalentLogicallyTo(IDiscriminatedElement other)
		{
#warning IsEquivalentLogicallyTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentLogicallyTo is not implemented");
		}

		public bool IsEquivalentPhysicallyTo(IDiscriminatedElement other)
		{
#warning IsEquivalentPhysicallyTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentPhysicallyTo is not implemented");
		}

	}
}