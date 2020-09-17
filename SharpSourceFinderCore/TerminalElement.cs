using System;
using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class TerminalElement : DiscriminatedElement
	{
		protected TerminalElement(IDiscriminatedElement parent) : base(parent)
		{
		}

		public sealed override void RegisterChild(IDiscriminatedElement child) =>
			throw new NotSupportedException($"{nameof(RegisterChild)} is not supported.");

		public sealed override IEnumerable<IDiscriminatedElement> Children() => Array.Empty<IDiscriminatedElement>();

		public sealed override IEnumerable<IDiscriminatedElement> Descendants() => Array.Empty<IDiscriminatedElement>();
	}
}