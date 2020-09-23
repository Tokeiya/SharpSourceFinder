using System;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class PhysicalStorage : IPhysicalStorage
	{
		public PhysicalStorage(string path)
		{
#warning PhysicalStorage_Is_NotImpl
			throw new NotImplementedException("PhysicalStorage is not implemented");
		}

		public string Path { get; }
		public bool IsEquivalentTo(IPhysicalStorage other)
		{
#warning IsEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentTo is not implemented");
		}
	}
}