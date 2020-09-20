using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class NonTerminalElement<T> : DiscriminatedElement where T : IDiscriminatedElement
	{
		protected NonTerminalElement()
		{
#warning NonTerminalElement_Is_NotImpl
			throw new NotImplementedException("NonTerminalElement is not implemented");
		}
		protected NonTerminalElement(IDiscriminatedElement parent) : base(parent)
		{
#warning NonTerminalElement_Is_NotImpl
			throw new NotImplementedException("NonTerminalElement is not implemented");
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