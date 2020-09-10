using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class UsingAliasDirective : UsingDirective
	{
		public UsingAliasDirective(IDiscriminatedElement parent, string alias, params string[] names) : base(parent, names)
		{
			if (string.IsNullOrWhiteSpace(alias)) throw new ArgumentException($"{nameof(alias)} is unexpected.");

			Alias = new IdentityName(this, alias);
		}

		public IdentityName Alias { get; }

		public override IEnumerable<IDiscriminatedElement> Children()
		{
			yield return Alias;
			yield return _qualifiedNames;
		}

		public override IEnumerable<IDiscriminatedElement> Descendants()
		{
			yield return Alias;

			foreach (var elem in _qualifiedNames.DescendantsAndSelf())
			{
				yield return elem;
			}
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			stringBuilder.Append("using ");
			Alias.Describe(stringBuilder);

			stringBuilder.Append("=");
			_qualifiedNames.Describe(stringBuilder);
			stringBuilder.Append(";");

		}
	}
}