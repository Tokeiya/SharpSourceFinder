using ChainingAssertion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SharpSourceFinderCoreTests
{
	public class EquivalentTestHelperTest
	{
		class Mock
		{
			public Mock(int value) => Value = value;

			public int Value { get; }
			public int EqCount { get; private set; }
			public int OpEqCount { get; private set; }
			public int OpInEqCount { get; private set; }
			public int GetHashCount { get; private set; }
			public void ResetCount() => (OpEqCount, OpInEqCount, EqCount, GetHashCount) = (0, 0, 0, 0);

			public override bool Equals(object? obj)
			{
				EqCount++;
				if (obj is null) return false;
				if (obj is Mock m) return m.Value == Value;
				return false;
			}

			public override int GetHashCode()
			{
				GetHashCount++;
				return Value.GetHashCode();
			}

			public static bool operator ==(Mock x, Mock y)
			{
				x.OpInEqCount++;
				y.OpInEqCount++;

				return x.Value == y.Value;
			}

			public static bool operator!=(Mock x,Mock y)
			{
				x.OpInEqCount++;
				y.OpInEqCount++;

				return x.Value != y.Value;
			}

			public void PreCheck()
			{
				ResetCount();

				EqCount.Is(0);
				GetHashCount.Is(0);
				OpEqCount.Is(0);
				OpInEqCount.Is(0);
			}
		}

		[Fact]
		public void IsObjectEqualTest()
		{
			var piv = new Mock(42);
			var other = new Mock(42);

			piv.PreCheck();
			other.PreCheck();

			EquivalentTestHelper<Mock>.IsObjectEqual(piv, other);
			piv.EqCount.Is(1);
			other.EqCount.Is(1);

			piv.OpEqCount.Is(1);
			other.OpEqCount.Is(1);

			piv.OpInEqCount.Is(1);
			other.OpInEqCount.Is(1);

			EquivalentTestHelper<Mock>.IsNotEqual(piv, new Mock(44));
		}

		[Fact]
		public void IsObjectNotEqualTest()
		{
			var piv = new Mock(42);
			var other = new Mock(84);

			piv.PreCheck();
			other.PreCheck();

			EquivalentTestHelper<Mock>.IsObjectNotEqual(piv, other);

			piv.EqCount.Is(1);
			other.EqCount.Is(1);

			piv.OpEqCount.Is(1);
			other.OpEqCount.Is(1);

			piv.OpInEqCount.Is(1);
			other.OpInEqCount.Is(1);

			EquivalentTestHelper<Mock>.IsEqual(piv, other);
		}

		[Fact]
		public void HashEqualTest()
		{
			var piv = new Mock(42);
			var other = new Mock(42);

			piv.PreCheck();
			other.PreCheck();

			EquivalentTestHelper<Mock>.HashEqual(piv, other);

			piv.GetHashCount.Is(1);
			other.GetHashCount.Is(1);

			piv.PreCheck();
			other = new Mock(44);

			EquivalentTestHelper<Mock>.HashEqual(piv, other);
		}

	}
}
