using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class Qualified : IEquatable<Qualified>
	{
		protected Qualified()
		{
#warning Qualified_Is_NotImpl
			throw new NotImplementedException("Qualified is not implemented");
		}

		public abstract IReadOnlyList<Identity> Identities { get; }
		public bool Equals(Qualified? other)
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

		public static bool operator ==(Qualified x, Qualified y)
		{
#warning ==_Is_NotImpl
			throw new NotImplementedException("== is not implemented");
		}

		public static bool operator !=(Qualified x, Qualified y)
		{
#warning !=_Is_NotImpl
			throw new NotImplementedException("!= is not implemented");
		}
	}
}