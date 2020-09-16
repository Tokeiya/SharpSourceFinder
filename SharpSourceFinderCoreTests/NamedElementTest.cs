using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class NamedElementTest:EquatabilityTester<NamedElement>
	{
		private readonly ITestOutputHelper _output;

		protected NamedElementTest(ITestOutputHelper output) => _output = output;


	}
}
