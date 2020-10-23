using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore.DataControl;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DataControl
{
	public class OrderControlElementTest
	{
		private const string TraitName = "OrderControlElement";

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
		public void Value()
		{
			{
				var actual = new OrderControlElement<string?>();
				actual.Value.IsNull();

				actual.Value = "hello world";
				actual.Value.Is("hello world");

				actual.Value = "tokeiya3";
				actual.Value.Is("tokeiya3");

				actual.Value = default;
				actual.Value.IsNull();
			}

			{
				var actual = new OrderControlElement<string>();
				actual.Value.IsNull();

				actual.Value = "42";
				actual.Value.Is("42");

				actual.Value = "114514";
				actual.Value.Is("114514");
			}
		}

		private static OrderControlElement<string>[] GenerateSample()
		{
			var ret = Enumerable.Range(0, 4).Select(i => new OrderControlElement<string> {Value = i.ToString()}).ToArray();

			var piv = ret[0];


			for (int i = 1; i < ret.Length; i++)
			{
				ret[i].AddToBehind(piv);
				piv = ret[i];
			}

			void verify(int ahead, int behind)
			{
				ret[ahead].BehindElement.IsSameReferenceAs(ret[behind]);
				ret[behind].AheadElement.IsSameReferenceAs(ret[ahead]);
			}

			ret[0].AheadElement.IsNull();
			verify(0, 1);
			verify(1, 2);
			verify(2, 3);
			ret[3].BehindElement.IsNull();
			


			return ret;
		}

		static void Verify<T>(OrderControlElement<T> ahead, OrderControlElement<T> behind) where T:class
		{
			ahead.BehindElement.IsSameReferenceAs(behind);
			behind.AheadElement.IsSameReferenceAs(ahead);
		}

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void MoveToAhead()
		{
			var samples = GenerateSample();

			samples[2].MoveToAhead(samples[1]);
			Verify(samples[2], samples[1]);
			Verify(samples[0], samples[2]);
			Verify(samples[1], samples[3]);

			samples = GenerateSample();
			samples[1].MoveToAhead(samples[0]);

			Verify(samples[1], samples[0]);
			samples[1].AheadElement.IsNull();

			Verify(samples[0], samples[2]);

			samples = GenerateSample();
			samples[3].MoveToAhead(samples[0]);

			samples[3].AheadElement.IsNull();
			Verify(samples[3],samples[0]);
			Verify(samples[0], samples[1]);

		}





		[Trait("TestLayer", TraitName)]
		[Fact]
		public void AddToBehind()
		{
			var actual = new OrderControlElement<string> {Value = "42"};
			var pivot = new OrderControlElement<string> {Value = "114514"};

			actual.AddToBehind(pivot);

			pivot.BehindElement.IsSameReferenceAs(actual);
			pivot.AheadElement.IsNull();

			actual.BehindElement.IsNull();
			actual.AheadElement.IsSameReferenceAs(pivot);

			var ary = Enumerable.Range(1, 4).Select(i => new OrderControlElement<string> {Value = i.ToString()}).ToArray();

			for (int i = 0; i < ary.Length - 1; i++) ary[i].AddToBehind(ary[i + 1]);

			for (int i = 1; i < ary.Length - 1; i++)
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