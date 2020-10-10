using ChainingAssertion;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class NonTerminalElementTest<T, U> : DiscriminatedElementInterfaceTest<T>
		where T : NonTerminalElement<U>
		where U : IDiscriminatedElement
	{
		protected NonTerminalElementTest(ITestOutputHelper output) : base(output)
		{
		}

		protected abstract IEnumerable<(T sample, IReadOnlyList<IDiscriminatedElement> expected,
				Action<T> registerAction)>
			GenerateRegisterChildSample();

		protected abstract IEnumerable<(T sample, IDiscriminatedElement errSample)> GenerateErrSample();


		[Trait("TestLayer", nameof(NonTerminalElementTest<T, U>))]
		[Fact]
		public void RegisterChildTest()
		{
			GenerateRegisterChildSample().IsNotEmpty();

			foreach (var (sample, expected, action) in GenerateRegisterChildSample())
			{
				action(sample);

				var actual = sample.Children().ToArray();
				actual.Length.Is(expected.Count);

				for (int i = 0; i < expected.Count; i++) AreEqual(actual[i], expected[i]);
			}
		}

		[Trait("TestLayer", nameof(NonTerminalElement<T>))]
		[Fact]
		public void ErrorRegisterChildTest()
		{
			GenerateErrSample().IsNotEmpty();

			foreach ((T sample, IDiscriminatedElement errSample) in GenerateErrSample())
				Assert.Throws<ArgumentException>(() => sample.RegisterChild(errSample));
		}
	}
}