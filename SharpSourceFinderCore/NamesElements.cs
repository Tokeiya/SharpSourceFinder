using System.Text;
using Tokeiya3.StringManipulator;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NamesElements : MultiDescendantsElement<NameExpressionElement>
	{
		internal NamesElements(IDiscriminatedElement parent, string identity) : base(parent, identity)
		{
		}

		public NameExpressionElement Add(string name)
		{
			var ret = new NameExpressionElement(this, name);
			Add(ret);
			return ret;
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			foreach (var elem in ChildElements) stringBuilder.Append(elem.Representation).Append('.');

			stringBuilder.Extract(..(stringBuilder.Length - 1));
		}
	}
}