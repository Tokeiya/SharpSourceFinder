using System;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;
using static Tokeiya3.SharpSourceFinderCore.IDiscriminatedElement;


namespace SharpSourceFinderCoreTests
{
	// ReSharper disable once InconsistentNaming
	public class IDiscriminatedElementTests
	{
		private readonly ITestOutputHelper _output;

		public IDiscriminatedElementTests(ITestOutputHelper output) => _output = output;

		[Fact]
		public void RootTest()
		{
			Assert.Throws<NotSupportedException>(() => Root.Parent);
			Assert.Throws<NotSupportedException>(() => Root.Identity);

			Assert.Throws<NotSupportedException>(() => Root.Describe());
			Assert.Throws<NotSupportedException>(() => Root.Describe(new StringBuilder()));
		}

		[Fact]
		public void StringBuilderPoolTest()
		{
			var bld = StringBuilderPool.Get();
			bld.Append("hello world");

			StringBuilderPool.Return(bld);
			bld.Length.Is(0);

		}


	}
}
