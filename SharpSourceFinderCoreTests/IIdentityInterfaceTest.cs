using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class IdentityInterfaceTest
	{
		protected readonly ITestOutputHelper Output;

		protected IdentityInterfaceTest(ITestOutputHelper output) => Output = output;

		protected abstract void AreEqual(IIdentity actual, IIdentity expected);

		protected abstract void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected);


		protected abstract IEnumerable<(IIdentity sample, IPhysicalStorage expected)>
			GenerateStorageGetTestSamples();

		protected abstract IEnumerable<(IIdentity x, IIdentity y, IIdentity z)>
			GenerateLogicallyTransitiveSample();


		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void LogicallyTransitiveTest()
		{
			GenerateLogicallyTransitiveSample().Any().IsTrue();

			foreach (var (x, y, z) in GenerateLogicallyTransitiveSample())
			{
				x.IsLogicallyEquivalentTo(y).IsTrue();
				y.IsLogicallyEquivalentTo(z).IsTrue();
				x.IsLogicallyEquivalentTo(z).IsTrue();
			}
		}


		protected virtual IEnumerable<(IIdentity x, IIdentity y)>
			GenerateLogicallySymmetricSample()
		{
			foreach (var (x, y, z) in GenerateLogicallyTransitiveSample())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void LogicallySymmetricTest()
		{
			GenerateLogicallySymmetricSample().Any().IsTrue();

			foreach (var (x, y) in GenerateLogicallySymmetricSample())
			{
				x.IsLogicallyEquivalentTo(y).IsTrue();
				y.IsLogicallyEquivalentTo(x).IsTrue();
			}
		}


		protected virtual IEnumerable<IIdentity> GenerateLogicallyReflexiveSample()
		{
			foreach (var (x, y, z) in GenerateLogicallyTransitiveSample())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void LogicallyReflexiveTest()
		{
			GenerateLogicallyReflexiveSample().Any().IsTrue();

			foreach (var element in GenerateLogicallyReflexiveSample())
				element.IsLogicallyEquivalentTo(element).IsTrue();
		}


		protected abstract IEnumerable<(IIdentity x, IIdentity y)>
			GenerateLogicallyInEquivalentSample();

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void LogicallyInEquivalentTest()
		{
			GenerateLogicallyInEquivalentSample().Any().IsTrue();

			foreach (var (x, y) in GenerateLogicallyInEquivalentSample())
			{
				x.IsLogicallyEquivalentTo(y).IsFalse();
				x.IsPhysicallyEquivalentTo(y).IsFalse();

				y.IsLogicallyEquivalentTo(x).IsFalse();
				y.IsPhysicallyEquivalentTo(x).IsFalse();
			}
		}


		protected abstract IEnumerable<(IIdentity x, IIdentity y, IIdentity z)>
			GeneratePhysicallyTransitiveSample();

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void PhysicallyTransitiveTest()
		{
			GeneratePhysicallyTransitiveSample().Any().IsTrue();

			foreach (var (x, y, z) in GeneratePhysicallyTransitiveSample())
			{
				x.IsPhysicallyEquivalentTo(y).IsTrue();
				x.IsLogicallyEquivalentTo(y).IsTrue();

				y.IsPhysicallyEquivalentTo(z).IsTrue();
				y.IsLogicallyEquivalentTo(z).IsTrue();

				x.IsPhysicallyEquivalentTo(z).IsTrue();
				x.IsLogicallyEquivalentTo(z).IsTrue();
			}
		}


		protected virtual IEnumerable<(IIdentity x, IIdentity y)>
			GeneratePhysicallySymmetricSample()
		{
			foreach (var (x, y, z) in GeneratePhysicallyTransitiveSample())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void PhysicallySymmetricTest()
		{
			GeneratePhysicallySymmetricSample().Any().IsTrue();

			foreach (var (x, y) in GeneratePhysicallySymmetricSample())
			{
				x.IsPhysicallyEquivalentTo(y).IsTrue();
				x.IsLogicallyEquivalentTo(y).IsTrue();

				y.IsPhysicallyEquivalentTo(x).IsTrue();
				y.IsLogicallyEquivalentTo(x).IsTrue();
			}
		}

		protected virtual IEnumerable<IIdentity> GeneratePhysicallyReflexiveSample()
		{
			foreach (var (x, y, z) in GeneratePhysicallyTransitiveSample())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void PhysicallyReflexiveTest()
		{
			GeneratePhysicallyReflexiveSample().Any().IsTrue();

			foreach (var element in GeneratePhysicallyReflexiveSample())
			{
				element.IsPhysicallyEquivalentTo(element).IsTrue();
				element.IsLogicallyEquivalentTo(element).IsTrue();
			}
		}

		protected abstract IEnumerable<(IIdentity x, IIdentity y)>
			GeneratePhysicallyInEqualitySample();

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void PhysicallyInEquivalentTest()
		{
			GeneratePhysicallyInEqualitySample().Any().IsTrue();

			foreach (var (x, y) in GeneratePhysicallyInEqualitySample())
			{
				x.IsPhysicallyEquivalentTo(y).IsFalse();
				y.IsPhysicallyEquivalentTo(x).IsFalse();
			}
		}

		protected abstract IEnumerable<(IIdentity sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample();


		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void PhysicalStorageGetterTest()
		{
			GeneratePhysicalStorageSample().Any().IsTrue();

			foreach ((IIdentity sample, IPhysicalStorage expected) in GeneratePhysicalStorageSample())
				AreEqual(sample.Storage, expected);
		}






	}
}