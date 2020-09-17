using System;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;

namespace SharpSourceFinderCoreTests
{
	public class SourceFileTests
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
			CreateDefaultSample().Describe(bld, "\t", 0);
			bld.ToString().Is(string.Empty);
		}
	}
}