using Tokeiya3.SharpSourceFinderCore;
using ChainingAssertion;
using System;
using System.Linq;
using System.Text;
using Xunit;
using static SharpSourceFinderCoreTests.EquivalentTestHelper<Tokeiya3.SharpSourceFinderCore.IdentityName>;


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
			var root = CreateStandardSample();
			var name = new IdentityName(root, SampleNameSpace);

			name.Describe().Is(SampleNameSpace);

		}

		[Fact]
		public void InvalidNameTest()
		{
			var root = CreateStandardSample();

			Assert.Throws<ArgumentException>(() => new IdentityName(root,""));
			Assert.Throws<ArgumentException>(() => new IdentityName(root," "));
			Assert.Throws<ArgumentException>(() => new IdentityName(root, "\t"));
		}

		[Fact()]
		public void EqualsTest()
		{
			var ns = CreateStandardSample();
			var piv = new IdentityName(ns, "Tokeiya3");

#warning EqualsTest_Is_NotImpl
			throw new NotImplementedException("EqualsTest is not implemented");
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


	}
}