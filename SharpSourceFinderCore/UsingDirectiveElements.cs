using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public class UsingDirectiveElements:DiscriminatedElement
	{
		private readonly QualifiedName _qualifiedNames;
		public UsingDirectiveElements(IDiscriminatedElement parent) : base(parent, string.Empty)
		{
			_qualifiedNames = new QualifiedName(this);
		}

		public IdentityName Add(string name) => _qualifiedNames.Add(name);

		public override void Describe(StringBuilder stringBuilder)
		{
			stringBuilder.Append("using ");
			_qualifiedNames.Describe(stringBuilder);
			stringBuilder.Append(";");
		}

		public override IEnumerable<IDiscriminatedElement> Children() => _qualifiedNames.Children();

		public override IEnumerable<IDiscriminatedElement> Descendants() => _qualifiedNames.Descendants();
	}
}
