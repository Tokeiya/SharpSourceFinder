﻿using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IDiscriminatedElement
	{
		IDiscriminatedElement Parent { get; }
		IPhysicalStorage Storage { get; }
		void RegisterChild(IDiscriminatedElement child);
		void Describe(StringBuilder builder, string indent, int depth);
		string Describe(string indent = "\t");
		IEnumerable<IDiscriminatedElement> Ancestors();
		IEnumerable<IDiscriminatedElement> AncestorsAndSelf();
		IEnumerable<IDiscriminatedElement> Children();
		IEnumerable<IDiscriminatedElement> Descendants();
		IEnumerable<IDiscriminatedElement> DescendantsAndSelf();
		Qualified GetQualifiedName();
		void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator);

		bool IsEquivalentLogicallyTo(IDiscriminatedElement other);
		bool IsEquivalentPhysicallyTo(IDiscriminatedElement other);
	}
}