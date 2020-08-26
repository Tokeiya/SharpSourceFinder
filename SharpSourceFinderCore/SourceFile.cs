using System;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class SourceFile
	{
		public SourceFile(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException($"{nameof(path)} is invalid.");
			Path = path;
		}

		public string Path { get; }
	}
}