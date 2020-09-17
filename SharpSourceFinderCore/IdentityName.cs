using System;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityName : TerminalElement
	{
		internal IdentityName(IDiscriminatedElement parent, string identity) : base(parent)
		{
			if (string.IsNullOrWhiteSpace(identity))
				throw new ArgumentException($"{nameof(identity)} isn't accept empty or whitespace.");
			Identity = identity;
		}


		public string Identity { get; }


		public override void Describe(StringBuilder stringBuilder, string indent, int depth) =>
			AppendIndent(stringBuilder, indent, depth).Append(Identity);
	}
}