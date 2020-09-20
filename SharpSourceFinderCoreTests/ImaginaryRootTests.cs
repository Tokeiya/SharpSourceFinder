using System;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;
using static Tokeiya3.SharpSourceFinderCore.DiscriminatedElement;


namespace SharpSourceFinderCoreTests
{
	// ReSharper disable once InconsistentNaming
	public class ImaginaryRootTests
	{
		private readonly ITestOutputHelper _output;

		public ImaginaryRootTests(ITestOutputHelper output) => _output = output;

		[Fact]
		public void RootTest()
		{
			Assert.Throws<NotSupportedException>(() => Root.Parent);

			Assert.Throws<NotSupportedException>(() => Root.Describe());
			Assert.Throws<NotSupportedException>(() => Root.Describe(new StringBuilder(), "\t", 0));

			Assert.Throws<NotSupportedException>(() => Root.Ancestors());
			Assert.Throws<NotSupportedException>(() => Root.AncestorsAndSelf());

			Assert.Throws<NotSupportedException>(() => Root.Descendants());
			Assert.Throws<NotSupportedException>(() => Root.DescendantsAndSelf());

			Assert.Throws<NotSupportedException>(() => Root.Children());
			Assert.Throws<NotSupportedException>(() => Root.RegisterChild(new SourceFile("path")));

			var tmp = new SourceFile("hoge");

			Root.IsFullyEquivalentTo(Root).IsTrue();
			Root.IsFullyEquivalentTo(tmp).IsFalse();

			Root.IsIndividualEquivalentTo(Root).IsTrue();
			Root.IsFullyEquivalentTo(tmp).IsFalse();

			Root.IsEquivalentToIncludeAncestors(Root).IsTrue();
			Root.IsEquivalentToIncludeAncestors(tmp).IsFalse();

		}
	}
}