using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class InspectionOfEquatabilityTester : EquatabilityTester<Mock>
	{
		private readonly (Mock x, Mock y)[] _inEqualSamples =
			Enumerable.Range(0, 10).Select(i => (new Mock(i), new Mock(i + 1))).ToArray();


		private readonly ITestOutputHelper _output;

		private readonly (Mock x, Mock y)[] _reflexivelySamples =
			Enumerable.Range(0, 10).Select(i => (new Mock(i), new Mock(i))).ToArray();

		private readonly Mock[] _symmetricallySamples = Enumerable.Range(0, 10).Select(i => new Mock(i)).ToArray();

		private readonly (Mock x, Mock y, Mock z)[] _transitivelySamples =
			Enumerable.Range(0, 10).Select(i => (new Mock(i), new Mock(i), new Mock(i))).ToArray();

		public InspectionOfEquatabilityTester(ITestOutputHelper output) => _output = output;

		protected override IEnumerable<(Mock x, Mock y, Mock z)> CreateTransitivelyTestSamples()
			=> _transitivelySamples;

		protected override IEnumerable<(Mock x, Mock y)> CreateReflexivelyTestSamples() => _reflexivelySamples;
		protected override IEnumerable<Mock> CreateSymmetricallyTestSamples() => _symmetricallySamples;

		protected override IEnumerable<(Mock x, Mock y)> CreateInEqualTestSamples() => _inEqualSamples;

		protected override IEnumerable<(object x, object y)> CreateObjectEqualSamples() =>
			_reflexivelySamples.Select(t => ((object) t.x, (object) t.y));

		protected override IEnumerable<(object x, object y)> CreateObjectInEqualSamples() =>
			_inEqualSamples.Select(t => ((object) t.x, (object) t.y));

		void ResetAll()
		{
			foreach (var (x, y, z) in _transitivelySamples)
			{
				x.Reset();
				y.Reset();
				z.Reset();
			}

			foreach (var (x, y) in _reflexivelySamples)
			{
				x.Reset();
				y.Reset();
			}

			foreach (var elem in _symmetricallySamples) elem.Reset();

			foreach (var (x, y) in _inEqualSamples)
			{
				x.Reset();
				y.Reset();
			}
		}

		[Fact]
		public void InspectionOfTransitivelyTest()
		{
			ResetAll();

			TransitivelyTest();

			foreach (var (x, y, z) in _transitivelySamples)
			{
				x.OpInequalityCount.Is(2);
				y.OpInequalityCount.Is(2);
				z.OpInequalityCount.Is(2);

				x.OpEqualityCount.Is(2);
				y.OpEqualityCount.Is(2);
				z.OpEqualityCount.Is(2);

				x.ObjEqualsCount.Is(1);
				y.ObjEqualsCount.Is(1);
				z.ObjEqualsCount.Is(1);

				x.EquatableCount.Is(1);
				y.EquatableCount.Is(1);
				z.EquatableCount.Is(1);
			}
		}


		[Fact]
		public void InspectionOfSymmetricallyTest()
		{
			ResetAll();

			SymmetricallyTest();

			foreach (var elem in _symmetricallySamples)
			{
				elem.OpEqualityCount.Is(2);
				elem.EquatableCount.Is(1);
				elem.ObjEqualsCount.Is(1);
				elem.OpInequalityCount.Is(2);
			}
		}

		[Fact]
		public void InspectionOfGetHashCodeTest()
		{
			ResetAll();

			GetHashCodeTest();

			foreach (var (x, y) in _reflexivelySamples)
			{
				x.GetHashCodeCount.Is(1);
				y.GetHashCodeCount.Is(1);
			}
		}

		[Fact]
		public void InspectionOfInequalityTest()
		{
			ResetAll();

			InequalityTest();

			foreach (var (x, y) in _inEqualSamples)
			{
				x.OpEqualityCount.Is(2);
				y.OpEqualityCount.Is(2);

				x.OpInequalityCount.Is(1);
				y.OpInequalityCount.Is(1);

				x.EquatableCount.Is(1);
				y.EquatableCount.Is(1);

				x.ObjEqualsCount.Is(1);
				y.ObjEqualsCount.Is(1);
			}
		}

		[Fact]
		public void InspectionOfObjectEqualTest()
		{
			ResetAll();

			ObjectEqualTest();

			foreach (var (x, y) in _reflexivelySamples)
			{
				x.ObjEqualsCount.Is(2);
				y.ObjEqualsCount.Is(2);
			}
		}

		[Fact]
		public void InspectionOfObjectInequalityTest()
		{
			ResetAll();

			ObjectInequalityTest();

			foreach (var (x, y) in _inEqualSamples)
			{
				x.ObjEqualsCount.Is(1);
				y.ObjEqualsCount.Is(1);
			}
		}
	}
}