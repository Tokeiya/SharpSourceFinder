using ChainingAssertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class DiscriminatedElementTests
	{
		private const string ExpectedRepresentation = "ExpectedSample";
		private const string ExpectedParentRepresentation = "ExpectedParent";


		private static readonly IDiscriminatedElement ExpectedParent = new TestTarget(ExpectedParentRepresentation);

		private static IDiscriminatedElement CreateExpectedSample() => new TestTarget(ExpectedParent, ExpectedRepresentation);


		[Fact]
		public void CtorTest()
		{
			var root = new TestTarget("root");
			var actual = new TestTarget(root, "actual");

			Assert.Throws<ArgumentException>(() => new TestTarget(DiscriminatedElement.Root, "hoge"));
		}

		[Fact]
		public void DescribeTest()
		{
			CreateExpectedSample().Describe().Is(ExpectedRepresentation);
		}


		[Fact]
		public void AncestorsTest()
		{
			var element = CreateExpectedSample();

			element.Ancestors().Count().Is(1);
			element.Ancestors().Contains(ExpectedParent).IsTrue();
		}

		[Fact]
		public void AncestorsAndSelfTest()
		{
			var element = CreateExpectedSample();

			element.AncestorsAndSelf().Count().Is(2);
			element.AncestorsAndSelf().Contains(element).IsTrue();
			element.AncestorsAndSelf().Contains(ExpectedParent).IsTrue();
		}

		[Fact]
		public void ChildrenTest() =>
			Assert.Throws<NotSupportedException>(() => CreateExpectedSample().Children().Count());


		[Fact]
		public void DescendantsAndSelfTest()
		{
			var element = CreateExpectedSample() as TestTarget;
			element.IsNotNull();
			element!.DescendantsFlag.IsFalse();

			var expected = element.DescendantsAndSelf().ToArray();
			element.DescendantsFlag.IsTrue();

			expected.Length.Is(1);
			expected[0].Is(element);
		}

		class TestTarget : DiscriminatedElement
		{
			public TestTarget(string identity) => Identity = identity;

			public TestTarget(IDiscriminatedElement parent, string identity) : base(parent) => Identity = identity;


			public string Identity { get; }

			public bool DescendantsFlag { get; private set; }

			public override void RegisterChild(IDiscriminatedElement child)
			{
			}

			public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Identity);


			public override IEnumerable<IDiscriminatedElement> Children() => throw new NotSupportedException();


			public override IEnumerable<IDiscriminatedElement> Descendants()
			{
				DescendantsFlag.IsFalse();

				DescendantsFlag = true;

				yield break;
			}
		}
	}
}