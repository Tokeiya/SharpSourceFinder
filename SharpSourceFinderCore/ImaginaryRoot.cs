using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class ImaginaryRoot : IDiscriminatedElement
	{
		private ImaginaryRoot()
		{
		}

		public static ImaginaryRoot Root { get; } = new ImaginaryRoot();

		public IDiscriminatedElement Parent =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} is not support this method or property.");

		public IPhysicalStorage Storage =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support this method or property.");

		public void RegisterChild(IDiscriminatedElement child) =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support this method or property.");

		public IEnumerable<IDiscriminatedElement> Ancestors() => Array.Empty<IDiscriminatedElement>();

		public IEnumerable<IDiscriminatedElement> AncestorsAndSelf() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support this method or property.");

		public IEnumerable<IDiscriminatedElement> Children() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support this method or property.");

		public IEnumerable<IDiscriminatedElement> Descendants() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support this method or property.");

		public IEnumerable<IDiscriminatedElement> DescendantsAndSelf() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support this method or property.");

		public QualifiedElement GetQualifiedName() =>
			throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support this method or property.");

		public void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
		}

		public bool IsLogicallyEquivalentTo(IDiscriminatedElement other) => other is ImaginaryRoot;
		public bool IsPhysicallyEquivalentTo(IDiscriminatedElement other) => other is ImaginaryRoot;

		public bool IsEquivalentTo(IDiscriminatedElement other) => other is ImaginaryRoot;

		public static bool IsImaginaryRoot(IDiscriminatedElement element) => element is ImaginaryRoot;
	}
}