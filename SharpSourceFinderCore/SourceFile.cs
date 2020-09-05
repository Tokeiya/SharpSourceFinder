using System;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class SourceFile : MultiDescendantsElement<IDiscriminatedElement>,IEquatable<SourceFile>
	{
		public SourceFile(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException($"{nameof(path)} is invalid.");
			Path = path;
		}

		public string Path { get; }

		public override void Describe(StringBuilder stringBuilder)
		{

		}
		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is SourceFile other && Equals(other);
		}

		public override int GetHashCode()
		{
			return Path.GetHashCode();
		}

		public static bool operator ==(SourceFile x, SourceFile y) => x.Path == y.Path;

		public static bool operator !=(SourceFile x, SourceFile y) => x.Path != y.Path;

		public bool Equals(SourceFile? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Path == other.Path;
		}
	}
}