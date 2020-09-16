using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{


	public sealed class ClassType:TypeElement
	{
		public ClassType(NameSpace parent, Accessibilities accessibility, bool isPartial, string name) : base(parent, accessibility, isPartial, name)
		{
		}

		public ClassType(TypeElement parent, Accessibilities accessibility, bool isPartial, string name) : base(parent, accessibility, isPartial, name)
		{
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			
		}
	}
}
