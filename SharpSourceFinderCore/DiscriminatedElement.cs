﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class DiscriminatedElement : IDiscriminatedElement
	{
		protected DiscriminatedElement(string identity) => (Parent, Identity) = (Root, identity);

		protected DiscriminatedElement(IDiscriminatedElement parent, string identity)
		{
			if (parent is ImaginaryRoot) throw new ArgumentException("parent can't accept DiscriminatedElement.Root .");
			(Parent, Identity) = (parent, identity);
		}

		public static IDiscriminatedElement Root { get; } = new ImaginaryRoot();

		protected static ObjectPool<StringBuilder> StringBuilderPool { get; } =
			new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());

		public string Identity { get; }
		public IDiscriminatedElement Parent { get; }
		public abstract void Describe(StringBuilder stringBuilder);

		public virtual string Describe()
		{
			var bld = StringBuilderPool.Get();

			try
			{
				Describe(bld);
				return bld.ToString();
			}
			finally
			{
				StringBuilderPool.Return(bld);
			}
		}

		public IEnumerable<IDiscriminatedElement> Ancestors()
		{
			if (Parent is ImaginaryRoot) yield break;

			for (var piv = Parent; !(piv is ImaginaryRoot); piv = piv.Parent) yield return piv;
		}

		public IEnumerable<IDiscriminatedElement> AncestorsAndSelf()
		{
			yield return this;

			foreach (var elem in Ancestors()) yield return elem;
		}

		public abstract IEnumerable<IDiscriminatedElement> Children();

		public abstract IEnumerable<IDiscriminatedElement> Descendants();

		public IEnumerable<IDiscriminatedElement> DescendantsAndSelf()
		{
			yield return this;

			foreach (var elem in Descendants()) yield return elem;
		}
	}
}