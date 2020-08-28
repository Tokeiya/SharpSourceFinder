using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameSpace:MultiDescendantsElement<IDiscriminatedElement>
	{
		
		public NameSpace(IDiscriminatedElement parent) : base(parent)
		{
#warning NameSpace_Is_NotImpl
			throw new NotImplementedException("NameSpace is not implemented");
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			throw new NotImplementedException();
		}
	}
}
