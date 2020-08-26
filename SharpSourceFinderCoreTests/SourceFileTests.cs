using System;
using ChainingAssertion;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class SourceFileTests
	{
		[Fact]
		public void SourceFileTest()
		{
			var expected = @"G:\Some\Bar.cs";
			var actual = new SourceFile(expected);

			actual.Path.Is(expected);

			Assert.Throws<ArgumentException>(() => new SourceFile(""));
			Assert.Throws<ArgumentException>(() => new SourceFile("  "));
		}
	}
}