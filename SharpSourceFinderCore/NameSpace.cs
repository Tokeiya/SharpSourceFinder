using System;
using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameSpace:MultiDescendantsElement<IDiscriminatedElement>
	{
		private QualifiedName? _name;

		public QualifiedName Name
		{
			get
			{
#warning Name_Is_NotImpl
				throw new NotImplementedException("Name is not implemented");
			}
		}

		internal void SetName(QualifiedName name)
		{
#warning SetName_Is_NotImpl
			throw new NotImplementedException("SetName is not implemented");
		}


		public NameSpace(IDiscriminatedElement parent) : base(parent)
		{
#warning NameSpace_Is_NotImpl
			throw new NotImplementedException("NameSpace is not implemented");
		}

		public QualifiedName GetFullQualifiedName()
		{
#warning GetFUllQualifiedName_Is_NotImpl
			throw new NotImplementedException("GetFUllQualifiedName is not implemented");
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			throw new NotImplementedException();
		}
	}
}
