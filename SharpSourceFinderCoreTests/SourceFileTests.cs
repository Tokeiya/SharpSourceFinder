using System;
using System.Text;
using ChainingAssertion;
using Xunit;
using static SharpSourceFinderCoreTests.EquivalentTestHelper<Tokeiya3.SharpSourceFinderCore.SourceFile>;

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
		public void DescribeTest()
		{
			var bld = new StringBuilder();
			CreateDefaultSample().Describe(bld);
			bld.ToString().Is(string.Empty);
		}

		[Fact]
		public void EqualsTest()
		{

			var pivot = new SourceFile(SamplePath);
			IsObjectEqual(pivot, pivot);
			IsObjectEqual(pivot, new SourceFile(SamplePath));

			IsObjectNotEqual(pivot, new SourceFile("C:\\Hoge\\Piyo.cs"));
			IsObjectNotEqual(pivot, SamplePath);
		}

		[Fact]
		public void GetHashCodeTest()
		{

			var pivot = new SourceFile(SamplePath);
			HashEqual(pivot, new SourceFile(SamplePath));

			//Who will test the tester?
			HashNotEqual(SamplePath, "C:\\Hoge\\Piyo.cs");
			HashNotEqual(pivot, new SourceFile("C:\\Hoge\\Piyo.cs"));
		}

		[Fact]
		public void OpEqTest()
		{
			var piv = new SourceFile(SamplePath);
			IsEqual(piv, piv);
			IsEqual(piv, new SourceFile(SamplePath));
			
			IsNotEqual(piv, new SourceFile("C:\\Hoge\\Piyo.cs"));
		}

	}
}