using System.Collections.Generic;
using ChainingAssertion;
using FastEnumUtility;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public abstract class TypedElementTest<T> : NonTerminalElementTest<T, IDiscriminatedElement> where T : TypeElement
	{
		protected TypedElementTest(ITestOutputHelper output) : base(output)
		{
		}

		protected abstract IEnumerable<T> GenerateIdentityErrorGetterSample();


		protected abstract void AreEqual(IdentityElement actual, IQualified expected);

		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IdentityErrorTest()
		{
			GenerateIdentityErrorGetterSample().IsNotEmpty();

			foreach (var elem in GenerateIdentityErrorGetterSample())
			{
				Assert.Throws<IdentityNotFoundException>(() => _ = elem.Identity);
			}
		}


		protected abstract IEnumerable<(T sample, IQualified expected)> GenerateIdentityGetterSample();


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IdentityTest()
		{
			GenerateIdentityGetterSample().IsNotEmpty();

			foreach ((T sample, IQualified expected)  in GenerateIdentityGetterSample())
			{
				AreEqual(sample.Identity,expected);
			}
		}

		protected abstract IEnumerable<(T sample, bool expected)> GenerateIsUnsafeGetterSample();


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsUnsafeGetterTest()
		{
			GenerateIsUnsafeGetterSample().IsNotEmpty();

			foreach (var (sample, expected) in GenerateIsUnsafeGetterSample()) sample.IsUnsafe.Is(expected);
		}

		protected abstract IEnumerable<(T sample, bool expected)> GenerateIsPartialGetterSample();

		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsPartialGetterTest()
		{
			GenerateIsPartialGetterSample().IsNotEmpty();
			foreach (var (sample,expected) in GenerateIsPartialGetterSample()) sample.IsPartial.Is(expected);
		}

		protected abstract IEnumerable<(T sample, bool expected)> GenerateIsStaticGetterTest();
		
		
		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsStaticGetterTest()
		{
			GenerateIsStaticGetterTest().IsNotEmpty();

			foreach (var (sample, expected) in GenerateIsStaticGetterTest()) sample.IsStatic.Is(expected);
		}

		protected abstract IEnumerable<(T sample, ScopeCategories expected)> GenerateScopeGetterSample();

		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void ScopeGetterTest()
		{
			GenerateScopeGetterSample().IsNotEmpty();

			foreach (var (sample, expected) in GenerateScopeGetterSample())
			{
				FastEnum.IsDefined(sample.Scope).IsTrue();
				FastEnum.IsDefined(expected).IsTrue();

				sample.Scope.Is(expected);
			}
		}

		protected abstract IEnumerable<(T sample, bool expected)> GenerateIsAbstractSample();

		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsAbstractGetterTest()
		{
			GenerateIsAbstractSample().IsNotEmpty();

			foreach ((T sample, bool expected)  in GenerateIsAbstractSample())
			{
				sample.IsAbstract.Is(expected);

				if(sample.IsAbstract) sample.IsSealed.IsFalse();
			}
		}

		protected abstract IEnumerable<(T sample, bool expected)> GenerateIsSealedSample();

		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsSealedGetterTest()
		{
			GenerateIsSealedSample().IsNotEmpty();

			foreach (var (sample,expected) in GenerateIsSealedSample())
			{
				sample.IsSealed.Is(expected);

				if (sample.IsSealed) sample.IsAbstract.IsFalse();
			}
		}



	}
}