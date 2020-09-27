using System;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class PhysicalStorage : IPhysicalStorage
	{
		public PhysicalStorage(string path)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentException($"{nameof(path)} is null or empty.");
			Path = path;
		}

		public string Path { get; }

		public bool IsEquivalentTo(IPhysicalStorage other)
		{
#warning IsEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentTo is not implemented");
		}
	}
}