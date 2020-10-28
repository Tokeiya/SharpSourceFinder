using System.Collections.Generic;
using ChainingAssertion;
using FastEnumUtility;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
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
			foreach (var scope in FastEnum.GetValues<ScopeCategories>())
			{

				var x = new IdentityElement(new QualifiedElement(), scope, elem, scope.ToString() + elem.ToString());
				var y = new IdentityElement(new QualifiedElement(), scope, elem, scope.ToString() + elem.ToString());
				var z = new IdentityElement(new QualifiedElement(), scope, elem, scope.ToString() + elem.ToString());

				yield return (x, y, z);
			}
		}

		protected override IEnumerable<(IIdentity x, IIdentity y)> GenerateInEquivalentSample()
		{
			var q = new QualifiedElement();
			var x = new IdentityElement(q, ScopeCategories.Public,IdentityCategories.Namespace, "Hoge");
			var y = new IdentityElement(q, ScopeCategories.Public,IdentityCategories.Namespace, "Hoge");
			yield return (x, y);

			x = new IdentityElement(new QualifiedElement(), ScopeCategories.Public,IdentityCategories.Namespace, "Hoge");
			y = new IdentityElement(new QualifiedElement(), ScopeCategories.Public,IdentityCategories.Class, "Hoge");

			yield return (x, y);

			x = new IdentityElement(new QualifiedElement(), ScopeCategories.Public,IdentityCategories.Class, "Foo");
			y = new IdentityElement(new QualifiedElement(),ScopeCategories.Public, IdentityCategories.Class, "Bar");
			yield return (x, y);

			x = new IdentityElement(new QualifiedElement(), ScopeCategories.Internal, IdentityCategories.Class, "Foo");
			y = new IdentityElement(new QualifiedElement(), ScopeCategories.Public, IdentityCategories.Class, "Foo");
			yield return (x, y);

		}


		protected override
			IEnumerable<(IIdentity sample, string expectedName,ScopeCategories expectedScope, IdentityCategories expectedCategory, IQualified
				expectedFrom)> GenerateSample()
		{
			var q = new QualifiedElement();
			var sample = new IdentityElement(q, ScopeCategories.Public,IdentityCategories.Class, "Hoge");

			yield return (sample, "Hoge", ScopeCategories.Public,IdentityCategories.Class, q);
		}

		protected override IEnumerable<(IIdentity sample, int expected)> GenerateOrderSample()
		{
			var q = new QualifiedElement();
			return new (IIdentity sample, int expected)[]
			{
				(new IdentityElement(q, ScopeCategories.Public,IdentityCategories.Class, "Foo"), 1),
				(new IdentityElement(q, ScopeCategories.Public,IdentityCategories.Class, "Bar"), 2),
				(new IdentityElement(q, ScopeCategories.Public,IdentityCategories.Struct, "Hoge"), 3),
				(new IdentityElement(new QualifiedElement(), ScopeCategories.Public,IdentityCategories.Namespace, "Hoge"), 1)
			};
		}
	}
}