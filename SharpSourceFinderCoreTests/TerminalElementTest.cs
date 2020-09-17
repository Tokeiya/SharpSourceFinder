using System;
using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;
using static Xunit.Assert;

namespace SharpSourceFinderCoreTests
{
	public abstract class TerminalElementTest<T>:DiscriminatedElementTest<T> where T:TerminalElement
	{
		protected TerminalElementTest(ITestOutputHelper output):base(output){}

		[Trait("Type","TerminalElement")]
		[Fact]
		public void RegisterChildTest()
		{
			var sample = new SourceFile("hoge");

			foreach (var (actual,_) in GetTestSamples())
			{
				Throws<NotSupportedException>(() => actual.RegisterChild(sample));
			}
		}

		[Trait("Type", "TerminalElement")]
		[Fact]
		public void ChildrenTest()
		{
			foreach (var (actual,_) in GetTestSamples())
			{
				actual.Children().Any().IsFalse();
			}
		}

		[Trait("Type", "TerminalElement")]
		[Fact]
		public void DescendantsTest()
		{
			foreach (var (actual, _) in GetTestSamples())
			{
				actual.Descendants().Any().IsFalse();
			}
		}

	}
}
