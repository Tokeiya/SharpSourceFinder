using System;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class SourceFile : MultiDescendantsElement<IDiscriminatedElement>
	{
		public SourceFile(string path) : base(string.Empty)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException($"{nameof(path)} is invalid.");
			Path = path;
		}

		public string Path { get; }

		public override void Describe(StringBuilder stringBuilder)
		{
		}
	}
}