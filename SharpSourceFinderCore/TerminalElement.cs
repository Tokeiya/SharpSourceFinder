using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class TerminalElement : DiscriminatedElement
	{
		protected TerminalElement(IDiscriminatedElement parent) : base(parent)
		{
#warning TerminalElement_Is_NotImpl
			throw new NotImplementedException("TerminalElement is not implemented");
		}
		public override IEnumerable<IDiscriminatedElement> Children()
		{
#warning Children_Is_NotImpl
			throw new NotImplementedException("Children is not implemented");
		}

		public override void RegisterChild(IDiscriminatedElement child)
		{
#warning RegisterChild_Is_NotImpl
			throw new NotImplementedException("RegisterChild is not implemented");
		}
	}
}