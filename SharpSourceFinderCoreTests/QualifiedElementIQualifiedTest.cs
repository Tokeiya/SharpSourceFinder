using ChainingAssertion;
using System.Collections.Generic;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class QualifiedElementIQualifiedTest : QualifiedInterfaceTest
	{
		private const string PathA = "C:\\Hoge\\Piyo.cs";
		private const string PathB = "D:\\Foo\\Bar.cs";


		protected QualifiedElementIQualifiedTest(ITestOutputHelper output) : base(output)
		{

		}

		protected override void AreEqual(IQualified actual, IQualified expected) => ReferenceEquals(actual, expected).IsTrue();

		protected override void AreEqual(IIdentity actual, IIdentity expected) => ReferenceEquals(actual, expected).IsTrue();


		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();


		protected override IEnumerable<(IQualified sample, IReadOnlyList<IIdentity> expected)>
			GenerateIdentitiesSample()
		{
			var sample = new QualifiedElement();
			var expected = new[] { new IdentityElement(sample, "Hoge"), new IdentityElement(sample, "Piyo") };

			yield return (sample, expected);

			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			sample = new QualifiedElement(ns);
			expected = new[] { new IdentityElement(sample, "Foo"), new IdentityElement(sample, "Bar") };

			yield return (sample, expected);


			ns = new NameSpace(ns);
			sample = new QualifiedElement(ns);
			expected = new[] { new IdentityElement(sample, "System"), new IdentityElement(sample, "Collections"), };

			yield return (sample, expected);


		}

		protected override IEnumerable<(IQualified x, IQualified y, IQualified z)> GenerateEquivalentSample()
		{
			throw new System.NotImplementedException();
		}

		protected override IEnumerable<(IQualified x, IQualified y)> GenerateInEquivalentSample()
		{
			throw new System.NotImplementedException();
		}

	}
}
