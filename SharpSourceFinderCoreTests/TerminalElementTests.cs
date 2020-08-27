using ChainingAssertion;
using System.Linq;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class TerminalElementTests
	{
		[Fact]
		public void ChildrenTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");

			var actual = new TestSample(root, "ident");
			actual.Children().Any().IsFalse();
		}

		[Fact]
		public void DescendantsTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");

			var actual = new TestSample(root, "ident");
			actual.Descendants().Any().IsFalse();

			actual.DescendantsAndSelf().Count().Is(1);
			actual.DescendantsAndSelf().First().Is(actual);
		}

		private class TestSample : TerminalElement
		{
			public TestSample(IDiscriminatedElement parent, string identity) : base(parent, identity)
			{
			}

			public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Representation);
		}
	}
}