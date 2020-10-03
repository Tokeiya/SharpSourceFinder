using System;
using System.Collections.Generic;
using Microsoft.Extensions.ObjectPool;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class DiscriminatedElement : IDiscriminatedElement
	{
		protected DiscriminatedElement(IDiscriminatedElement parent)
		{
			if (parent is ImaginaryRoot) throw new ArgumentException($"{nameof(parent)} can't accept ImaginaryRoot.");
			Parent = parent;
			parent.RegisterChild(this);
		}

		protected DiscriminatedElement() => Parent = ImaginaryRoot.Root;

		protected static ObjectPool<Stack<(IdentityCategories category, string name)>> StackPool { get; }
			= new DefaultObjectPool<Stack<(IdentityCategories category, string name)>>(
				new DefaultPooledObjectPolicy<Stack<(IdentityCategories category, string name)>>());

		public IDiscriminatedElement Parent { get; }

		public virtual IPhysicalStorage Storage => Parent.Storage;


		public abstract void RegisterChild(IDiscriminatedElement child);

		public IEnumerable<IDiscriminatedElement> Ancestors()
		{
			var piv = Parent;

			while (!(piv is ImaginaryRoot))
			{
				yield return piv;
				piv = piv.Parent;
			}
		}

		public IEnumerable<IDiscriminatedElement> AncestorsAndSelf()
		{
			yield return this;

			foreach (var elem in Ancestors()) yield return elem;
		}

		public abstract IEnumerable<IDiscriminatedElement> Children();

		public IEnumerable<IDiscriminatedElement> Descendants()
		{
			foreach (var elem in Children())
			{
				yield return elem;

				foreach (var ret in elem.Children())
					yield return ret;
			}
		}

		public IEnumerable<IDiscriminatedElement> DescendantsAndSelf()
		{
			yield return this;

			foreach (var elem in Descendants()) yield return elem;
		}

		public abstract QualifiedElement GetQualifiedName();

		public abstract void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator);


		public abstract bool IsLogicallyEquivalentTo(IDiscriminatedElement other);

		public virtual bool IsPhysicallyEquivalentTo(IDiscriminatedElement other) =>
			IsLogicallyEquivalentTo(other) && Storage.IsEquivalentTo(other.Storage);
	}
}