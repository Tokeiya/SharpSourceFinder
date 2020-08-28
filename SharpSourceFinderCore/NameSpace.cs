using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameSpace:MultiDescendantsElement<IDiscriminatedElement>
	{
		
		public NameSpace(IDiscriminatedElement parent, string identity) : base(parent, identity)
		{
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			throw new NotImplementedException();
		}
	}
}
