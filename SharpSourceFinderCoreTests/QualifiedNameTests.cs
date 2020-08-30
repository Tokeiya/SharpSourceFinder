using System;
using Tokeiya3.SharpSourceFinderCore;
using ChainingAssertion;
using System.Linq;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class QualifiedNameTests
	{
		[Fact]
		public void AddTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new QualifiedName(root);

			names.Add("System");
			names.Add("Collections");
			names.Add("Generics");


			var actual = names.Descendants().Cast<IdentityName>().ToArray();
			actual.Length.Is(3);

			actual[0].Identity.Is("System");
			actual[1].Identity.Is("Collections");
			actual[2].Identity.Is("Generics");
		}

		[Fact]
		public void DescribeTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new QualifiedName(root);

			names.Add("System");
			names.Add("Collections");
			names.Add("Generics");

			var bld = new StringBuilder();
			names.Describe(bld);
			bld.ToString().Is("System.Collections.Generics");
			names.Describe().Is("System.Collections.Generics");
		}

		[Fact()]
		public void GetHashCodeTest()
		{
			Assert.True(false, "This test needs an implementation");
		}

		[Fact()]
		public void EqualsTest()
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
#warning OpNotEqTest_Is_NotImpl
			throw new NotImplementedException("OpNotEqTest is not implemented");
		}


	}
}