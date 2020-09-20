using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class DiscriminatedElement : IDiscriminatedElement
	{
		protected DiscriminatedElement() => Parent = Root;

		protected DiscriminatedElement(IDiscriminatedElement parent)
		{
			if (parent is ImaginaryRoot) throw new ArgumentException("parent can't accept DiscriminatedElement.Root .");

			Parent = parent;

			parent.RegisterChild(this);
		}

		public static IDiscriminatedElement Root { get; } = new ImaginaryRoot();

		protected static ObjectPool<StringBuilder> StringBuilderPool { get; } =
			new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());

		protected static ObjectPool<Stack<(IdentityCategories category,string identity)>> StackPool { get; }=
			new DefaultObjectPool<Stack<(IdentityCategories,string)>>(new DefaultPooledObjectPolicy<Stack<(IdentityCategories,string)>>());

		public IDiscriminatedElement Parent { get; }
		public abstract void RegisterChild(IDiscriminatedElement child);
		public abstract void Describe(StringBuilder stringBuilder, string indent, int depth);

		public virtual string Describe(string indent = "\t")
		{
			var bld = StringBuilderPool.Get();

			try
			{
				Describe(bld, indent, 0);
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

		public virtual IQualified GetQualifiedName()
		{
			var accum = StackPool.Get();

			try
			{
				var ret = new QualifiedName();
				foreach (var elem in AncestorsAndSelf())
				{
					elem.AggregateIdentities(accum);
				}

				while (accum.Count!=0)
				{
					var (cat,id) = accum.Pop();
					_ = new IdentityName(ret, cat, id);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(accum.Count == 0);
				accum.Clear();
				StackPool.Return(accum);

			}
		}

		public abstract void AggregateIdentities(Stack<(IdentityCategories category,string identity)> accumulator);


		protected static StringBuilder AppendIndent(StringBuilder stringBuilder, string indent, int depth)
		{
			if (depth < 0) throw new ArgumentOutOfRangeException($"{nameof(depth)}");

			for (int i = 0; i < depth; i++) stringBuilder.Append(indent);

			return stringBuilder;
		}


	}
}