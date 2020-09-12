using System;
using System.Collections.Generic;
using ChainingAssertion;
using SharpSourceFinderCoreTests;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class IdentityEquatabilityTest : EquatabilityTester<IdentityName>
	{
		private const string NameA = "Tokeiya3";
		private const string NameB = "時計屋";
		static readonly IDiscriminatedElement RootA = new SourceFile("C:\\Hoge\\Piyo.cs");
		static readonly IDiscriminatedElement RootB = new SourceFile("G:\\Foo\\Bar.cs");


		protected override IEnumerable<(IdentityName x, IdentityName y, IdentityName z)> CreateTransitivelyTestSamples()
		{
			yield return (new IdentityName(RootA, NameA), new IdentityName(RootA, NameA),
				new IdentityName(RootA, NameA));
			yield return (new IdentityName(RootB, NameB), new IdentityName(RootB, NameB),
				new IdentityName(RootB, NameB));

			yield return (new IdentityName(RootB, NameA), new IdentityName(RootA, NameA),
				new IdentityName(RootB, NameA));
		}


		protected override IEnumerable<(IdentityName x, IdentityName y)> CreateReflexivelyTestSamples()
		{
			yield return (new IdentityName(RootB, NameA), new IdentityName(RootB, NameA));
			yield return (new IdentityName(RootA, NameA), new IdentityName(RootB, NameA));
		}

		protected override IEnumerable<IdentityName> CreateSymmetricallyTestSamples()
		{
			yield return new IdentityName(RootA, NameA);
			yield return new IdentityName(RootB, NameB);
		}

		protected override IEnumerable<(IdentityName x, IdentityName y)> CreateInEqualTestSamples()
		{
			yield return (new IdentityName(RootA, NameA), new IdentityName(RootA, NameB));
		}

		protected override IEnumerable<(object x, object y)> CreateObjectEqualSamples()
		{
			foreach (var (x, y) in CreateReflexivelyTestSamples()) yield return (x, y);
		}

		protected override IEnumerable<(object x, object y)> CreateObjectInEqualSamples()
		{
			foreach (var (x, y) in CreateInEqualTestSamples()) yield return (x, y);

			yield return (new IdentityName(RootA, NameA), RootA);
		}
	}

	public class IdentityNameTests
	{
		const string SamplePath = @"G:\Hoge\Moge.cs";
		const string AnotherPath = @"G:\Foo\Bar.cs";

		const string SampleNameSpace = "Tokeiya3";
		const string AnotherNameSpace = "時計屋";


		static SourceFile CreateStandardSample(string path = SamplePath)
		{
			var file = new SourceFile(path);
			//return new NameSpace(file);
			return file;
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
	}
}