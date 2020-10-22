using System.Linq;
using System.Reflection;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore.DataControl;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DataControl
{

	public class OrderControlElementTest
	{
		private const string TraitName = "OrderControlElement";

		private readonly ITestOutputHelper _output;

		public OrderControlElementTest(ITestOutputHelper output) => _output = output;

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void InitialState()
		{
			var actual = new OrderControlElement<string>();
			actual.Value.Is(default(string));
			actual.AheadElement.IsNull();
			actual.BehindElement.IsNull();
		}

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void ValueTes()
		{
			{
				var actual =new  OrderControlElement<string?>();
				actual.Value.IsNull();

				actual.Value = "hello world";
				actual.Value.Is("hello world");

				actual.Value = "tokeiya3";
				actual.Value.Is("tokeiya3");

				actual.Value = default;
				actual.Value.IsNull();
			}

			{
				var actual = new OrderControlElement<int>();
				actual.Value.Is(default(int));

				actual.Value = 42;
				actual.Value.Is(42);

				actual.Value = 114514;
				actual.Value.Is(114514);
			}
		}


		[Trait("TestLayer", TraitName)]
		[Fact]
		public void MoveToAheadTest()
		{
			var actual = new OrderControlElement<int> { Value = 42 };
			var pivot = new OrderControlElement<int> { Value = 114514 };

			actual.MoveToAhead(pivot);
			pivot.AheadElement.IsSameReferenceAs(actual);
			pivot.BehindElement.IsNull();

			actual.BehindElement.IsSameReferenceAs(pivot);
			actual.AheadElement.IsNull();

			var ary = Enumerable.Range(1, 4).Select(i => new OrderControlElement<int> { Value = i }).ToArray();

			for (int i = 0; i < ary.Length-1; i++)
			{
				ary[i].MoveToAhead(ary[i+1]);
			}


			for (int i = 1; i < ary.Length-1; i++)
			{
				ary[i].AheadElement.Is(ary[i - 1]);
				ary[i].BehindElement.Is(ary[i + 1]);
			}

			ary[0].AheadElement.IsNull();
			ary[0].BehindElement.IsSameReferenceAs(ary[1]);

			ary[3].AheadElement.IsSameReferenceAs(ary[2]);
			ary[3].BehindElement.IsNull();


		}

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void MoveToBehindTest()
		{
			var actual = new OrderControlElement<int> {Value = 42};
			var pivot = new OrderControlElement<int> {Value = 114514};

			actual.MoveToBehind(pivot);

			pivot.BehindElement.IsSameReferenceAs(actual);
			pivot.AheadElement.IsNull();

			actual.BehindElement.IsNull();
			actual.AheadElement.IsSameReferenceAs(pivot);

			var ary = Enumerable.Range(1, 4).Select(i => new OrderControlElement<int> {Value = i}).ToArray();

			for (int i = 0; i < ary.Length - 1; i++)
			{
				ary[i].MoveToBehind(ary[i + 1]);
			}

			for (int i = 1; i < ary.Length-1; i++)
			{
				ary[i].AheadElement.IsSameReferenceAs(ary[i + 1]);
				ary[i].BehindElement.IsSameReferenceAs(ary[i - 1]);
			}

			ary[0].AheadElement.IsSameReferenceAs(ary[1]);
			ary[0].BehindElement.IsNull();

			ary[3].AheadElement.IsNull();
			ary[3].BehindElement.IsSameReferenceAs(ary[2]);
		}

	}
}
