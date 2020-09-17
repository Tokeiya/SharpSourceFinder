using System;
using System.Collections.Generic;
using System.Text;
using Tokeiya3.StringManipulator;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class QualifiedName : MultiDescendantsElement<IdentityName>
	{
		internal QualifiedName(IDiscriminatedElement parent) : base(parent)
		{
		}


		internal IdentityName Add(string name)
		{
			var ret = new IdentityName(this, name);
			return ret;
		}

		internal void Add(QualifiedName source)
		{
			if (ReferenceEquals(this, source)) throw new ArgumentException($"{nameof(source)} is same.");

			foreach (var elem in source.GetIdentityies()) Add(elem.Identity);
		}

		internal void Add(IdentityName name)
		{
			if (ReferenceEquals(this, name.Parent))
				throw new ArgumentException($"{nameof(name)} specified this child identity.");
			Add(name.Identity);
		}

		public override void Describe(StringBuilder stringBuilder, string indent, int depth)
		{
			AppendIndent(stringBuilder, indent, depth);

			foreach (var elem in ChildElements)
			{
				elem.Describe(stringBuilder, string.Empty, 0);
				stringBuilder.Append('.');
			}

			stringBuilder.Extract(..(stringBuilder.Length - 1));
		}


		public IEnumerable<IdentityName> GetIdentityies() => ChildElements;
	}
}