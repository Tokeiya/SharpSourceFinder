using System;
using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore.DataControl;
using Xunit;
using static Xunit.Assert;

namespace SharpSourceFinderCoreTests.DataControl
{
	public class OrderControllerTest
	{
		private const string TraitName = "OrderController";


		[Trait("TestLayer", TraitName)]
		[Fact]
		public void Ctor()
		{
			Throws<ArgumentOutOfRangeException>(() => _ = new OrderController<string>(0));
			Throws<ArgumentOutOfRangeException>(() => _ = new OrderController<string>(-1));
			Throws<ArgumentOutOfRangeException>(() => _ = new OrderController<string>(1));
		}

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void SizeGet()
		{
			var actual = new OrderController<string>(42);
			actual.Size.Is(42);
		}

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void Register()

		{
			var target = new OrderController<string>(2);
			var actual = target.Register("hello");

			actual.removed.IsNull();
			actual.added.AheadElement.IsNull();
			actual.added.BehindElement.IsNotNull();

			actual = target.Register("world");
			actual.removed.IsNull();
			actual.added.AheadElement.IsNull();
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
			actual.added.BehindElement.Value.Is("hello");

			actual = target.Register("c#");
			actual.removed.Is("hello");
			actual.added.AheadElement.IsNull();
			actual.added.BehindElement.Value.Is("world");
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

			target = new OrderController<string>(4);
			var samples = Enumerable.Range(0, 4)
				.Select(i => target.Register((3 - i).ToString()).added).Reverse().ToArray();

			for (int i = 0; i < samples.Length; i++)
			{
				samples[i].Value.Is(i.ToString());

				if (i < samples.Length - 1) samples[i].BehindElement.IsSameReferenceAs(samples[i + 1]);
				else samples[i].BehindElement.IsNull();


				if (i > 0) samples[i].AheadElement.IsSameReferenceAs(samples[i - 1]);
				else samples[i].AheadElement.IsNull();
			}
		}

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void GotoAhead()
		{
			var target = new OrderController<string>(4);
			var samples = Enumerable.Range(0, 4)
				.Select(i => target.Register((3 - i).ToString()).added).Reverse().ToArray();


			void verify(int idx, int ahead, int behind)
			{
				samples[idx].Value.Is(idx.ToString());

				if (ahead == -1) samples[idx].AheadElement.IsNull();
				else samples[idx].AheadElement.IsSameReferenceAs(samples[ahead]);

				if (behind == -1) samples[idx].BehindElement.IsNull();
				else samples[idx].BehindElement.IsSameReferenceAs(samples[behind]);
			}


			//h-0-1-2-3-t
			target.GotoAhead(samples[1]);
			//h-1-0-2-3-t


			verify(1, -1, 0);
			verify(0, 1, 2);
			verify(2, 0, 3);
			verify(3, 2, -1);

			target.GotoAhead(samples[1]);

			verify(1, -1, 0);
			verify(0, 1, 2);
			verify(2, 0, 3);
			verify(3, 2, -1);


			target = new OrderController<string>(2);

			//h-0-1-t
			samples = Enumerable.Range(0, 2)
				.Select(i => target.Register((1 - i).ToString()).added).Reverse().ToArray();

			target.GotoAhead(samples[1]);
			//h-1-0-t

			verify(1, -1, 0);
			verify(0, 1, -1);

			target.GotoAhead(samples[1]);
			verify(1, -1, 0);
			verify(0, 1, -1);
		}

		[Trait("TestLayer", TraitName)]
		[Fact]
		public void BubbleUp()
		{
			var target = new OrderController<string>(4);
			var samples = Enumerable.Range(0, 4)
				.Select(i => target.Register((3 - i).ToString()).added).Reverse().ToArray();

			void verify(int idx, int ahead, int behind)
			{
				var actual = samples[idx];
				actual.Value.Is(idx.ToString());

				if (ahead == -1) actual.AheadElement.IsNull();
				else actual.AheadElement.IsSameReferenceAs(samples[ahead]);

				if (behind == -1) actual.BehindElement.IsNull();
				else actual.BehindElement.IsSameReferenceAs(samples[behind]);
			}

			//h-0-1-2-3-t
			target.BubbleUp(samples[2]);
			//h-0-2-1-3-h

			verify(0, -1, 2);
			verify(2, 0, 1);
			verify(1, 2, 3);
			verify(3, 1, -1);

			target.BubbleUp(samples[0]);

			verify(0, -1, 2);
			verify(2, 0, 1);
			verify(1, 2, 3);
			verify(3, 1, -1);


			target = new OrderController<string>(2);
			samples = Enumerable.Range(0, 2)
				.Select(i => target.Register((1 - i).ToString()).added).Reverse().ToArray();

			target.BubbleUp(samples[1]);

			verify(1, -1, 0);
			verify(0, 1, -1);

			target.BubbleUp(samples[1]);

			verify(1, -1, 0);
			verify(0, 1, -1);
		}
	}
}