using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class NonTerminalElementTest<T>:DiscriminatedElementInterfaceTest where T:IDiscriminatedElement
	{
		protected NonTerminalElementTest(ITestOutputHelper output):base(output){}

		protected abstract IEnumerable<(NonTerminalElement<T> sample, IReadOnlyList<IDiscriminatedElement> expected,Action<NonTerminalElement<T>> registerAction)>
			GenerateRegisterChildSample();

		protected abstract IEnumerable<(NonTerminalElement<T> sample, T errSample)> GenerateErrSample();



		[Trait("TestLayer", nameof(NonTerminalElementTest<T>))]
		[Fact]
		public void RegisterChildTest()
		{
			GenerateRegisterChildSample().IsNotEmpty();

			foreach (var (sample,expected,action) in GenerateRegisterChildSample())
			{
				action(sample);

				var actual = sample.Children().ToArray();
				actual.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++)
				{
					AreEqual(actual[i],expected[i]);
				}
			}
		}

		[Trait("TestLayer", nameof(NonTerminalElement<T>))]
		[Fact]
		public void ErrorRegisterChildTest()
		{
			GenerateErrSample().IsNotEmpty();

			foreach ((NonTerminalElement<T> sample, T errSample)  in GenerateErrSample())
			{
				Assert.Throws<ArgumentException>(() => sample.RegisterChild(errSample));
			}
		}


	}
}
