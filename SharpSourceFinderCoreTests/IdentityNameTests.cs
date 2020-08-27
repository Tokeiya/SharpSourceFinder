using System.Text;
using ChainingAssertion;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class IdentityNameTests
	{
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
	}
}