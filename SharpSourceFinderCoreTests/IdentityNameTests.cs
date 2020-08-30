using Tokeiya3.SharpSourceFinderCore;
using ChainingAssertion;
using System;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class IdentityNameTests
	{
		const string SamplePath = @"G:\Hoge\Moge.cs";
		const string SampleNameSpace = "Tokeiya3";


		static NameSpace CreateStandardSample(string path = SamplePath)
		{
			var file = new SourceFile(path);
			return new NameSpace(file);
		}
		[Fact]
		public void DescribeTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new QualifiedName(root);

			var actual = new IdentityName(names, "System");
			var bld = new StringBuilder();

			actual.Describe(bld);
			bld.ToString().Is("System");
		}

		[Fact]
		public void InvalidNameTest()
		{
			var root = new SourceFile("G:\\Hoge\\Piyo.cs");
			var names = new QualifiedName(root);

			Assert.Throws<ArgumentException>(() => names.Add(""));
			Assert.Throws<ArgumentException>(() => names.Add("  "));
			Assert.Throws<ArgumentException>(() => names.Add("\n"));


		}

		[Fact()]
		public void EqualsTest()
		{
			var expectedName = CreateStandardSample();
			
			var pivot = new IdentityName(expectedName, "Tokeiya3");


			Assert.True(false, "This test needs an implementation");
		}

		[Fact()]
		public void GetHashCodeTest()
		{
			Assert.True(false, "This test needs an implementation");
		}

		[Fact]
		public void OpEqTest()
		{
#warning OpEqTest_Is_NotImpl
			throw new NotImplementedException("OpEqTest is not implemented");
		}

		[Fact]
		public void OpNotEqTest()
		{
			
		}

	}
}