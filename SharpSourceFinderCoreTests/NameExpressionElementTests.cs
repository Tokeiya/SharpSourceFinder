using System.Text;
using ChainingAssertion;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class NameExpressionElementTests
	{
		[Fact]
		public void DescribeTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new NamesElements(root, "names");

			var actual = new NameExpressionElement(names, "System");
			var bld = new StringBuilder();

			actual.Describe(bld);
			bld.ToString().Is("System");
		}
	}
}