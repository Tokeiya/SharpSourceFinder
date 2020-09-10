using System;

namespace SharpSourceFinderCoreTests
{
	public class Mock : IEquatable<Mock>
	{
		public Mock(int value) => Value = value;

		public int Value { get; }

		public int ObjEqualsCount { get; private set; }
		public int EquatableCount { get; private set; }
		public int OpEqualityCount { get; private set; }
		public int OpInequalityCount { get; private set; }
		public int GetHashCodeCount { get; private set; }

		public void Reset()
		{
			ObjEqualsCount = 0;
			EquatableCount = 0;
			OpEqualityCount = 0;
			OpInequalityCount = 0;
			GetHashCodeCount = 0;
		}

		public override bool Equals(object? obj)
		{
			ObjEqualsCount++;

			if (obj is null) return false;

			return obj switch
			{
				Mock m => m.Value == Value,
				_ => false,
			};
		}

		public override int GetHashCode()
		{
			// ReSharper disable once NonReadonlyMemberInGetHashCode
			GetHashCodeCount++;
			return Value;
		}

		public static bool operator ==(Mock x, Mock y)
		{
			x.OpEqualityCount++;
			y.OpEqualityCount++;

			return x.Value == y.Value;
		}

		public static bool operator !=(Mock x, Mock y)
		{
			x.OpInequalityCount++;
			y.OpInequalityCount++;

			return x.Value != y.Value;
		}

		public bool Equals(Mock? other)
		{
			EquatableCount++;

			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return Value == other.Value;
		}
	}
}