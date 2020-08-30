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
#warning Equals_Is_NotImpl
			throw new NotImplementedException("Equals is not implemented");
		}

		public override int GetHashCode()
		{
#warning GetHashCode_Is_NotImpl
			throw new NotImplementedException("GetHashCode is not implemented");
		}

		public static bool operator ==(IdentityName x,IdentityName y)
		{
#warning ==_Is_NotImpl
			throw new NotImplementedException("== is not implemented");
		}

		public static bool operator !=(IdentityName x, IdentityName y)
		{
#warning !=_Is_NotImpl
			throw new NotImplementedException("!= is not implemented");
		}

	}
}