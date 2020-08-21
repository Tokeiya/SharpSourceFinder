using System;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace SharpSourceFinderCore
{
	public interface IDiscriminatedElement
	{
		public static ObjectPool<StringBuilder> StringBuilderPool { get; } =
			new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());

		public static IDiscriminatedElement Root { get; } = new ImaginaryRoot();

		string Identity { get; }

		IDiscriminatedElement Parent { get; }

		public static bool IsImaginaryRoot(IDiscriminatedElement element) => element is ImaginaryRoot;

		void Describe(StringBuilder stringBuilder);
		string Describe();

		private class ImaginaryRoot : IDiscriminatedElement
		{
			public void Describe(StringBuilder stringBuilder) =>
				throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

			public string Describe() =>
				throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

			public string Identity =>
				throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");

			public IDiscriminatedElement Parent =>
				throw new NotSupportedException($"{nameof(ImaginaryRoot)} isn't support any methods and properties.");
		}
	}
}