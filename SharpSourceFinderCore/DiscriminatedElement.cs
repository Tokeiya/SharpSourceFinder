using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class DiscriminatedElement : IDiscriminatedElement
	{
		protected static ObjectPool<Stack<(IdentityCategories category, string name)>> StackPool { get; } 
			= new DefaultObjectPool<Stack<(IdentityCategories category, string name)>>(new DefaultPooledObjectPolicy<Stack<(IdentityCategories category, string name)>>());
		protected DiscriminatedElement(IDiscriminatedElement parent)
		{
			if (parent is ImaginaryRoot) throw new ArgumentException($"{nameof(parent)} can't accept ImaginaryRoot.");
			Parent = parent;
		}

		protected DiscriminatedElement() => Parent = ImaginaryRoot.Root;

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

			foreach (var elem in Ancestors())
			{
				yield return elem;
			}
		}

		public abstract IEnumerable<IDiscriminatedElement> Children();

		public IEnumerable<IDiscriminatedElement> Descendants()
		{
			foreach (var elem in Children())
				foreach (var ret in elem.Children())
					yield return ret;
		}

		public IEnumerable<IDiscriminatedElement> DescendantsAndSelf()
		{
			yield return this;

			foreach (var elem in Descendants())
			{
				yield return elem;
			}
		}

		public abstract QualifiedElement GetQualifiedName();

		public abstract void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator);

		public abstract bool IsEquivalentTo(IDiscriminatedElement other);

		public bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
#warning IsLogicallyEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsLogicallyEquivalentTo is not implemented");
		}

		public bool IsPhysicallyEquivalentTo(IDiscriminatedElement other)
		{
#warning IsPhysicallyEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsPhysicallyEquivalentTo is not implemented");
		}
	}
}