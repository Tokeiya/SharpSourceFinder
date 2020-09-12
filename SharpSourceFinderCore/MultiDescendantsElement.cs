using System;
using System.Collections.Generic;
using System.Linq;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class MultiDescendantsElement<T> : DiscriminatedElement
		where T : IDiscriminatedElement
	{
		private readonly List<T> _children = new List<T>();

		protected MultiDescendantsElement()
		{
		}

		protected MultiDescendantsElement(IDiscriminatedElement parent) : base(parent)
		{
		}


		protected IReadOnlyList<T> ChildElements => _children;

		public override void RegisterChild(IDiscriminatedElement child)
		{
			if (child is T t) _children.Add(t);
			else throw new ArgumentException($"{nameof(child)} can't accept.");
		}

		protected void Add(T value) => _children.Add(value);

		public override IEnumerable<IDiscriminatedElement> Descendants()
		{
			foreach (var elem in _children)
			{
				yield return elem;
				foreach (var e in elem.Descendants()) yield return e;
			}
		}

		public override IEnumerable<IDiscriminatedElement> Children() => _children.Cast<IDiscriminatedElement>();
	}
}