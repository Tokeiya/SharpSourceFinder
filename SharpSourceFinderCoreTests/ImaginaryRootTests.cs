using System;
using System.Text;
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
			Assert.Throws<NotSupportedException>(() => Root.Describe(new StringBuilder()));

			Assert.Throws<NotSupportedException>(() => Root.Ancestors());
			Assert.Throws<NotSupportedException>(() => Root.AncestorsAndSelf());

			Assert.Throws<NotSupportedException>(() => Root.Descendants());
			Assert.Throws<NotSupportedException>(() => Root.DescendantsAndSelf());

			Assert.Throws<NotSupportedException>(() => Root.Children());
		}
	}
}