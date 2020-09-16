using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class TypeElementTest : EquatabilityTester<TypeElement>
	{
		private readonly ITestOutputHelper _output;

		protected TypeElementTest(ITestOutputHelper output) => _output = output;

		protected abstract IEnumerable<(TypeElement actual, QualifiedName expected)> GetQualifiedNameSamples();

		protected abstract IEnumerable<(TypeElement actual, Accessibilities expected)> GetAccessibilitiesSamples();

		protected abstract IEnumerable<(TypeElement actual, bool expected)> GetIsPartialSamples();

		[Fact]
		public void GetFullQualifiedNameTest()
		{
			foreach (var elem in GetQualifiedNameSamples())
			{
				elem.actual.GetFullQualifiedName().Is(elem.expected);
			}
		}

		[Fact]
		public void GetAccessibilitiesTest()
		{
			foreach (var elem in GetAccessibilitiesSamples())
			{
				elem.actual.Accessibility.Is(elem.expected);
			}
		}

		[Fact]
		public void GetIsPartialTest()
		{
			foreach (var elem in GetIsPartialSamples())
			{
				elem.actual.IsPartial.Is(elem.expected);
			}
		}


	}
}
