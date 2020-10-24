using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FastEnumUtility;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityElement : TerminalElement, IIdentity
	{
		public IdentityElement(QualifiedElement from, IdentityCategories category, string name) : base(from)
		{
			if (!category.IsDefined()) throw new ArgumentException($"{nameof(category)} is unexpected value");
			Category = category;
			Name = name;
		}

		public IdentityElement(QualifiedElement parent, string name) : base(parent)
		{
			var piv = parent.Parent;
			IdentityCategories? cat = default;

			while (!(piv is ImaginaryRoot))
			{
				cat = piv switch
				{
					NameSpaceElement _ => IdentityCategories.Namespace,
					ClassElement _ => IdentityCategories.Class,
					StructElement _ => IdentityCategories.Struct,
					InterfaceElement _ => IdentityCategories.Interface,
					EnumElement _ => IdentityCategories.Enum,
					DelegateElement _ => IdentityCategories.Delegate,
					_ => null
				};

				piv = piv.Parent;

				if (cat is null) continue;
				break;
			}

			Name = name;
			Category = cat ?? throw new CategoryNotFoundException(name);
		}

		public int Order { get; internal set; }

		public IdentityCategories Category { get; }
		public string Name { get; }
		public IQualified From => (IQualified) Parent;

		public bool IsEquivalentTo(IIdentity identity) =>
			(Name == identity.Name && Category == identity.Category && Order == identity.Order);

		public override QualifiedElement GetQualifiedName()
		{
			var accum = StackPool.Get();

			try
			{
				foreach (var identity in From.Identities.Take(Order).Cast<IdentityElement>().Reverse())
					identity.AggregateIdentities(accum);

				var ret = new QualifiedElement();

				while (accum.Count != 0)
				{
					var (category, name) = accum.Pop();
					_ = new IdentityElement(ret, category, name);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(accum.Count == 0);
				StackPool.Return(accum);
			}
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator) =>
			accumulator.Push((Category, Name));

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
			if (ReferenceEquals(this, other)) return true;

			return other switch
			{
				IdentityElement elem => IsEquivalentTo(elem) &&
				                        ((QualifiedElement) Parent).IsLogicallyEquivalentTo(
					                        ((QualifiedElement) other.Parent), Order),
				_ => false
			};
		}

		public override string ToString() => Name;
	}
}