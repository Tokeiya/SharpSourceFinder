using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) => ReferenceEquals(actual, expected);

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected);


		protected override IEnumerable<(IdentityElement x, IdentityElement y, IdentityElement z)> GenerateLogicallyTransitiveSample()
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

			_ = new IdentityElement(qx, "Foo");
			_ = new IdentityElement(qy, "Bar");
			_ = new IdentityElement(qz, "Piyo");

			yield return (x, y, z);
		}

		protected override IEnumerable<(IdentityElement x, IdentityElement y)> GenerateLogicallyInEquivalentSample()
		{
#warning GenerateLogicallyInEquivalentSample_Is_NotImpl
			throw new NotImplementedException("GenerateLogicallyInEquivalentSample is not implemented");
		}

		protected override IEnumerable<(IdentityElement x, IdentityElement y, IdentityElement z)> GeneratePhysicallyTransitiveSample()
		{
#warning GeneratePhysicallyTransitiveSample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicallyTransitiveSample is not implemented");
		}

		protected override IEnumerable<(IdentityElement x, IdentityElement y)> GeneratePhysicallyInEqualitySample()
		{
#warning GeneratePhysicallyInEqualitySample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicallyInEqualitySample is not implemented");
		}

		protected override IEnumerable<(IdentityElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
#warning GenerateParentSample_Is_NotImpl
			throw new NotImplementedException("GenerateParentSample is not implemented");
		}

		protected override IEnumerable<(IdentityElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			var sample = new IdentityElement(q, "Name");

			yield return (sample, storage);

		}

		protected override IEnumerable<(IdentityElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateGetAncestorsSample(bool isContainSelf)
		{
#warning GenerateGetAncestorsSample_Is_NotImpl
			throw new NotImplementedException("GenerateGetAncestorsSample is not implemented");
		}

		protected override IEnumerable<(IdentityElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateChildrenSample()
		{
#warning GenerateChildrenSample_Is_NotImpl
			throw new NotImplementedException("GenerateChildrenSample is not implemented");
		}

		protected override IEnumerable<(IdentityElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateDescendantsSample(bool isContainSelf)
		{
#warning GenerateDescendantsSample_Is_NotImpl
			throw new NotImplementedException("GenerateDescendantsSample is not implemented");
		}

		protected override IEnumerable<(IdentityElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
#warning GenerateQualifiedNameSample_Is_NotImpl
			throw new NotImplementedException("GenerateQualifiedNameSample is not implemented");
		}

		protected override void AreEqual(IQualified actual, IQualified expected)
		{
#warning AreEqual_Is_NotImpl
			throw new NotImplementedException("AreEqual is not implemented");
		}

		protected override IEnumerable<(IdentityElement sample, Stack<(IdentityCategories category, string identity)> expected)> GenerateAggregateIdentitiesSample()
		{
#warning GenerateAggregateIdentitiesSample_Is_NotImpl
			throw new NotImplementedException("GenerateAggregateIdentitiesSample is not implemented");
		}

		[Trait("TestLayer", nameof(IdentityElement))]
		[Fact]
		public void RegisterChildTest()
		{
			var q = new QualifiedElement();
			var sample = new IdentityElement(q, IdentityCategories.Struct, "Foo");

			Assert.Throws<InvalidOperationException>(() => sample.RegisterChild(new IdentityElement(q,IdentityCategories.Struct,"foo")));

		}

	}
}