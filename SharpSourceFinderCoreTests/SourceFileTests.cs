using Xunit;
using Tokeiya3.SharpSourceFinderCore;
using System;
using System.Collections.Generic;
using System.Text;
using ChainingAssertion;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class SourceFileTests
	{
		[Fact()]
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