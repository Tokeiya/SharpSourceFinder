using System;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class StorageNotAvailable : IPhysicalStorage
	{
		private StorageNotAvailable()
		{
#warning StorageNotAvailable_Is_NotImpl
			throw new NotImplementedException("StorageNotAvailable is not implemented");
		}

		public static IPhysicalStorage NotAvailable
		{
			get
			{
#warning NotAvailable_Is_NotImpl
				throw new NotImplementedException("NotAvailable is not implemented");
			}
		}

		public string Path
		{
			get
			{
#warning Path_Is_NotImpl
				throw new NotImplementedException("Path is not implemented");
			}
		}

		public static bool IsNotAvailable(IPhysicalStorage storage)
		{
#warning IsNotAvailable_Is_NotImpl
			throw new NotImplementedException("IsNotAvailable is not implemented");
		}
	}
}