using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using FastEnumUtility;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class IdentityNameTests:TerminalElementTest<IdentityName>
	{
		private const string SamplePath = @"G:\Hoge\Moge.cs";
		const string SampleNameSpace = "Tokeiya3";


		static SourceFile CreateStandardSample(string path = SamplePath)
		{
			var file = new SourceFile(path);
			return file;
		}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) => ReferenceEquals(actual, expected);

		protected override IEnumerable<(IdentityName actual, IReadOnlyList<IDiscriminatedElement> expectedAncestors)> GetTestSamples()
		{
			var root = CreateStandardSample();
			var identity = new IdentityName(root, IdentityCategories.Namespace,"Foo");

			yield return (identity, new IDiscriminatedElement[] { root });
		}

		protected override IEnumerable<(IdentityName actual, IReadOnlyList<IIdentity> expected)> GenerateQualifiedNameTest()
		{
			foreach (var cat in FastEnum.GetValues<IdentityCategories>())
			{
				var sample = new IdentityName(CreateStandardSample(), cat, "Hoge");
				var ary = new[] { new IdentityName(CreateStandardSample(), cat, "Hoge"), };

				yield return (sample, ary);
			}
		}


		[Fact]
		public void DescribeTest()
		{
			var root = CreateStandardSample();
			var name = new IdentityName(root, IdentityCategories.Namespace,SampleNameSpace);

			name.Describe().Is(SampleNameSpace);
		}

		[Fact]
		public void InvalidNameTest()
		{
			var root = CreateStandardSample();

			Assert.Throws<ArgumentException>(() => new IdentityName(root, IdentityCategories.Namespace,""));
			Assert.Throws<ArgumentException>(() => new IdentityName(root, IdentityCategories.Namespace," "));
			Assert.Throws<ArgumentException>(() => new IdentityName(root,IdentityCategories.Namespace, "\t"));

			var outValue = (IdentityCategories)(FastEnum.GetValues<IdentityCategories>().Select(x => (int)x).Max() + 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => new IdentityName(root, 0, "hoge"));
			Assert.Throws<ArgumentOutOfRangeException>(() => new IdentityName(root, outValue, "hoge"));
		}

		[Fact]
		public void IdentityTest()
		{
			var sample = new IdentityName(CreateStandardSample(),IdentityCategories.Namespace, "Hoge");
			sample.Identity.Is("Hoge");
		}

		[Fact]
		public void CategoryTest()
		{
			foreach (var cat in FastEnum.GetValues<IdentityCategories>())
			{
				var sample = new IdentityName(CreateStandardSample(), cat, "Hoge");
				sample.IdentityCategory.Is(cat);
			}
		}



		public IdentityNameTests(ITestOutputHelper output) : base(output)
		{
		}
	}
}