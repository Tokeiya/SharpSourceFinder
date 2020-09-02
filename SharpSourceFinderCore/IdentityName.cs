using System;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityName : TerminalElement
	{
		internal IdentityName(IDiscriminatedElement parent, string identity) : base(parent)
		{
			if (string.IsNullOrWhiteSpace(identity)) throw new ArgumentException($"{nameof(identity)} isn't accept empty or whitespace.");
			Identity = identity;

		}

		public string Identity { get; }

		public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Identity);

		public override bool Equals(object obj)
		{
			if (obj is IdentityName another)
			{
				return another == this;
			}

			return false;

		}

		public override int GetHashCode() => Identity.GetHashCode();


		public static bool operator ==(IdentityName x, IdentityName y) => x.Identity == y.Identity;
		public static bool operator !=(IdentityName x, IdentityName y) => x.Identity != y.Identity;
	}
}