using System;
using System.Text;
using Tokeiya3.StringManipulator;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class QualifiedName : MultiDescendantsElement<IdentityName>
	{
		internal QualifiedName(IDiscriminatedElement parent) : base(parent, String.Empty)
		{
		}

		public IdentityName Add(string name)
		{
			var ret = new IdentityName(this, name);
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