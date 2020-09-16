using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public class NamedElement:MultiDescendantsElement<IDiscriminatedElement>,IEquatable<NamedElement>
	{
		protected NamedElement(IDiscriminatedElement parent) : base(parent)
		{

		}




			public bool Equals(NamedElement? other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((NamedElement) obj);
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(NamedElement x, NamedElement y)
		{
#warning ==_Is_NotImpl
			throw new NotImplementedException("== is not implemented");
		}

		public static bool operator !=(NamedElement x, NamedElement y)
		{
#warning !=_Is_NotImpl
			throw new NotImplementedException("!= is not implemented");
		}
	}
}
