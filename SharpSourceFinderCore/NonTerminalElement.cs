using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class NonTerminalElement<T> : DiscriminatedElement where T : IDiscriminatedElement
	{
		private readonly List<T> _children = new List<T>();

		protected NonTerminalElement()
		{
		}

		protected NonTerminalElement(IDiscriminatedElement parent) : base(parent)
		{
		}

		public IReadOnlyList<T> TypedChildren => _children;

		public override IEnumerable<IDiscriminatedElement> Children() => (IEnumerable<IDiscriminatedElement>)_children;

		public override void RegisterChild(IDiscriminatedElement child)
		{
			if (!(child is T))
				throw new ArgumentException($"{nameof(child)} must be {typeof(T).Name}");

			if (!ReferenceEquals(this, child.Parent))
				throw new ArgumentException($"{nameof(child)}'s parent is another.");

			_children.Add((T)child);
		}
	}
}