using System;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityName : TerminalElement
	{
		internal IdentityName(IDiscriminatedElement parent, string name) : base(parent, name)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(name)} isn't accept empty or whitespace.");
		}

		public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Representation);
	}
}