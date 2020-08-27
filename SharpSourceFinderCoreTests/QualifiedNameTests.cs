using System.Linq;
using System.Text;
using ChainingAssertion;
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


			var actual = names.Descendants().ToArray();
			actual.Length.Is(3);

			actual[0].Representation.Is("System");
			actual[1].Representation.Is("Collections");
			actual[2].Representation.Is("Generics");
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
	}
}