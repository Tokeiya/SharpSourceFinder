using System;
using System.Text;
using Tokeiya3.StringManipulator;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class QualifiedName : MultiDescendantsElement<IdentityName>, IEquatable<QualifiedName>
	{
		internal QualifiedName(IDiscriminatedElement parent) : base(parent)
		{
		}

		public bool Equals(QualifiedName other) => other == this;

		internal IdentityName Add(string name)
		{
			var ret = new IdentityName(this, name);
			Add(ret);
			return ret;
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			foreach (var elem in ChildElements)
			{
				elem.Describe(stringBuilder);
				stringBuilder.Append('.');
			}

			stringBuilder.Extract(..(stringBuilder.Length - 1));
		}

		public override bool Equals(object obj) => obj switch
		{
			null => false,
			{ } when ReferenceEquals(this, obj) => true,
			QualifiedName other => other == this,
			_ => false
		};

		public override int GetHashCode()
		{
			var ret = 0;

			foreach (var elem in ChildElements) ret ^= elem.GetHashCode();

			return ret;
		}

		public static bool operator ==(QualifiedName x, QualifiedName y)
		{
			if (x.ChildElements.Count != y.ChildElements.Count) return false;

			for (int i = 0; i < x.ChildElements.Count; i++)
				if (x.ChildElements[i] != y.ChildElements[i])
					return false;

			return true;
		}

		public static bool operator !=(QualifiedName x, QualifiedName y) => !(x == y);
	}
}