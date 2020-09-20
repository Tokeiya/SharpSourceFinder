using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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