using System;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class StorageNotAvailable : IPhysicalStorage
	{
		private StorageNotAvailable()
		{
		}

		public static IPhysicalStorage NotAvailable { get; } = new StorageNotAvailable();

		public string Path => throw new NotSupportedException();
		public bool IsEquivalentTo(IPhysicalStorage other) => ReferenceEquals(other, this);

		public static bool IsNotAvailable(IPhysicalStorage storage) => ReferenceEquals(storage, NotAvailable);
	}
}