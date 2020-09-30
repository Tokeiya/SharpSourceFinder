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

		protected abstract void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected);

		protected abstract void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected);


		protected abstract IEnumerable<(DiscriminatedElement sample, IPhysicalStorage expected)>
			GenerateStorageGetTestSamples();

		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)>
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


		protected virtual IEnumerable<IDiscriminatedElement> GenerateLogicallyReflexiveSample()
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


		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
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


		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)>
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


		protected virtual IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
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

		protected abstract IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
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

		protected abstract IEnumerable<(IDiscriminatedElement sample, IDiscriminatedElement expected)>
			GenerateParentSample();

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void ParentGetterTest()
		{
			GenerateParentSample().Any().IsTrue();

			foreach (var (sample, expected) in GenerateParentSample()) AreEqual(sample.Parent, expected);
		}

		protected abstract IEnumerable<(IDiscriminatedElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample();


		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void PhysicalStorageGetterTest()
		{
			GeneratePhysicalStorageSample().Any().IsTrue();

			foreach ((IDiscriminatedElement sample, IPhysicalStorage expected) in GeneratePhysicalStorageSample())
				AreEqual(sample.Storage, expected);
		}



		protected abstract IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample(bool isContainSelf);

		[Trait("TestLayer", nameof(IDiscriminatedElement))]
		[Fact]
		public void AncestorsTest()
		{
			GenerateGetAncestorsSample(false).Any().IsTrue();

			foreach ((IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected) in
				GenerateGetAncestorsSample(false))
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
			GenerateGetAncestorsSample(true).Any().IsTrue();

			foreach (var (sample, expected) in GenerateGetAncestorsSample(true))
			{
				var actual = sample.AncestorsAndSelf().ToArray();
				actual.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}

		protected abstract IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
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

		protected abstract IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample(bool isContainSelf);

		[Fact]
		public void DescendantsTest()
		{
			GenerateDescendantsSample(false).Any().IsTrue();

			foreach ((IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected) in
				GenerateDescendantsSample(false))
			{
				var actual = sample.Descendants().ToArray();
				actual.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}

		[Fact]
		public void DescendantsAndSelfTest()
		{
			GenerateDescendantsSample(true).Any().IsTrue();

			foreach (var (sample, expected) in GenerateDescendantsSample(true))
			{
				var actual = sample.DescendantsAndSelf().ToArray();
				actual.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}

		protected abstract IEnumerable<(IDiscriminatedElement sample, QualifiedElement expected)>
			GenerateQualifiedNameSample();

		[Fact]
		public void GetQualifiedNameTest()
		{
			GenerateQualifiedNameSample().Any().IsTrue();

			foreach ((IDiscriminatedElement sample, QualifiedElement expected) in GenerateQualifiedNameSample())
			{
				var actual = sample.GetQualifiedName();

				actual.Is(expected);
			}
		}


		protected abstract
			IEnumerable<(IDiscriminatedElement sample, Stack<(IdentityCategories category, string identity)>
				expected)> GenerateAggregateIdentitiesSample();

		[Fact]
		public void AggregateIdentitiesTest()
		{
			GenerateAggregateIdentitiesSample().Any().IsTrue();

			var actual = new Stack<(IdentityCategories category, string identiy)>();
			foreach ((IDiscriminatedElement sample, Stack<(IdentityCategories category, string identity)> expected) in
				GenerateAggregateIdentitiesSample())
			{
				actual.Count.Is(0);

				sample.AggregateIdentities(actual);

				actual.Count.Is(expected.Count);

				while (expected.Count != 0)
				{
					var a = actual.Pop();
					var e = expected.Pop();

					a.category.Is(e.category);
					a.identiy.Is(e.identity);
				}
			}
		}
	}
}