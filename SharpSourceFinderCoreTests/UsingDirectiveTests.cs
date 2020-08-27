using ChainingAssertion;
using System.Linq;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class UsingDirectiveTests
	{
		static (SourceFile root, UsingDirective element) CreateSample()
		{
			var root = new SourceFile("G:\\Hoge\\Sample.cs");
			var element = new UsingDirective(root, "System", "Linq");

			return (root, element);
		}

		[Fact]
		public void UsingDirectiveElementsTest()
		{
			var (root, element) = CreateSample();
			element.Parent.Is(root);
		}


		[Fact]
		public void DescribeTest()
		{
			var (_, actual) = CreateSample();

			var bld = new StringBuilder();
			actual.Describe(bld);
			bld.ToString().Is("using System.Linq;");

			actual.Describe().Is("using System.Linq;");
		}

		[Fact]
		public void ChildrenTest()
		{
			var (_, actual) = CreateSample();

			var array = actual.Children().ToArray();
			array.Length.Is(1);

			(array[0] is QualifiedName).IsTrue();

		}

		[Fact]
		public void DescendantsTest()
		{
			var (_, actual) = CreateSample();

			var array = actual.Descendants().ToArray();
			array.Length.Is(3);

			(array[0] is QualifiedName).IsTrue();

			(array[1] is IdentityName).IsTrue();
			array[1].Representation.Is("System");

			(array[2] is IdentityName).IsTrue();
			array[2].Representation.Is("Linq");

		}
	}
}