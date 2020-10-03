using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class DiscriminatedElementInterfaceTest<T> where T : IDiscriminatedElement
	{
		protected readonly ITestOutputHelper Output;

		protected DiscriminatedElementInterfaceTest(ITestOutputHelper output) => Output = output;

		protected abstract void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected);

		protected abstract void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected);


		protected abstract IEnumerable<(T x, T y, T z)>
			GenerateLogicallyTransitiveSample();


		[Trait("TestLayer", nameof(IDiscriminatedElement))]
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


		protected virtual IEnumerable<(T x, T y)>
			GenerateLogicallySymmetricSample()
		{
			foreach (var (x, y, z) in GenerateLogicallyTransitiveSample())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
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


		protected virtual IEnumerable<T> GenerateLogicallyReflexiveSample()
		{
			foreach (var (x, y, z) in GenerateLogicallyTransitiveSample())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void LogicallyReflexiveTest()
		{
			GenerateLogicallyReflexiveSample().Any().IsTrue();

			foreach (var element in GenerateLogicallyReflexiveSample())
				element.IsLogicallyEquivalentTo(element).IsTrue();
		}


		protected abstract IEnumerable<(T x, T y)>
			GenerateLogicallyInEquivalentSample();

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
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


		protected abstract IEnumerable<(T x, T y, T z)>
			GeneratePhysicallyTransitiveSample();

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
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


		protected virtual IEnumerable<(T x, T y)>
			GeneratePhysicallySymmetricSample()
		{
			foreach (var (x, y, z) in GeneratePhysicallyTransitiveSample())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
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

		protected virtual IEnumerable<IDiscriminatedElement> GeneratePhysicallyReflexiveSample()
		{
			foreach (var (x, y, z) in GeneratePhysicallyTransitiveSample())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
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

		protected abstract IEnumerable<(T x, T y)>
			GeneratePhysicallyInEqualitySample();

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
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

		protected abstract IEnumerable<(T sample, IDiscriminatedElement expected)>
			GenerateParentSample();

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void ParentGetterTest()
		{
			GenerateParentSample().Any().IsTrue();

			foreach (var (sample, expected) in GenerateParentSample()) AreEqual(sample.Parent, expected);
		}

		protected abstract IEnumerable<(T sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample();


		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void PhysicalStorageGetterTest()
		{
			GeneratePhysicalStorageSample().Any().IsTrue();

			foreach ((IDiscriminatedElement sample, IPhysicalStorage expected) in GeneratePhysicalStorageSample())
				AreEqual(sample.Storage, expected);
		}


		protected abstract IEnumerable<(T sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample();

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void AncestorsTest()
		{
			GenerateGetAncestorsSample().Any().IsTrue();

			foreach ((IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected) in
				GenerateGetAncestorsSample())
			{
				var actual = sample.Ancestors().ToList();

				actual.Count.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void AncestorsAndSelfTest()
		{
			GenerateGetAncestorsSample().Any().IsTrue();

			foreach (var (sample, expected) in GenerateGetAncestorsSample())
			{
				var actual = sample.AncestorsAndSelf().ToArray();
				actual.Length.Is(expected.Count + 1);

				AreEqual(actual[0], sample);
				for (int i = 1; i < expected.Count; i++) AreEqual(actual[i], expected[i - 1]);
			}
		}

		protected abstract IEnumerable<(T sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample();


		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void ChildrenTest()
		{
			GenerateChildrenSample().Any().IsTrue();

			foreach (var (sample, expected) in GenerateChildrenSample())
			{
				var actual = sample.Children().ToArray();
				actual.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}

		protected abstract IEnumerable<(T sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample();

		[Fact]
		public void DescendantsTest()
		{
			GenerateDescendantsSample().Any().IsTrue();

			foreach ((IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected) in
				GenerateDescendantsSample())
			{
				var actual = sample.Descendants().ToArray();
				actual.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}


		[Fact]
		public void DescendantsAndSelfTest()
		{
			GenerateDescendantsSample().Any().IsTrue();

			foreach (var (sample, expected) in GenerateDescendantsSample())
			{
				var actual = sample.DescendantsAndSelf().ToArray();
				actual.Length.Is(expected.Count + 1);

				AreEqual(actual[0], sample);
				for (var i = 1; i < expected.Count; i++) AreEqual(actual[i], expected[i-1]);
			}
		}

		protected abstract IEnumerable<(T sample, IQualified expected)>
			GenerateQualifiedNameSample();

		protected abstract void AreEqual(IQualified actual, IQualified expected);

		[Fact]
		public void GetQualifiedNameTest()
		{
			GenerateQualifiedNameSample().Any().IsTrue();

			foreach ((IDiscriminatedElement sample, IQualified expected) in GenerateQualifiedNameSample())
			{
				var actual = sample.GetQualifiedName();

				AreEqual(actual, expected);
			}
		}


		protected abstract
			IEnumerable<(T sample, Stack<(IdentityCategories category, string identity)>
				expected)> GenerateAggregateIdentitiesSample();

		[Fact]
		public void AggregateIdentitiesTest()
		{
			GenerateAggregateIdentitiesSample().Any().IsTrue();

			var actual = new Stack<(IdentityCategories category, string identity)>();
			foreach ((IDiscriminatedElement sample, Stack<(IdentityCategories category, string identity)> expected) in
				GenerateAggregateIdentitiesSample())
			{
				actual.Count.Is(0);

				sample.AggregateIdentities(actual);

				actual.Count.Is(expected.Count);

				while (expected.Count != 0)
				{

#pragma warning disable IDE0042 // 変数の宣言を分解
					// ReSharper disable UseDeconstruction
					var a = actual.Pop();
					var e = expected.Pop();
					// ReSharper restore UseDeconstruction
#pragma warning restore IDE0042 // 変数の宣言を分解


					a.category.Is(e.category);
					a.identity.Is(e.identity);

				}
			}
		}
	}
}