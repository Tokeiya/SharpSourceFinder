using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class IdentityElementIIdentityTest : IdentityInterfaceTest
	{
		private readonly PhysicalStorage _storageB = new PhysicalStorage("D:\\Foo\\Bar.cs");
		private readonly PhysicalStorage _strageA = new PhysicalStorage("C:\\Hoge\\Piyo.cs");


		protected IdentityElementIIdentityTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override IEnumerable<(IIdentity x, IIdentity y, IIdentity z)> GenerateTransitiveSample()
		{
#warning GenerateTransitiveSample_Is_NotImpl
			throw new NotImplementedException("GenerateTransitiveSample is not implemented");
		}

		protected override IEnumerable<(IIdentity x, IIdentity y)> GenerateInEquivalentSample()
		{
#warning GenerateInEquivalentSample_Is_NotImpl
			throw new NotImplementedException("GenerateInEquivalentSample is not implemented");
		}


		protected override
			IEnumerable<(IIdentity sample, string expectedName, IdentityCategories expectedCategory, IQualified
				expectedFrom)> GenerateSample()
		{
#warning GenerateSample_Is_NotImpl
			throw new NotImplementedException("GenerateSample is not implemented");
		}

		protected override IEnumerable<(IIdentity sample, int expected)> GenerateOrderSample()
		{
#warning GenerateOrderSample_Is_NotImpl
			throw new NotImplementedException("GenerateOrderSample is not implemented");
		}
	}
}