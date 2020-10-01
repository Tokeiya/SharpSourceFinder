using System;
using System.Collections.Generic;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class TerminalElementTest : DiscriminatedElementInterfaceTest
	{
		private TerminalElementTest(ITestOutputHelper output) : base(output)
		{
		}

		protected abstract IEnumerable<TerminalElement> GenerateTerminalElementSample();


		[Trait("TestLayer", nameof(TerminalElement))]
		[Fact]
		public void RegisterChildTest()
		{
#warning RegisterChildTest_Is_NotImpl
			throw new NotImplementedException("RegisterChildTest is not implemented");
		}
	}
}