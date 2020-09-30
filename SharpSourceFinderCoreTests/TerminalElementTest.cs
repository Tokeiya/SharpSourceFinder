using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class TerminalElementTest:DiscriminatedElementInterfaceTest
	{
		private TerminalElementTest(ITestOutputHelper output):base(output){}

		protected abstract IEnumerable<TerminalElement> GenerateTerminalElementSample();


		[Trait("TestLayer", nameof(TerminalElement))]
		[Fact]
		public void RegisterChildTest()
		{
			GenerateTerminalElementSample().IsNotEmpty();

			foreach (var elem in GenerateTerminalElementSample())
			{
				Assert.Throws<ArgumentException>(() => new IdentityElement(elem, "Some"));
			}
		}

	}
}
