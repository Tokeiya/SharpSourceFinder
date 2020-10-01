using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class QualifiedElement : NonTerminalElement<IdentityElement>, IQualified
	{
		public QualifiedElement()
		{
		}

		public QualifiedElement(IDiscriminatedElement parent) : base(parent)
		{
		}

		public IReadOnlyList<IIdentity> Identities => TypedChildren;

		public bool IsEquivalentTo(IQualified other)
		{
			if (ReferenceEquals(this, other)) return true;
			if (Identities.Count != other.Identities.Count) return false;

			for (int i = 0; i < Identities.Count; i++)
			{
				if (!Identities[i].IsEquivalentTo(other.Identities[i])) return false;
			}

			return true;
		}

		public override QualifiedElement GetQualifiedName()
		{
			var accum = StackPool.Get();

			try
			{
				foreach (var element in AncestorsAndSelf())
				{
					element.AggregateIdentities(accum);
				}

				var ret = new QualifiedElement();

				while (accum.Count != 0)
				{
					var (cat, nme) = accum.Pop();
					_ = new IdentityElement(ret, cat, nme);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(accum.Count!=0);
				StackPool.Return(accum);
			}
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
			foreach (var elem in TypedChildren.Reverse())
			{
				accumulator.Push((elem.Category, elem.Name));
			}

		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
			if (ReferenceEquals(other, this)) return true;

			return other switch
			{
				QualifiedElement elem => IsEquivalentTo(elem) && Parent.IsLogicallyEquivalentTo(other.Parent),
				_ => false
			};
		}

		//Must coordinate the IdentityElement's Order property.
		public override void RegisterChild(IDiscriminatedElement child)
		{
			base.RegisterChild(child);
			((IdentityElement) child).Order = TypedChildren.Count;
		}
	}
}