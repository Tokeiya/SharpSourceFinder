using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Sdk;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{

	public class DiscriminatedElementTests
	{

		class TestTarget : DiscriminatedElement
		{

			public TestTarget(string identity) : base(identity)
			{

			}

			public TestTarget(IDiscriminatedElement parent, string identity) : base(parent, identity)
			{
			}

			public bool DescendantsFlag { get; private set; }

			public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Identity);


			public override IEnumerable<IDiscriminatedElement> Children() => throw new NotSupportedException();


			public override IEnumerable<IDiscriminatedElement> Descendants()
			{
				DescendantsFlag.IsFalse();

				DescendantsFlag = true;

				yield break;
			}

		}

		private const string ExpectedIdentity = "ExpectedSample";
		private const string ExpectedParentIdentity = "ExpectedParent";

		
		private static readonly IDiscriminatedElement ExpectedParent = new TestTarget(ExpectedParentIdentity);

		private static IDiscriminatedElement CreateExpectedSample() => new TestTarget(ExpectedParent, ExpectedIdentity);



		[Fact]
		public void CtorTest()
		{
			var root = new TestTarget("root");
			var actual = new TestTarget(root, "actual");

			Assert.Throws<ArgumentException>(() => new TestTarget(DiscriminatedElement.Root, "hoge"));

		}

		[Fact()]
		public void DescribeTest()
		{
			CreateExpectedSample().Describe().Is(ExpectedIdentity);
		}


		[Fact()]
		public void AncestorsTest()
		{
			var element = CreateExpectedSample();

			element.Ancestors().Count().Is(1);
			element.Ancestors().Contains(ExpectedParent).IsTrue();
		}

		[Fact()]
		public void AncestorsAndSelfTest()
		{
			var element = CreateExpectedSample();

			element.AncestorsAndSelf().Count().Is(2);
			element.AncestorsAndSelf().Contains(element).IsTrue();
			element.AncestorsAndSelf().Contains(ExpectedParent).IsTrue();

		}

		[Fact()]
		public void ChildrenTest() =>
			Assert.Throws<NotSupportedException>(() => CreateExpectedSample().Children().Count());



		[Fact()]
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
	}
}