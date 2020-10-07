using Microsoft.VisualBasic.CompilerServices;
using ChainingAssertion;


namespace SharpSourceFinderCoreTests
{
	
	public class EquivalentTestHelperTest
	{
		private class TestMock
		{
			TestMock(int value) => Value = value;
			public int Value { get; }

			public int EqualCount { get; private set; }
			public int OpEqCount { get; private set; }
			public int OpInEqCount { get; private set; }

			public int GetHashCodeCount { get; private set; }

			public void ResetCounter() => (EqualCount, OpEqCount, OpInEqCount, GetHashCodeCount) = (0, 0, 0, 0);

			public override bool Equals(object? obj)
			{
				++EqualCount;

				return ReferenceEquals(this, obj);

			}
		}
	}
}