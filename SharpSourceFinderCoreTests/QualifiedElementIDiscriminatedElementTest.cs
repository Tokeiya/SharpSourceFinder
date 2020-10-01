using System;
using System.Collections.Generic;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class QualifiedElementIDiscriminatedElementTest : NonTerminalElementTest<QualifiedElement,IdentityElement>
	{
		public QualifiedElementIDiscriminatedElementTest(ITestOutputHelper output) : base(output)
		{
		}


		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected)
		{
#warning AreEqual_Is_NotImpl
			throw new NotImplementedException("AreEqual is not implemented");
		}

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected)
		{
#warning AreEqual_Is_NotImpl
			throw new NotImplementedException("AreEqual is not implemented");
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y, QualifiedElement z)> GenerateLogicallyTransitiveSample()
		{
#warning GenerateLogicallyTransitiveSample_Is_NotImpl
			throw new NotImplementedException("GenerateLogicallyTransitiveSample is not implemented");
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y)> GenerateLogicallyInEquivalentSample()
		{
#warning GenerateLogicallyInEquivalentSample_Is_NotImpl
			throw new NotImplementedException("GenerateLogicallyInEquivalentSample is not implemented");
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y, QualifiedElement z)> GeneratePhysicallyTransitiveSample()
		{
#warning GeneratePhysicallyTransitiveSample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicallyTransitiveSample is not implemented");
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y)> GeneratePhysicallyInEqualitySample()
		{
#warning GeneratePhysicallyInEqualitySample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicallyInEqualitySample is not implemented");
		}

		protected override IEnumerable<(QualifiedElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
#warning GenerateParentSample_Is_NotImpl
			throw new NotImplementedException("GenerateParentSample is not implemented");
		}

		protected override IEnumerable<(QualifiedElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
#warning GeneratePhysicalStorageSample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicalStorageSample is not implemented");
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateGetAncestorsSample(bool isContainSelf)
		{
#warning GenerateGetAncestorsSample_Is_NotImpl
			throw new NotImplementedException("GenerateGetAncestorsSample is not implemented");
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateChildrenSample()
		{
#warning GenerateChildrenSample_Is_NotImpl
			throw new NotImplementedException("GenerateChildrenSample is not implemented");
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateDescendantsSample(bool isContainSelf)
		{
#warning GenerateDescendantsSample_Is_NotImpl
			throw new NotImplementedException("GenerateDescendantsSample is not implemented");
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
#warning GenerateQualifiedNameSample_Is_NotImpl
			throw new NotImplementedException("GenerateQualifiedNameSample is not implemented");
			throw new NotImplementedException();
		}

		protected override void AreEqual(IQualified actual, IQualified expected)
		{
#warning AreEqual_Is_NotImpl
			throw new NotImplementedException("AreEqual is not implemented");
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedElement sample, Stack<(IdentityCategories category, string identity)> expected)> GenerateAggregateIdentitiesSample()
		{
#warning GenerateAggregateIdentitiesSample_Is_NotImpl
			throw new NotImplementedException("GenerateAggregateIdentitiesSample is not implemented");
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected, Action<QualifiedElement> registerAction)> GenerateRegisterChildSample()
		{
#warning GenerateRegisterChildSample_Is_NotImpl
			throw new NotImplementedException("GenerateRegisterChildSample is not implemented");
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedElement sample, IdentityElement errSample)> GenerateErrSample()
		{
#warning GenerateErrSample_Is_NotImpl
			throw new NotImplementedException("GenerateErrSample is not implemented");
			throw new NotImplementedException();
		}
	}
}