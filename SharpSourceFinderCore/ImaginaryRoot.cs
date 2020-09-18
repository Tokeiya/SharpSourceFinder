﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	internal class ImaginaryRoot : IDiscriminatedElement
	{
		public void RegisterChild(IDiscriminatedElement child) =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public void Describe(StringBuilder stringBuilder, string indent, int depth) =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");


		public string Describe(string indent) =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public IEnumerable<IDiscriminatedElement> Ancestors() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public IEnumerable<IDiscriminatedElement> AncestorsAndSelf() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");


		public IEnumerable<IDiscriminatedElement> Children() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public IEnumerable<IDiscriminatedElement> Descendants() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public IEnumerable<IDiscriminatedElement> DescendantsAndSelf() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public IQualified GetQualifiedName()=>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public void AggregateIdentities(Stack<(IdentityCategories category,string identity)> accumulator)=>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

		public IDiscriminatedElement Parent =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");
	}
}