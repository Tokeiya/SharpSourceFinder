using System;
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

		public static bool operator ==(QualifiedName x, QualifiedName y)
		{
#warning ==_Is_NotImpl
			throw new NotImplementedException("== is not implemented");
		}

		public static bool operator !=(QualifiedName x, QualifiedName y)
		{
#warning !=_Is_NotImpl
			throw new NotImplementedException("!= is not implemented");
		}
	}
}