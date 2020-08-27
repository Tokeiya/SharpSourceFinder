using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameExpressionElement : TerminalElement
	{
		internal NameExpressionElement(NamesElements parent, string identity) : base(parent, identity)
		{
		}

		public override void Describe(StringBuilder stringBuilder) => stringBuilder.Append(Representation);
	}
}