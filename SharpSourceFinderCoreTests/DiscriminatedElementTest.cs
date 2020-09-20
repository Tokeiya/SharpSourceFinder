using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class DiscriminatedElementTest<T> where T : DiscriminatedElement
	{
		protected readonly ITestOutputHelper Output;

		protected DiscriminatedElementTest(ITestOutputHelper output) => Output = output;

		protected abstract void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected);

		protected abstract IEnumerable<(T actual, IReadOnlyList<IDiscriminatedElement> expectedAncestors)>
			GetTestSamples();

		protected abstract IEnumerable<(T actual, IReadOnlyList<IIdentity> expected)> GenerateQualifiedNameTest();

		protected abstract IEnumerable<(T x, T y, T z)> GenerateIndividualEquivalentSamples();
		protected abstract IEnumerable<(T x, T y)> GenerateIndividualInEquivalentSamples();
		protected abstract IEnumerable<(T x, T y, T z)> GenerateEquivalentIncludeAncestorsSamples();
		protected abstract IEnumerable<(T x, T y)> GenerateInEquivalentIncludeAncestorsSamples();

		protected abstract IEnumerable<(T x, T y, T z)> GenerateFullyEquivalentSamples();
		protected abstract IEnumerable<(T x, T y)> GenerateFullyInEquivalentSamples();

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void IsIndividualEquivalentTest()
		{
			var flg = false;

			foreach (var (x,y,z)in GenerateIndividualEquivalentSamples())
			{
				flg = true;
				x.IsIndividualEquivalentTo(x).IsTrue();
				y.IsIndividualEquivalentTo(y).IsTrue();
				z.IsIndividualEquivalentTo(z).IsTrue();

				x.IsIndividualEquivalentTo(y).IsTrue();
				y.IsIndividualEquivalentTo(x).IsTrue();

				y.IsIndividualEquivalentTo(z).IsTrue();
				z.IsIndividualEquivalentTo(y).IsTrue();

				x.IsIndividualEquivalentTo(z).IsTrue();
				z.IsIndividualEquivalentTo(x).IsTrue();
			}

			flg.IsTrue();
		}

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void IsIndividualInEquivalentTest()
		{
			var flg = false;

			foreach (var (x,y) in GenerateIndividualInEquivalentSamples())
			{
				flg = true;
				x.IsIndividualEquivalentTo(y).IsFalse();
				y.IsIndividualEquivalentTo(x).IsFalse();
			}

			flg.IsTrue();
		}

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void IsEquivalentIncludeAncestorsTest ()
		{
			var flg = false;

			foreach (var (x,y,z) in GenerateEquivalentIncludeAncestorsSamples())
			{
				flg = true;

				x.IsIndividualEquivalentTo(x).IsTrue();
				x.IsEquivalentToIncludeAncestors(x).IsTrue();

				y.IsIndividualEquivalentTo(y).IsTrue();
				y.IsEquivalentToIncludeAncestors(y).IsTrue();

				z.IsIndividualEquivalentTo(z).IsTrue();
				z.IsEquivalentToIncludeAncestors(z).IsTrue();


				x.IsIndividualEquivalentTo(y).IsTrue();
				x.IsEquivalentToIncludeAncestors(y).IsTrue();

				y.IsIndividualEquivalentTo(x).IsTrue();
				y.IsEquivalentToIncludeAncestors(x).IsTrue();

				y.IsIndividualEquivalentTo(z).IsTrue();
				y.IsEquivalentToIncludeAncestors(z).IsTrue();

				z.IsIndividualEquivalentTo(y).IsTrue();
				z.IsEquivalentToIncludeAncestors(y).IsTrue();

				x.IsIndividualEquivalentTo(z).IsTrue();
				x.IsEquivalentToIncludeAncestors(z).IsTrue();

				z.IsIndividualEquivalentTo(x).IsTrue();
				z.IsEquivalentToIncludeAncestors(x).IsTrue();

			}

			flg.IsTrue();
		}

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void IsInEquivalentIncludeAncestorsTest()
		{
			var flg = false;

			foreach (var (x,y) in GenerateInEquivalentIncludeAncestorsSamples())
			{
				flg = true;

				x.IsEquivalentToIncludeAncestors(y).IsFalse();
				y.IsEquivalentToIncludeAncestors(x).IsFalse();
			}
			flg.IsTrue();
		}

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void IsFullyEquivalentToTest()
		{
			var flg = false;

			foreach ((T x, T y, T z)  in GenerateFullyEquivalentSamples())
			{
				flg = true;

				x.IsIndividualEquivalentTo(x).IsTrue();
				x.IsEquivalentToIncludeAncestors(x).IsTrue();
				x.IsFullyEquivalentTo(x).IsTrue();

				y.IsIndividualEquivalentTo(y).IsTrue();
				y.IsEquivalentToIncludeAncestors(y).IsTrue();
				y.IsFullyEquivalentTo(y).IsTrue();

				z.IsIndividualEquivalentTo(z).IsTrue();
				z.IsEquivalentToIncludeAncestors(z).IsTrue();
				z.IsFullyEquivalentTo(z).IsTrue();

				x.IsIndividualEquivalentTo(y).IsTrue();
				x.IsEquivalentToIncludeAncestors(y).IsTrue();
				x.IsFullyEquivalentTo(y).IsTrue();

				y.IsIndividualEquivalentTo(x).IsTrue();
				y.IsEquivalentToIncludeAncestors(x).IsTrue();
				y.IsFullyEquivalentTo(x).IsTrue();


				y.IsIndividualEquivalentTo(z).IsTrue();
				y.IsEquivalentToIncludeAncestors(z).IsTrue();
				y.IsFullyEquivalentTo(z).IsTrue();


				z.IsIndividualEquivalentTo(y).IsTrue();
				z.IsEquivalentToIncludeAncestors(y).IsTrue();
				z.IsFullyEquivalentTo(y).IsTrue();


				x.IsIndividualEquivalentTo(z).IsTrue();
				x.IsEquivalentToIncludeAncestors(z).IsTrue();
				x.IsFullyEquivalentTo(z).IsTrue();


				z.IsIndividualEquivalentTo(x).IsTrue();
				z.IsEquivalentToIncludeAncestors(x).IsTrue();
				z.IsFullyEquivalentTo(x).IsTrue();

			}

			flg.IsTrue();

		}


		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void IsFullyInEquivalentToTest()
		{
			var flg = false;

			foreach (var (x,y)in GenerateFullyInEquivalentSamples())
			{
				flg = true;
				x.IsFullyEquivalentTo(y).IsFalse();
				y.IsFullyEquivalentTo(x).IsFalse();
			}
			flg.IsTrue();

		}




		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void StringReturenedDescribeTest()
		{
			var bld = new StringBuilder();

			foreach (var (actual, _) in GetTestSamples())
			{
				bld.Clear();
				actual.Describe(bld, "\t", 0);
				actual.Describe().Is(bld.ToString());

				bld.Clear();
				actual.Describe(bld, "  ", 0);
				actual.Describe("  ").Is(bld.ToString());
			}
		}

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void AncestorsTest()
		{
			foreach (var (actual, expected) in GetTestSamples())
			{
				var output = actual.Ancestors().ToArray();

				output.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(output[i], expected[i]);
			}
		}

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void AncestorsAdnSelfTest()
		{
			foreach (var (sample, expected) in GetTestSamples())
			{
				var actual = sample.AncestorsAndSelf().ToArray();
				actual[0].IsSameReferenceAs(sample);

				actual.Length.Is(expected.Count + 1);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i + 1], expected[i]);
			}
		}

		[Trait("Type", "DiscriminatedElement")]
		[Fact]
		public void DescendantsAndSelfTest()
		{
			foreach (var (sample, _) in GetTestSamples())
			{
				var expected = sample.Descendants().ToArray();
				var actual = sample.DescendantsAndSelf().ToArray();

				actual.Length.Is(expected.Length + 1);

				for (int i = 0; i < expected.Length; i++) AreEqual(actual[i + 1], expected[i]);
			}
		}

		[Fact]
		[Trait("Type","DiscriminatedElement")]
		public virtual void GetQualifiedNameTest()
		{
			static void areEqual(IIdentity actual, IIdentity expected)
			{
				actual.Identity.Is(expected.Identity);
				actual.IdentityCategory.Is(expected.IdentityCategory);
			}

			foreach (var (sample,expected) in GenerateQualifiedNameTest())
			{
				var actual = sample.GetQualifiedName();
				actual.Identities.Count.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++)
				{
					areEqual(actual.Identities[i],expected[i]);
				}
			}
		}

	}
}