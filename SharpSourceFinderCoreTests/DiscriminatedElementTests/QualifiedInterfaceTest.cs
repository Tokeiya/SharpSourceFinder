using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public abstract class QualifiedInterfaceTest
	{
		protected readonly ITestOutputHelper Output;

		protected QualifiedInterfaceTest(ITestOutputHelper output) => Output = output;

		protected abstract void AreEqual(IQualified actual, IQualified expected);
		protected abstract void AreEqual(IIdentity actual, IIdentity expected);
		protected abstract void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected);


		protected abstract IEnumerable<(IQualified sample, IReadOnlyList<IIdentity> expected)>
			GenerateIdentitiesSample();

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void IdentitiesGetterTest()
		{
			GenerateIdentitiesSample().IsNotEmpty();

			foreach ((IQualified sample, IReadOnlyList<IIdentity> expected) in GenerateIdentitiesSample())
			{
				var actual = sample.Identities;
				actual.Count.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}

		protected abstract IEnumerable<(IQualified x, IQualified y, IQualified z)> GenerateEquivalentSample();
		protected abstract IEnumerable<(IQualified x, IQualified y)> GenerateInEquivalentSample();

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void TransitiveTest()
		{
			GenerateEquivalentSample().IsNotEmpty();

			foreach (var (x, y, z) in GenerateEquivalentSample())
			{
				x.IsEquivalentTo(y).IsTrue();
				y.IsEquivalentTo(z).IsTrue();
				x.IsEquivalentTo(z).IsTrue();
			}
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void SymmetricTest()
		{
			GenerateEquivalentSample().IsNotEmpty();

			foreach (var (x, y, _) in GenerateEquivalentSample())
			{
				x.IsEquivalentTo(y).IsTrue();
				y.IsEquivalentTo(x).IsTrue();
			}
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void ReflexiveTest()
		{
			GenerateEquivalentSample().IsNotEmpty();

			foreach (var (x, _, _) in GenerateEquivalentSample()) x.IsEquivalentTo(x).IsTrue();
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void InEquivalentTest()
		{
			GenerateInEquivalentSample().IsNotEmpty();

			foreach (var (x, y) in GenerateInEquivalentSample())
			{
				x.IsEquivalentTo(y).IsFalse();

				y.IsEquivalentTo(x).IsFalse();
			}
		}
	}
}