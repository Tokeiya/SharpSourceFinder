using System;
using System.Collections.Generic;
using ChainingAssertion;
using FastEnumUtility;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class IdentityElementIIdentityTest : IdentityInterfaceTest
	{


		public IdentityElementIIdentityTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override IEnumerable<(IIdentity x, IIdentity y, IIdentity z)> GenerateTransitiveSample()
		{
			foreach (var elem in FastEnum.GetValues<IdentityCategories>())
			{
				var x = new IdentityElement(new QualifiedElement(), elem, elem.ToString());
				var y = new IdentityElement(new QualifiedElement(), elem, elem.ToString());
				var z = new IdentityElement(new QualifiedElement(), elem, elem.ToString());

				yield return (x, y, z);
			}

		}

		protected override IEnumerable<(IIdentity x, IIdentity y)> GenerateInEquivalentSample()
		{
			var q = new QualifiedElement();
			var x = new IdentityElement(q, IdentityCategories.Namespace, "Hoge");
			var y = new IdentityElement(q, IdentityCategories.Namespace, "Hoge");
			yield return (x, y);

			x = new IdentityElement(new QualifiedElement(), IdentityCategories.Namespace, "Hoge");
			y = new IdentityElement(new QualifiedElement(), IdentityCategories.Class, "Hoge");

			yield return (x, y);

			x = new IdentityElement(new QualifiedElement(), IdentityCategories.Class, "Foo");
			y = new IdentityElement(new QualifiedElement(), IdentityCategories.Class, "Bar");
			yield return (x, y);

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
			var q = new QualifiedElement();
			return new (IIdentity sample, int expected)[]
			{
				(new IdentityElement(q, IdentityCategories.Class, "Foo"), 1),
				(new IdentityElement(q, IdentityCategories.Class, "Bar"), 2),
				(new IdentityElement(q, IdentityCategories.Struct, "Hoge"), 3),
				(new IdentityElement(new QualifiedElement(), IdentityCategories.Namespace, "Hoge"), 1)
			};


		}
	}
}