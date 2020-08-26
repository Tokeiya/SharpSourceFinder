using System.Linq;
using System.Text;
using ChainingAssertion;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class NamesElementsTests
	{
		[Fact]
		public void AddTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new NamesElements(root, "identity");

			names.Add("System");
			names.Add("Collections");
			names.Add("Generics");


			var actual = names.Descendants().ToArray();
			actual.Length.Is(3);

			actual[0].Identity.Is("System");
			actual[1].Identity.Is("Collections");
			actual[2].Identity.Is("Generics");
		}

		[Fact]
		public void DescribeTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new NamesElements(root, "identity");

			names.Add("System");
			names.Add("Collections");
			names.Add("Generics");

			var bld = new StringBuilder();
			names.Describe(bld);
			bld.ToString().Is("System.Collections.Generics");
			names.Describe().Is("System.Collections.Generics");
		}
	}
}