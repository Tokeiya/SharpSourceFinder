using ChainingAssertion;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class MultiDescendantsElementTests
	{
		(TestSample root, IReadOnlyList<TestSample> elements) Create()
		{
			var list = new List<TestSample>();

			var ret = new TestSample("root");
			list.Add(ret);

			list.Add(ret.Add("A"));
			list.Add(ret.Add("B"));

			var piv = list[1];
			list.Add(piv.Add("AA"));
			list.Add(piv.Add("AB"));

			piv = list[2];
			list.Add(piv.Add("BA"));
			list.Add(piv.Add("BB"));

			return (ret, list);
		}

		[Fact]
		public void DescendantsTest()
		{
			var actual = Create();
			actual.root.Descendants().Count().Is(actual.elements.Count - 1);
			var set = new HashSet<IDiscriminatedElement>();

			foreach (var elem in actual.root.Descendants())
			{
				actual.elements.Skip(1).Contains(elem).IsTrue();
				set.Add(elem).IsTrue();
			}

			actual.root.DescendantsAndSelf().Count().Is(actual.elements.Count);
			set.Clear();

			foreach (var elem in actual.root.DescendantsAndSelf())
			{
				actual.elements.Contains(elem).IsTrue();
				set.Add(elem).IsTrue();
			}
		}

		[Fact]
		public void ChildrenTest()
		{
			var (actual, expected) = Create();

			actual.Children().Count().Is(2);

			var idx = 1;

			foreach (var elem in actual.Children()) elem.Is(expected[idx++]);
		}

		[Fact]
		public void ChildElementsTest()
		{
			var (actual, expected) = Create();
			actual.Elements.Count.Is(2);

			actual.Elements[0].Is(expected[1]);
			actual.Elements[1].Is(expected[2]);
		}

		internal class TestSample : MultiDescendantsElement<IDiscriminatedElement>
		{
			public TestSample(string identity) => Identity = identity;
			public TestSample(IDiscriminatedElement parent, string identity) : base(parent) => Identity = identity;

			public IReadOnlyList<IDiscriminatedElement> Elements => ChildElements;

			public string Identity { get; }

			public TestSample Add(string identity)
			{
				var ret = new TestSample(this, identity);
				return ret;
			}

			public override void Describe(StringBuilder stringBuilder)
			{
			}

			public override string ToString() => Identity;
		}
	}
}