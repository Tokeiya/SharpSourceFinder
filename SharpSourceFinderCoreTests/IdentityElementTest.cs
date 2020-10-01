using ChainingAssertion;
using System.Collections.Generic;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class IdentityElementIdentityInterFaceTest : IdentityInterfaceTest
	{
		private readonly PhysicalStorage _strageA = new PhysicalStorage("C:\\Hoge\\Piyo.cs");
		private readonly PhysicalStorage _storageB = new PhysicalStorage("D:\\Foo\\Bar.cs");


		protected IdentityElementIdentityInterFaceTest(ITestOutputHelper output) : base(output) { }
		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) => ReferenceEquals(actual, expected).IsTrue();

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override IEnumerable<(IIdentity x, IIdentity y, IIdentity z)> GenerateTransitiveSample()
		{

			throw new System.NotImplementedException();
		}

		protected override IEnumerable<(IIdentity x, IIdentity y)> GenerateInEquivalentSample()
		{
			throw new System.NotImplementedException();
		}



		protected override IEnumerable<(IIdentity sample, string expectedName, IdentityCategories expectedCategory, IQualified expectedFrom)> GenerateSample()
		{
			throw new System.NotImplementedException();
		}

		protected override IEnumerable<(IIdentity sample, int expected)> GenerateOrderSample()
		{
			throw new System.NotImplementedException();
		}
	}
}
