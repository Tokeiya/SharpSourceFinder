using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FastEnumUtility;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityElement : TerminalElement, IIdentity
	{
		public IdentityElement(QualifiedElement from,ScopeCategories scope, IdentityCategories category, string name) : base(from)
		{
			if (!category.IsDefined()) throw new ArgumentException($"{nameof(category)}:{category} is unexpected value");
			if (!scope.IsDefined()) throw new ArgumentException($"{nameof(scope)}:{scope} is unexpected value.");
			Category = category;
			Name = name;
			Scope = scope;
		}

		public IdentityElement(QualifiedElement parent, string name) : base(parent)
		{
			var piv = parent.Parent;
			(ScopeCategories scope, IdentityCategories category)? attr = default;

			while (!(piv is ImaginaryRoot))
			{
				attr = piv switch
				{
					NameSpaceElement _ => (ScopeCategories.Public,IdentityCategories.Namespace),
					ClassElement cls => (cls.Scope,IdentityCategories.Class),
					StructElement str => (str.Scope,IdentityCategories.Struct),
					InterfaceElement iface => (iface.Scope,IdentityCategories.Interface),
					EnumElement e => (e.Scope,IdentityCategories.Enum),
					DelegateElement d => (d.Scope,IdentityCategories.Delegate),
					_ => null
				};

				piv = piv.Parent;

				if (attr is null) continue;
				break;
			}

			Name = name;
			Category = attr?.category ?? throw new CategoryNotFoundException(name);
			Scope = attr.Value.scope;
		}

		public int Order { get; internal set; }

		public IdentityCategories Category { get; }

		public ScopeCategories Scope { get; }
		public string Name { get; }
		public IQualified From => (IQualified) Parent;

		public bool IsEquivalentTo(IIdentity identity) =>
			(Name == identity.Name && Scope == identity.Scope && Category == identity.Category &&
			 Order == identity.Order);

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
					var (scope,category, name) = accum.Pop();
					_ = new IdentityElement(ret,scope, category, name);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(accum.Count == 0);
				StackPool.Return(accum);
			}
		}

		public override void AggregateIdentities(Stack<(ScopeCategories scope,IdentityCategories category, string identity)> accumulator) =>
			accumulator.Push((Scope,Category, Name));

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