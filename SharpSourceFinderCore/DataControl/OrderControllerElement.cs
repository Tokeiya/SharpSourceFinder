using System;

namespace Tokeiya3.SharpSourceFinderCore.DataControl
{
	internal class OrderControlElement<T>
	{
		public OrderControlElement<T>? AheadElement { get; private set; }
		public OrderControlElement<T>? BehindElement { get; private set; }

		public T Value { get; set; }


		public void AddToAhead(OrderControlElement<T> pivot)
		{
			pivot.AheadElement = this;
			pivot.BehindElement = BehindElement;
			BehindElement = pivot;
		}

		public void MoveToAhead(OrderControlElement<T> pivot)
		{
			var tmpAhead = pivot.AheadElement;
			var tmpBehind = BehindElement;

			pivot.AheadElement = this;
			pivot.BehindElement = BehindElement;

			BehindElement = pivot;
			AheadElement = tmpAhead;

			if (!(tmpAhead is null)) tmpAhead.BehindElement = this;
			if (!(tmpBehind is null)) tmpBehind.AheadElement = pivot;


		}

		public void AddToBehind(OrderControlElement<T> pivot)
		{
			pivot.BehindElement = this;
			pivot.AheadElement = AheadElement;
			AheadElement = pivot;
		}

		public void MoveToBehind(OrderControlElement<T> pivot)
		{
			var tmpAhead = AheadElement;
			var tmpBehind = pivot.BehindElement;

			pivot.AheadElement = AheadElement;
			pivot.BehindElement = this;

			AheadElement = pivot;
			BehindElement = tmpBehind;

			if (!(tmpAhead is null)) tmpAhead.BehindElement = pivot;
			if (!(tmpBehind is null)) tmpBehind.AheadElement = this;
		}
	}
}