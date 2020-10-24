using System;
using System.Linq;

namespace Tokeiya3.SharpSourceFinderCore.DataControl
{
	internal class OrderController<T> where T:class
	{
		private OrderControlElement<T> _head;
		private OrderControlElement<T> _tail;


		public OrderController(int size)
		{
			if (size <= 1) throw new ArgumentOutOfRangeException(nameof(size));
			Size = size;

			var ary = Enumerable.Range(0, size).Select(i => new OrderControlElement<T>()).ToArray();

			var head = ary[0];

			foreach (var elem in ary.Skip(1))
			{
				elem.AddToBehind(head);
				head = elem;
			}

			_head = ary[0];
			_tail = ary[^1];
		}

		public int Size { get; }

		public (OrderControlElement<T> added,T? removed) Register(T value)
		{
			var removed = _tail.Value;
			var tmp = _tail;
			tmp.Value = value;
			_tail = _tail.AheadElement!;

			tmp.MoveToAhead(_head);
			_head = tmp;
			return (_head, removed);
		}

		public void GotoAhead(OrderControlElement<T> element)
		{
			if (element.AheadElement is null) return;

			element.MoveToAhead(_head);
			_head = element;
		}

		public void BubbleUp(OrderControlElement<T> element)
		{
			if(element.AheadElement is null) return;

			element.MoveToAhead(element.AheadElement);
			if (element.AheadElement is null) _head = element;
		}
	}
}