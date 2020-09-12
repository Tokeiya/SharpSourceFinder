using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainingAssertion;
using SharpSourceFinderCoreTests;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class SourceFileTests : EquatabilityTester<SourceFile>
	{
		private const string SamplePath = @"G:\Some\Bar.cs";
		private const string OtherPath = @"C:\Foo\Hoge.cs";


		static SourceFile CreateDefaultSample(string path = SamplePath) => new SourceFile(path);

		[Fact]
		public void CtorTest()
		{
			var actual = CreateDefaultSample();

			actual.Path.Is(SamplePath);

			Assert.Throws<ArgumentException>(() => new SourceFile(""));
			Assert.Throws<ArgumentException>(() => new SourceFile("  "));
		}

		[Fact]
		public void PathTest()
		{
			var actual = CreateDefaultSample();
			actual.Path.Is(SamplePath);
		}


		[Fact]
		public void DescribeTest()
		{
			var bld = new StringBuilder();
			CreateDefaultSample().Describe(bld);
			bld.ToString().Is(string.Empty);
		}


		protected override IEnumerable<(SourceFile x, SourceFile y, SourceFile z)> CreateTransitivelyTestSamples()
		{
			yield return (new SourceFile(SamplePath), new SourceFile(SamplePath), new SourceFile(SamplePath));
			yield return (new SourceFile(OtherPath), new SourceFile(OtherPath), new SourceFile(OtherPath));
		}

		protected override IEnumerable<(SourceFile x, SourceFile y)> CreateReflexivelyTestSamples()
		{
			yield return (new SourceFile(SamplePath), new SourceFile(SamplePath));
			yield return (new SourceFile(OtherPath), new SourceFile(OtherPath));
		}

		protected override IEnumerable<SourceFile> CreateSymmetricallyTestSamples()
		{
			yield return new SourceFile(SamplePath);
			yield return new SourceFile(OtherPath);
		}

		protected override IEnumerable<(SourceFile x, SourceFile y)> CreateInEqualTestSamples()
		{
			yield return (new SourceFile(SamplePath), new SourceFile(OtherPath));
		}

		protected override IEnumerable<(object x, object y)> CreateObjectEqualSamples() =>
			CreateReflexivelyTestSamples().Select(t => ((object) t.x, (object) t.y));


		protected override IEnumerable<(object x, object y)> CreateObjectInEqualSamples() =>
			CreateInEqualTestSamples().Select(t => ((object) t.x, (object) t.y));
	}
}