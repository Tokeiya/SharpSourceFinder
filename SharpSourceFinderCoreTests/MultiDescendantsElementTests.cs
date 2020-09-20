using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class MultiDescendantsElementTests<T> : DiscriminatedElementTest<T> where T : DiscriminatedElement
	{

		protected MultiDescendantsElementTests(ITestOutputHelper output) : base(output)
		{
		}

		protected abstract IEnumerable<(T sample, HashSet<IDiscriminatedElement> expected)> GenerateChildrenSamples();

		protected abstract IEnumerable<(T sample, HashSet<IDiscriminatedElement> expected)>
			GenerateDescendantsSamples();


		[Fact]
		[Trait("Type", "MultiDescendantsElementTests")]
		public void DescendantsTest()
		{
#warning DescendantsTest_Is_NotImpl
			throw new NotImplementedException("DescendantsTest is not implemented");
		}


	}
}
