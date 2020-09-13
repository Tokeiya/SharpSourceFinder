﻿using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IDiscriminatedElement
	{
		IDiscriminatedElement Parent { get; }
		public static bool IsImaginaryRoot(IDiscriminatedElement element) => element is ImaginaryRoot;
		void RegisterChild(IDiscriminatedElement child);
		void Describe(StringBuilder stringBuilder);
		string Describe();
		IEnumerable<IDiscriminatedElement> Ancestors();
		IEnumerable<IDiscriminatedElement> AncestorsAndSelf();
		IEnumerable<IDiscriminatedElement> Children();
		IEnumerable<IDiscriminatedElement> Descendants();
		IEnumerable<IDiscriminatedElement> DescendantsAndSelf();
	}
}