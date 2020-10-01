using System;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class TypeElement : NonTerminalElement<IDiscriminatedElement>
	{
		protected TypeElement(IDiscriminatedElement parent, ScopeCategories scope, bool isUnsafe, bool isPartial,
			bool isStatic) : base(parent)
		{
#warning TypeElement_Is_NotImpl
			throw new NotImplementedException("TypeElement is not implemented");
		}

		public IQualified Identity { get; }

		public bool IsUnsafe { get; }

		public bool IsPartial { get; }

		public bool IsStatic { get; }

		public ScopeCategories Scope { get; }

		public override IPhysicalStorage Storage
		{
			get
			{
#warning Storage_Is_NotImpl
				throw new NotImplementedException("Storage is not implemented");
			}
		}

		protected bool IsBasementEquivalentTo(TypeElement other)
		{
#warning IsBasementEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsBasementEquivalentTo is not implemented");
		}
	}
}