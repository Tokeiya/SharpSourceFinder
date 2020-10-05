using System;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using FastEnumUtility;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class TypeElement : NonTerminalElement<IDiscriminatedElement>
	{
		private IQualified? _identity;


		protected TypeElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial,
			bool isStatic) : base(parent)
		{
			if(!FastEnum.IsDefined(scope)) throw new ArgumentOutOfRangeException(nameof(scope));

			Scope = scope;
			IsUnsafe = isUnsafe;
			IsPartial = isPartial;
			IsStatic = isStatic;
		}

		public override void RegisterChild(IDiscriminatedElement child)
		{
			if (child is IQualified id)
			{
				if (!(_identity is null)) throw new IdentityDuplicatedException();
				_identity = id;
			}

			base.RegisterChild(child);
		}

		public IQualified Identity =>
			_identity ?? throw new IdentityNotFoundException();

		public bool IsUnsafe { get; }

		public bool IsPartial { get; }

		public bool IsStatic { get; }

		public ScopeCategories Scope { get; }

		public override IPhysicalStorage Storage => Parent.Storage;

		protected bool IsBasementEquivalentTo(TypeElement other)
		{
			if(other._identity is null || _identity is null) throw new IdentityNotFoundException();

			return (
				GetQualifiedName().IsEquivalentTo(other.GetQualifiedName()) &&
				IsUnsafe == other.IsUnsafe &&
				IsPartial == other.IsPartial &&
				IsStatic == other.IsStatic &&
				Scope == other.Scope
			);

		}
	}
}