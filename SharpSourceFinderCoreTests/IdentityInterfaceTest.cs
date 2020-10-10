using ChainingAssertion;
using System.Collections.Generic;
using System.Linq;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class IdentityInterfaceTest
	{
		protected readonly ITestOutputHelper Output;

		protected IdentityInterfaceTest(ITestOutputHelper output) => Output = output;

		protected abstract void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected);
		protected abstract void AreEqual(IQualified actual, IQualified expected);

		protected abstract IEnumerable<(IIdentity x, IIdentity y, IIdentity z)>
			GenerateTransitiveSample();


		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void TransitiveTest()
		{
			GenerateTransitiveSample().Any().IsTrue();

			foreach (var (x, y, z) in GenerateTransitiveSample())
			{
				x.IsEquivalentTo(y).IsTrue();
				y.IsEquivalentTo(z).IsTrue();
				x.IsEquivalentTo(z).IsTrue();
			}
		}


		protected virtual IEnumerable<(IIdentity x, IIdentity y)>
			GenerateSymmetricSample()
		{
			foreach (var (x, y, z) in GenerateTransitiveSample())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void SymmetricTest()
		{
			GenerateSymmetricSample().Any().IsTrue();

			foreach (var (x, y) in GenerateSymmetricSample())
			{
				x.IsEquivalentTo(y).IsTrue();
				y.IsEquivalentTo(x).IsTrue();
			}
		}


		protected virtual IEnumerable<IIdentity> GenerateReflexiveSample()
		{
			foreach (var (x, y, z) in GenerateTransitiveSample())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void ReflexiveTest()
		{
			GenerateReflexiveSample().Any().IsTrue();

			foreach (var element in GenerateReflexiveSample())
				element.IsEquivalentTo(element).IsTrue();
		}


		protected abstract IEnumerable<(IIdentity x, IIdentity y)>
			GenerateInEquivalentSample();

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void InEquivalentTest()
		{
			GenerateInEquivalentSample().Any().IsTrue();

			foreach (var (x, y) in GenerateInEquivalentSample())
			{
				x.IsEquivalentTo(y).IsFalse();
				y.IsEquivalentTo(x).IsFalse();
			}
		}


		protected abstract
			IEnumerable<(IIdentity sample, string expectedName, IdentityCategories expectedCategory, IQualified
				expectedFrom)> GenerateSample();


		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void NameGetterTest()
		{
			GenerateSample().Any().IsTrue();

			foreach (var (sample, expected, _, _) in GenerateSample()) sample.Name.Is(expected);
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void CategoryGetterTest()
		{
			GenerateSample().Any().IsTrue();

			foreach (var (sample, _, expected, _) in GenerateSample()) sample.Category.Is(expected);
		}

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void FromGetterTest()
		{
			GenerateSample().IsNotEmpty();

			foreach (var (sample, _, _, expected) in GenerateSample()) AreEqual(sample.From, expected);
		}

		protected abstract IEnumerable<(IIdentity sample, int expected)> GenerateOrderSample();

		[Trait("TestLayer", nameof(IIdentity))]
		[Fact]
		public void OrderGetterTest()
		{
			GenerateOrderSample().IsNotEmpty();

			foreach (var (sample, expected) in GenerateOrderSample()) sample.Order.Is(expected);
		}
	}
}