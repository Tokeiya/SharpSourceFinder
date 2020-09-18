using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public enum IdentityCategories
	{
		Namespace=1,
		Class,
		Struct,
		Interface,
		Enum,
		Delegate
	}
	public interface IIdentity
	{
		IdentityCategories IdentityCategory { get; }
		string Identity { get; }
	}

	public sealed class IdentityName : TerminalElement,IIdentity
	{
		internal IdentityName(IDiscriminatedElement parent, IdentityCategories identityCategory,string identity) : base(parent)
		{
			if (string.IsNullOrWhiteSpace(identity))
				throw new ArgumentException($"{nameof(identity)} isn't accept empty or whitespace.");

			if(!EnumChecker<IdentityCategories>.Verify(identityCategory)) throw new ArgumentOutOfRangeException($"{nameof(identityCategory)} is unexpected value.");

			Identity = identity;
			IdentityCategory = identityCategory;

		}


		public string Identity { get; }

		public IdentityCategories IdentityCategory { get; }

		public override void Describe(StringBuilder stringBuilder, string indent, int depth) =>
			AppendIndent(stringBuilder, indent, depth).Append(Identity);

		public override void AggregateIdentities(Stack<(IdentityCategories, string)> accumulator) => accumulator.Push((IdentityCategory, Identity));

	}
}