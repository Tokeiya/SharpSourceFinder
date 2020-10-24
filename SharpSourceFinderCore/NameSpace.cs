using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameSpaceElement : NonTerminalElement<IDiscriminatedElement>
	{
		private readonly IPhysicalStorage _storage;

		//ここに参照持たせる
		private IQualified? _identity;

		public NameSpaceElement() => _storage = StorageNotAvailable.NotAvailable;

		public NameSpaceElement(IPhysicalStorage physicalStorage) => _storage = physicalStorage;

		public NameSpaceElement(IDiscriminatedElement parent) : base(parent) => _storage = StorageNotAvailable.NotAvailable;

		public IQualified Identity => _identity ?? throw new IdentityNotFoundException();

		public override IPhysicalStorage Storage
		{
			get
			{
				if (Parent is ImaginaryRoot) return _storage;
				return Parent.Storage;
			}
		}


		public override void RegisterChild(IDiscriminatedElement child)
		{
			if (child is IQualified qualified)
			{
				if (!(_identity is null)) throw new IdentityDuplicatedException();
				_identity = qualified;
			}

			base.RegisterChild(child);
		}


		public override QualifiedElement GetQualifiedName()
		{
			var stack = StackPool.Get();

			try
			{
				foreach (var elem in AncestorsAndSelf()) elem.AggregateIdentities(stack);

				var ret = new QualifiedElement();

				while (stack.Count != 0)
				{
					var (category, name) = stack.Pop();
					_ = new IdentityElement(ret, category, name);
				}

				return ret;
			}
			finally
			{
				Debug.Assert(stack.Count == 0);
				StackPool.Return(stack);
			}
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
			foreach (var elem in Identity.Identities.Reverse()) accumulator.Push((elem.Category, elem.Name));
		}

		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
			var ret = other switch
			{
				NameSpaceElement ns => Identity.IsEquivalentTo(ns.Identity),
				_ => false
			};

			if (!ret) return false;

			return Parent.IsLogicallyEquivalentTo(other.Parent);
		}
	}
}