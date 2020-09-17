//using System.Linq;
//using System.Text;
//using ChainingAssertion;
//using Tokeiya3.SharpSourceFinderCore;
//using Xunit;

//namespace SharpSourceFinderCoreTests.Old
//{
//	public class TerminalElementTests
//	{
//		[Fact]
//		public void ChildrenTest()
//		{
//			var root = new MultiDescendantsElementTests.TestSample("root");

//			var actual = new TestSample(root, "ident");
//			actual.Children().Any().IsFalse();
//		}

//		[Fact]
//		public void DescendantsTest()
//		{
//			var root = new MultiDescendantsElementTests.TestSample("root");

//			var actual = new TestSample(root, "ident");
//			actual.Descendants().Any().IsFalse();

//			actual.DescendantsAndSelf().Count().Is(1);
//			actual.DescendantsAndSelf().First().Is(actual);
//		}

//		private class TestSample : TerminalElement
//		{
//			public TestSample(IDiscriminatedElement parent, string identity) : base(parent) => Identity = identity;

//			private string Identity { get; }

//			public override void RegisterChild(IDiscriminatedElement child)
//			{
//				throw new System.NotImplementedException();
//			}

//			public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Identity);
//		}
//	}
//}

