using System;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class SourceFile : MultiDescendantsElement<IDiscriminatedElement>
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
#warning Equals_Is_NotImpl
			throw new NotImplementedException("Equals is not implemented");
		}

		public override int GetHashCode()
		{
#warning GetHashCode_Is_NotImpl
			throw new NotImplementedException("GetHashCode is not implemented");
		}

		public static bool operator ==(SourceFile x, SourceFile y)
		{
#warning ==_Is_NotImpl
			throw new NotImplementedException("== is not implemented");
		}

		public static bool operator !=(SourceFile x, SourceFile y)
		{
#warning !=_Is_NotImpl
			throw new NotImplementedException("!= is not implemented");
		}
	}
}