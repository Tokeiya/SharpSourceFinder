﻿using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IDiscriminatedElement
	{
		IDiscriminatedElement Parent { get; }
		IPhysicalStorage Storage { get; }
		bool IsLogicallyEquivalentTo(IDiscriminatedElement other);
		bool IsPhysicallyEquivalentTo(IDiscriminatedElement other);
		void RegisterChild(IDiscriminatedElement child);
		IEnumerable<IDiscriminatedElement> Ancestors();
		IEnumerable<IDiscriminatedElement> AncestorsAndSelf();
		IEnumerable<IDiscriminatedElement> Children();
		IEnumerable<IDiscriminatedElement> Descendants();
		IEnumerable<IDiscriminatedElement> DescendantsAndSelf();
		QualifiedElement GetQualifiedName();
		void AggregateIdentities(Stack<(ScopeCategories scope, IdentityCategories category, string identity)> accumulator);
	}
}