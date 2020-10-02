using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class QualifiedElementIQualifiedTest : QualifiedInterfaceTest
	{
		private const string PathA = "C:\\Hoge\\Piyo.cs";
		private const string PathB = "D:\\Foo\\Bar.cs";


		public QualifiedElementIQualifiedTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override void AreEqual(IIdentity actual, IIdentity expected) =>
			ReferenceEquals(actual, expected).IsTrue();


		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();


		protected override IEnumerable<(IQualified sample, IReadOnlyList<IIdentity> expected)>
			GenerateIdentitiesSample()
		{
			var sample = new QualifiedElement();
			var expected = new[]
			{
				new IdentityElement(sample, IdentityCategories.Namespace, "Hoge"),
				new IdentityElement(sample, IdentityCategories.Namespace, "Piyo")
			};

			yield return (sample, expected);

			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			sample = new QualifiedElement(ns);
			expected = new[] {new IdentityElement(sample, "Foo"), new IdentityElement(sample, "Bar")};

			yield return (sample, expected);


			ns = new NameSpace(ns);
			sample = new QualifiedElement(ns);
			expected = new[] {new IdentityElement(sample, "System"), new IdentityElement(sample, "Collections")};

			yield return (sample, expected);
		}

		protected override IEnumerable<(IQualified x, IQualified y, IQualified z)> GenerateEquivalentSample()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();
			var z = new QualifiedElement();

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Foo");
			_ = new IdentityElement(y, IdentityCategories.Namespace, "Foo");
			_ = new IdentityElement(z, IdentityCategories.Namespace, "Foo");

			yield return (x, y, z);

			x = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));
			y = new QualifiedElement(new NameSpace(new PhysicalStorage(PathB)));
			z = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));

			_ = new IdentityElement(x, "Hoge");
			_ = new IdentityElement(y, "Hoge");
			_ = new IdentityElement(z, "Hoge");

			yield return (x, y, z);

			_ = new IdentityElement(x, "Piyo");
			_ = new IdentityElement(y, "Piyo");
			_ = new IdentityElement(z, "Piyo");

			yield return (x, y, z);
		}

		protected override IEnumerable<(IQualified x, IQualified y)> GenerateInEquivalentSample()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();

			_ = new IdentityElement(x, IdentityCategories.Class, "Hoge");
			_ = new IdentityElement(y, IdentityCategories.Delegate, "Hoge");
			yield return (x, y);

			x = new QualifiedElement();
			y = new QualifiedElement();

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y, IdentityCategories.Namespace, "hoge");
			yield return (x, y);
		}

		[Trait("TestLayer", nameof(QualifiedElement))]
		[Fact]
		public void OrderedEquivalentTest()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Foo");
			_ = new IdentityElement(y, IdentityCategories.Namespace, "Foo");

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Bar");
			_ = new IdentityElement(y, IdentityCategories.Namespace, "Hoge");

			x.IsEquivalentTo(y, 1).IsTrue();
			y.IsEquivalentTo(x, 1).IsTrue();

			x.IsEquivalentTo(y, 2).IsFalse();
			y.IsEquivalentTo(x, 2).IsFalse();

			Assert.Throws<ArgumentOutOfRangeException>(() => x.IsEquivalentTo(y, 0));
		}
	}
}