using System;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class Identity : IEquatable<Identity>

	{
		protected Identity(string name)
		{
#warning Identity_Is_NotImpl
			throw new NotImplementedException("Identity is not implemented");
		}
		public string Name
		{
			get
			{
#warning Name_Is_NotImpl
				throw new NotImplementedException("Name is not implemented");
			}
		}

		public abstract IdentityCategories Category { get; }

		public bool Equals(Identity? other)
		{
#warning Equals_Is_NotImpl
			throw new NotImplementedException("Equals is not implemented");
		}

		public override bool Equals(object? obj)
		{
#warning Equals_Is_NotImpl
			throw new NotImplementedException("Equals is not implemented");
		}

		public override int GetHashCode()
		{
#warning GetHashCode_Is_NotImpl
			throw new NotImplementedException("GetHashCode is not implemented");
		}

		public static bool operator ==(Identity x, Identity y)
		{
#warning ==_Is_NotImpl
			throw new NotImplementedException("== is not implemented");
		}

		public static bool operator !=(Identity x, Identity y)
		{
#warning !=_Is_NotImpl
			throw new NotImplementedException("!= is not implemented");
		}
	}
}