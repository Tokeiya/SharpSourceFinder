using Tokeiya3.SharpSourceFinderCore;
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
		public void DescribeTest()
		{
			var bld = new StringBuilder();
			CreateDefaultSample().Describe(bld);
			bld.ToString().Is(string.Empty);

		}

		[Fact()]
		public void EqualsTest()
		{
			Assert.True(false, "This test needs an implementation");
		}

		[Fact()]
		public void GetHashCodeTest()
		{
			Assert.True(false, "This test needs an implementation");
		}

		[Fact]
		public void OpoEqTest()
		{
#warning OpoEqTest_Is_NotImpl
			throw new NotImplementedException("OpoEqTest is not implemented");
		}

		[Fact]
		public void OpNotEqTest()
		{
#warning OpNotEqTest_Is_NotImpl
			throw new NotImplementedException("OpNotEqTest is not implemented");
		}

	}
}