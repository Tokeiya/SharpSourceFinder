using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class QualifiedInterfaceTest
	{
		protected readonly ITestOutputHelper Output;

		protected QualifiedInterfaceTest(ITestOutputHelper output) => Output = output;

		protected abstract void AreEqual(IQualified actual, IQualified expected);
		protected abstract void AreEqual(IIdentity actual, IIdentity expected);
		protected abstract void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected);

		protected abstract IEnumerable<(IQualified sample, IPhysicalStorage expected)> GenerateStorageSample();

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void StorageGetterTest()
		{
			GenerateStorageSample().IsNotEmpty();

			foreach (var (sample, expected) in GenerateStorageSample()) AreEqual(sample.Storage, expected);
		}

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

		protected abstract IEnumerable<(IQualified x, IQualified y, IQualified z)> GenerateLogicalEquivalentSample();
		protected abstract IEnumerable<(IQualified x, IQualified y)> GenerateLogicalInEquivalentSample();

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void LogicalTransitiveTest()
		{
			GenerateLogicalEquivalentSample().IsNotEmpty();

			foreach (var (x, y, z) in GenerateLogicalEquivalentSample())
			{
				x.IsLogicallyEquivalentTo(y).IsTrue();
				y.IsLogicallyEquivalentTo(z).IsTrue();
				x.IsLogicallyEquivalentTo(z).IsTrue();
			}
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void LogicalSymmetricTest()
		{
			GenerateLogicalEquivalentSample().IsNotEmpty();

			foreach (var (x, y, _) in GenerateLogicalEquivalentSample())
			{
				x.IsLogicallyEquivalentTo(y).IsTrue();
				y.IsLogicallyEquivalentTo(x).IsTrue();
			}
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void LogicalReflexiveTest()
		{
			GenerateLogicalEquivalentSample().IsNotEmpty();

			foreach (var (x, _, _) in GenerateLogicalEquivalentSample()) x.IsLogicallyEquivalentTo(x).IsTrue();
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void LogicalInEquivalentTest()
		{
			GenerateLogicalInEquivalentSample().IsNotEmpty();

			foreach (var (x, y) in GenerateLogicalInEquivalentSample())
			{
				x.IsLogicallyEquivalentTo(y).IsFalse();
				x.IsPhysicallyEquivalentTo(y).IsFalse();

				y.IsLogicallyEquivalentTo(x).IsFalse();
				y.IsPhysicallyEquivalentTo(x).IsFalse();
			}
		}

		protected abstract IEnumerable<(IQualified x, IQualified y, IQualified z)> GeneratePhysicalEquivalentSample();
		protected abstract IEnumerable<(IQualified x, IQualified y)> GeneratePhysicalInEquivalentSample();

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void PhysicalTransitiveTest()
		{
			GeneratePhysicalEquivalentSample().IsNotEmpty();

			foreach (var (x, y, z) in GeneratePhysicalEquivalentSample())
			{
				x.IsPhysicallyEquivalentTo(y).IsTrue();
				x.IsLogicallyEquivalentTo(y).IsTrue();

				y.IsPhysicallyEquivalentTo(z).IsTrue();
				y.IsLogicallyEquivalentTo(z).IsTrue();

				x.IsPhysicallyEquivalentTo(z).IsTrue();
				x.IsLogicallyEquivalentTo(z).IsTrue();
			}
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void PhysicalSymmetricTest()
		{
			GeneratePhysicalEquivalentSample().IsNotEmpty();

			foreach (var (x, y, _) in GeneratePhysicalEquivalentSample())
			{
				x.IsPhysicallyEquivalentTo(y).IsTrue();
				x.IsLogicallyEquivalentTo(y).IsTrue();

				y.IsPhysicallyEquivalentTo(x).IsTrue();
				y.IsLogicallyEquivalentTo(x).IsTrue();
			}
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void PhysicalReflexiveTest()
		{
			GeneratePhysicalEquivalentSample().IsNotEmpty();

			foreach (var (x, _, _) in GeneratePhysicalEquivalentSample())
			{
				x.IsPhysicallyEquivalentTo(x).IsTrue();
				x.IsLogicallyEquivalentTo(x).IsTrue();
			}
		}

		[Trait("TestLayer", nameof(IQualified))]
		[Fact]
		public void PhysicalInEquivalentTest()
		{
			GeneratePhysicalInEquivalentSample().IsNotEmpty();

			foreach (var (x, y) in GeneratePhysicalInEquivalentSample())
			{
				x.IsPhysicallyEquivalentTo(y).IsFalse();
				y.IsPhysicallyEquivalentTo(x).IsFalse();
			}
		}
	}
}