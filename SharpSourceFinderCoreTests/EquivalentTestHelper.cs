using System;
using System.Collections.Generic;
using System.Text;
using ChainingAssertion;

namespace SharpSourceFinderCoreTests
{
	public static class EquivalentTestHelper<T>
	{
		public static void IsObjectEqual(object x, object y)
		{
			x.Equals(y).IsTrue();
			y.Equals(x).IsTrue();

			if(!(x is T)||!(y is T)) return;

			dynamic dx = x;
			dynamic dy = y;

			bool actual = dx == dy;
			actual.IsTrue();

			actual = dx != dy;
			actual.IsFalse();

		}

		public static void IsObjectNotEqual(object x, object y)
		{
			x.Equals(y).IsFalse();
			y.Equals(x).IsFalse();

			if(!(x is T)||!(y is T)) return;

			dynamic dx = x;
			dynamic dy = y;

			bool actual = dx == dy;
			actual.IsFalse();

			actual = dx != dy;
			actual.IsTrue();

		}

		public static void IsEqual(T x, T y)
		{
			dynamic dx = x;
			dynamic dy = y;

			bool actual = dx == dy;
			actual.IsTrue();

			actual = dy == dx;
			actual.IsTrue();

			actual = dx != dy;
			actual.IsFalse();

			actual = dy != dx;
			actual.IsFalse();

			x.Equals(y).IsTrue();

		}

		public static void IsNotEqual(T x, T y)
		{
			dynamic dx = x;
			dynamic dy = y;

			bool actual = dx != dy;
			actual.IsTrue();

			actual = dy != dx;
			actual.IsTrue();

			actual = dx == dy;
			actual.IsFalse();

			actual = dy == dx;
			actual.IsFalse();

			x.Equals(y).IsFalse();
		}

		public static void HashEqual(object x, object y) => x.GetHashCode().Is(y.GetHashCode());

		public static void HashNotEqual(object x, object y) => x.GetHashCode().Is(y.GetHashCode());



	}
}
