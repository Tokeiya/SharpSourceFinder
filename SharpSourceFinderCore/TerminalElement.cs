using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class TerminalElement : DiscriminatedElement
	{
		protected TerminalElement(IDiscriminatedElement parent) : base(parent)
		{
		}

		public override IEnumerable<IDiscriminatedElement> Children() => Array.Empty<IDiscriminatedElement>();

		public override IEnumerable<IDiscriminatedElement> Descendants() => Array.Empty<IDiscriminatedElement>();
	}
}