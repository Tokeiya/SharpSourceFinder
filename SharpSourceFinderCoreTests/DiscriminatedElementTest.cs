using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class DiscriminatedElementInterfaceTest
	{
		protected readonly ITestOutputHelper Output;

		protected DiscriminatedElementInterfaceTest(ITestOutputHelper output) => Output = output;

		protected abstract IEnumerable<(DiscriminatedElement sample, IPhysicalStorage expected)>
			GenerateStorageGetTestSamples();

		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)>
			GenerateLogicallyTransitiveSample();

		[Trait("TestLayer",nameof(IDiscriminatedElement))]
		[Fact]
		public void LogicallyTransitiveTest()
		{
			GenerateLogicallyTransitiveSample().Any().IsTrue();

			foreach (var (x,y,z) in GenerateLogicallyTransitiveSample())
			{
				x.IsLogicallyEquivalentTo(y).IsTrue();
				y.IsLogicallyEquivalentTo(z).IsTrue();
				x.IsLogicallyEquivalentTo(z).IsTrue();
			}
		}




		protected virtual IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
			GenerateLogicallySymmetricSample()
		{
			foreach (var (x, y, z) in GenerateLogicallyTransitiveSample())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		[Trait("TestLayer",nameof(IDiscriminatedElement))]
		[Fact]
		public void LogicallySymmetricTest()
		{
			GenerateLogicallySymmetricSample().Any().IsTrue();

			foreach (var (x,y) in GenerateLogicallySymmetricSample())
			{
				x.IsLogicallyEquivalentTo(y).IsTrue();
				y.IsLogicallyEquivalentTo(x).IsTrue();
			}
		}


		protected virtual IEnumerable<IDiscriminatedElement> GenerateLogicallyReflexiveSample()
		{
			foreach (var (x, y, z) in GenerateLogicallyTransitiveSample())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		[Trait("TestLayer",nameof(IDiscriminatedElement))]
		[Fact]
		public void LogicallyReflexiveTest()
		{
			GenerateLogicallyReflexiveSample().Any().IsTrue();

			foreach (var element in GenerateLogicallyReflexiveSample())
			{
				element.IsLogicallyEquivalentTo(element).IsTrue();

			}
		}


		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
			GenerateLogicallyInEquivalentSample();

		[Trait("TestLayer",nameof(IDiscriminatedElement))]
		[Fact]
		public void LogicallyInEquivalentTest()
		{
			GenerateLogicallyInEquivalentSample().Any().IsTrue();

			foreach (var (x,y) in GenerateLogicallyInEquivalentSample())
			{
				x.IsLogicallyEquivalentTo(y).IsFalse();
				x.IsPhysicallyEquivalentTo(y).IsFalse();

				y.IsLogicallyEquivalentTo(x).IsFalse();
				y.IsPhysicallyEquivalentTo(x).IsFalse();
			}
		}


		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)>
			GeneratePhysicallyTransitiveSample();

		[Fact]
		public void PhysicallyTransitiveTest()
		{
			GeneratePhysicallyTransitiveSample().Any().IsTrue();

			foreach (var (x,y,z) in GeneratePhysicallyTransitiveSample())
			{
				x.IsPhysicallyEquivalentTo(y).IsTrue();
				x.IsLogicallyEquivalentTo(y).IsTrue();

				y.IsPhysicallyEquivalentTo(z).IsTrue();
				y.IsLogicallyEquivalentTo(z).IsTrue();

				x.IsPhysicallyEquivalentTo(z).IsTrue();
				x.IsLogicallyEquivalentTo(z).IsTrue();
			}
		}


		protected virtual IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
			GeneratePhysicallySymmetricSample()
		{
			foreach (var (x,y,z) in GeneratePhysicallyTransitiveSample())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		[Fact]
		public void PhysicallySymmetricTest()
		{
			GeneratePhysicallySymmetricSample().Any().IsTrue();

			foreach (var (x,y) in GeneratePhysicallySymmetricSample())
			{
				x.IsPhysicallyEquivalentTo(y).IsTrue();
				x.IsLogicallyEquivalentTo(y).IsTrue();

				y.IsPhysicallyEquivalentTo(x).IsTrue();
				y.IsLogicallyEquivalentTo(x).IsTrue();
			}
		}






		protected virtual IEnumerable<IDiscriminatedElement> GeneratePhysicallyReflexiveSample()
		{
			foreach (var (x,y,z) in GeneratePhysicallyTransitiveSample())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
			GeneratePhysicallyInEqualitySample();



	}
}