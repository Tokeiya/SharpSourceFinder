using System;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityName : TerminalElement, IEquatable<IdentityName>
	{
		internal IdentityName(IDiscriminatedElement parent, string identity) : base(parent)
		{
			if (string.IsNullOrWhiteSpace(identity)) throw new ArgumentException($"{nameof(identity)} isn't accept empty or whitespace.");
			Identity = identity;

		}


		public string Identity { get; }

		public override void RegisterChild(IDiscriminatedElement child)
		=>throw new NotSupportedException("IdentityName can't have child.");
		public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Identity);

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is IdentityName other && Equals(other);
		}

		public override int GetHashCode() => Identity.GetHashCode();


		public static bool operator ==(IdentityName x, IdentityName y) => x.Identity == y.Identity;
		public static bool operator !=(IdentityName x, IdentityName y) => x.Identity != y.Identity;

		public bool Equals(IdentityName? other)
		{
			var ret = other switch
			{
				{ } another => another == this,
				_ => false
			};

			return ret;
		}
	}
}