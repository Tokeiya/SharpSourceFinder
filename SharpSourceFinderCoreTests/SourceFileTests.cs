using ChainingAssertion;
using System;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class SourceFileTests
	{
		private const string SamplePath = @"G:\Some\Bar.cs";

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
		public void RepresentationTest()
		{
			CreateDefaultSample().Representation.Is(string.Empty);
		}

		[Fact]
		public void DescribeTest()
		{
			var bld = new StringBuilder();
			CreateDefaultSample().Describe(bld);
			bld.ToString().Is(string.Empty);

		}


	}
}