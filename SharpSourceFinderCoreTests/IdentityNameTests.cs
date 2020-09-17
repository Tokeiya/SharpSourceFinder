using System;
using System.Collections.Generic;
using ChainingAssertion;
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
			var identity = new IdentityName(root, "Foo");

			yield return (identity, new IDiscriminatedElement[] { root });

		}

		[Fact]
		public void DescribeTest()
		{
			var root = CreateStandardSample();
			var name = new IdentityName(root, SampleNameSpace);

			name.Describe().Is(SampleNameSpace);
		}

		[Fact]
		public void InvalidNameTest()
		{
			var root = CreateStandardSample();

			Assert.Throws<ArgumentException>(() => new IdentityName(root, ""));
			Assert.Throws<ArgumentException>(() => new IdentityName(root, " "));
			Assert.Throws<ArgumentException>(() => new IdentityName(root, "\t"));
		}

		[Fact]
		public void IdentityTest()
		{
			var sample = new IdentityName(CreateStandardSample(), "Hoge");
			sample.Identity.Is("Hoge");
		}


		public IdentityNameTests(ITestOutputHelper output) : base(output)
		{
		}
	}
}