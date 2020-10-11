using System;
using FastEnumUtility;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class TypeElement : NonTerminalElement<IDiscriminatedElement>
	{
		private IQualified? _identity;


		protected TypeElement(IDiscriminatedElement parent, ScopeCategories scope, bool isAbstract, bool isSealed,
			bool isUnsafe, bool isPartial,
			bool isStatic) : base(parent)
		{
			if (!FastEnum.IsDefined(scope)) throw new ArgumentOutOfRangeException(nameof(scope));

			if (isAbstract && (isStatic || isSealed))
				throw new ArgumentException(
					$"If {nameof(isAbstract)} is true then {nameof(isStatic)} or {nameof(isSealed)} can't true.");
			if (isStatic && isSealed)
				throw new ArgumentException($"{nameof(isStatic)} and {nameof(isSealed)} status are conflicted.");

			IsAbstract = isAbstract;
			IsSealed = isSealed;
			Scope = scope;
			IsUnsafe = isUnsafe;
			IsPartial = isPartial;
			IsStatic = isStatic;
		}

		public IQualified Identity =>
			_identity ?? throw new IdentityNotFoundException();

		public bool IsUnsafe { get; }

		public bool IsPartial { get; }

		public bool IsStatic { get; }

		public bool IsAbstract { get; }

		public bool IsSealed { get; }
		public ScopeCategories Scope { get; }

		public override IPhysicalStorage Storage => Parent.Storage;

		public override void RegisterChild(IDiscriminatedElement child)
		{
			if (child is NameSpace) throw new ArgumentException($"{nameof(child)} can't accept NameSpaceElement");

			if (child is IQualified id)
			{
				if (!(_identity is null)) throw new IdentityDuplicatedException();
				_identity = id;
			}

			base.RegisterChild(child);
		}

		protected bool IsBasementEquivalentTo(TypeElement other)
		{
			if (other._identity is null || _identity is null) throw new IdentityNotFoundException();

			return (
				GetQualifiedName().IsEquivalentTo(other.GetQualifiedName()) &&
				IsAbstract == other.IsAbstract &&
				IsSealed == other.IsSealed &&
				IsUnsafe == other.IsUnsafe &&
				IsPartial == other.IsPartial &&
				IsStatic == other.IsStatic &&
				Scope == other.Scope
			);
		}
	}
}