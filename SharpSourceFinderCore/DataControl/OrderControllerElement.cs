namespace Tokeiya3.SharpSourceFinderCore.DataControl
{
	internal class OrderControlElement<T> where T : class
	{
		public OrderControlElement<T>? AheadElement { get; private set; }
		public OrderControlElement<T>? BehindElement { get; private set; }

		public T? Value { get; set; }


		public void MoveToAhead(OrderControlElement<T> pivot)
		{
			//Remove
			AheadElement!.BehindElement = BehindElement;
			if (!(BehindElement is null)) BehindElement.AheadElement = AheadElement;

			//Insert
			if (!(pivot.AheadElement is null)) pivot.AheadElement.BehindElement = this;
			AheadElement = pivot.AheadElement;
			BehindElement = pivot;
			pivot.AheadElement = this;
		}

		public void AddToBehind(OrderControlElement<T> pivot)
		{
			pivot.BehindElement = this;
			AheadElement = pivot;
		}
	}
}