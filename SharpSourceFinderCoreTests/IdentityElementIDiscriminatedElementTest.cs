using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class IdentityElementIDiscriminatedElementTest : DiscriminatedElementInterfaceTest<IdentityElement>
	{
		private const string PathA = @"C:\Hoge\Piyo.cs";
		private const string PathB = @"D:\Foo\Bar.cs";

		public IdentityElementIDiscriminatedElementTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();


		protected override IEnumerable<(IdentityElement x, IdentityElement y, IdentityElement z)>
			GenerateLogicallyTransitiveSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var qx = new QualifiedElement(ns);
			var x = new IdentityElement(qx, "Hoge");

			ns = new NameSpace(new PhysicalStorage(PathB));
			var qy = new QualifiedElement(ns);
			var y = new IdentityElement(qy, "Hoge");

			ns = new NameSpace(new PhysicalStorage(PathB));
			var qz = new QualifiedElement(ns);
			var z = new IdentityElement(qz, "Hoge");

			yield return (x, y, z);

		}

		protected override IEnumerable<(IdentityElement x, IdentityElement y)> GenerateLogicallyInEquivalentSample()
		{
			var qx = new QualifiedElement();
			var x = new IdentityElement(qx, IdentityCategories.Delegate, "Hoge");

			var qy = new QualifiedElement();
			var y = new IdentityElement(qy, IdentityCategories.Class, "Hoge");

			yield return (x, y);
		}

		protected override IEnumerable<(IdentityElement x, IdentityElement y, IdentityElement z)>
			GeneratePhysicallyTransitiveSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			var x = new IdentityElement(q, "Foo");

			ns = new NameSpace(new PhysicalStorage(PathA));
			q = new QualifiedElement(ns);
			var y = new IdentityElement(q, "Foo");

			ns = new NameSpace(new PhysicalStorage(PathA));
			q = new QualifiedElement(ns);
			var z = new IdentityElement(q, "Foo");

			yield return (x, y, z);

		}

		protected override IEnumerable<(IdentityElement x, IdentityElement y)> GeneratePhysicallyInEqualitySample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			var x = new IdentityElement(q, "Foo");

			ns = new NameSpace(new PhysicalStorage(PathB));
			q = new QualifiedElement(ns);
			var y = new IdentityElement(q, "Foo");

			yield return (x, y);
		}

		protected override IEnumerable<(IdentityElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var expected = new QualifiedElement();
			var sample = new IdentityElement(expected, IdentityCategories.Class, "Hoge");

			yield return (sample, expected);

		}

		protected override IEnumerable<(IdentityElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			var sample = new IdentityElement(q, "Name");

			yield return (sample, storage);
		}

		protected override IEnumerable<(IdentityElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			var sample = new IdentityElement(q, "Identity");

			yield return (sample, new IDiscriminatedElement[] {q,ns});
		}

		protected override IEnumerable<(IdentityElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			var sample = new IdentityElement(q, "Identity");

			yield return (sample, Array.Empty<IDiscriminatedElement>());


		}

		protected override IEnumerable<(IdentityElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			var sample = new IdentityElement(q, "Identity");

			yield return (sample, Array.Empty<IDiscriminatedElement>());
		}

		protected override IEnumerable<(IdentityElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			var sample = new IdentityElement(q, "Identity");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected, IdentityCategories.Namespace, "Identity");

			yield return (sample, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected) => actual.IsEquivalentTo(expected).IsTrue();

		protected override
			IEnumerable<(IdentityElement sample, Stack<(IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			var sample = new IdentityElement(q, "Identity");

			var expected = new Stack<(IdentityCategories, string)>();
			expected.Push((IdentityCategories.Namespace, "Identity"));

			yield return (sample, expected);

		}

		[Trait("TestLayer", nameof(IdentityElement))]
		[Fact]
		public void RegisterChildTest()
		{
			var q = new QualifiedElement();
			var sample = new IdentityElement(q, IdentityCategories.Struct, "Foo");

			Assert.Throws<InvalidOperationException>(() =>
				sample.RegisterChild(new IdentityElement(q, IdentityCategories.Struct, "foo")));
		}
	}
}