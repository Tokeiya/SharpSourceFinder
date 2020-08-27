using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class IdentityName : TerminalElement
	{
		internal IdentityName(QualifiedName parent, string identity) : base(parent, identity)
		{
		}

		public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Representation);
	}
}