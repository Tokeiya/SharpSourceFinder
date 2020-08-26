using System.Collections.Generic;
using System.Linq;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class MultiDescendantsElement<T> : DiscriminatedElement
		where T : IDiscriminatedElement
	{
		private readonly List<T> _children = new List<T>();

		protected MultiDescendantsElement(string identity) : base(identity)
		{
		}

		protected MultiDescendantsElement(IDiscriminatedElement parent, string identity) : base(parent, identity)
		{
		}

		protected IReadOnlyList<T> ChildElements => _children;

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