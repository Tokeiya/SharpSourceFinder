using System;

namespace Tokeiya3.SharpSourceFinderCore.DataControl
{
	internal class OrderControlElement<T>
	{
		public OrderControlElement<T>? AheadElement { get; private set; }
		public OrderControlElement<T>? BehindElement { get; private set; }

		public void MoveToAhead(OrderControlElement<T> pivot)
		{
			if (pivot.AheadElement is null&&pivot.BehindElement is null)
			{
				pivot.AheadElement = this;
				pivot.BehindElement = BehindElement;
				BehindElement = pivot;
			}
			else
			{
				var tmp = pivot.AheadElement;
				pivot.AheadElement = this;
				BehindElement = pivot;

				AheadElement = tmp;
				if (!(tmp is null)) tmp.BehindElement = this;
			}
		}

		public void MoveToBehind(OrderControlElement<T> pivot)
		{
			if (pivot.AheadElement is null && pivot.BehindElement is null)
			{
				pivot.BehindElement = this;
				pivot.AheadElement = AheadElement;
				AheadElement = pivot;
			}
			else
			{
				var tmp = pivot.BehindElement;
				pivot.BehindElement = this;
				AheadElement = pivot;

				BehindElement = tmp;
				if (!(tmp is null)) tmp.AheadElement = this;
			}
		}

		public T Value { get; set; }
	}
}