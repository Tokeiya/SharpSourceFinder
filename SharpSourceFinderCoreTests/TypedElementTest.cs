//using System.Collections.Generic;
//using ChainingAssertion;
//using Tokeiya3.SharpSourceFinderCore;
//using Xunit;
//using Xunit.Abstractions;

//namespace SharpSourceFinderCoreTests
//{
//	public abstract class TypedElementTest<T> : NonTerminalElementTest<T, IDiscriminatedElement> where T:TypeElement
//	{
//		protected TypedElementTest(ITestOutputHelper output) : base(output)
//		{
//		}
		
//		protected abstract
//			IEnumerable<(T sample, IQualified expectedIdentity, bool expectedIsUnsafe, bool
//				expectedIsPartial, bool expectedIsStatic, ScopeCategories expectedScope, IPhysicalStorage
//				expectedStorage)> GenerateSample();

//		protected abstract IEnumerable<T> GenerateIdentityErrorGetterSample();


//		protected abstract void AreEqual(IdentityElement actual, IQualified expected);

//		[Trait("TestLayer", nameof(TypeElement))]
//		[Fact]
//		public void IdentityErrorTest()
//		{
//			GenerateIdentityErrorGetterSample().IsNotEmpty();

//			foreach (var elem in GenerateIdentityErrorGetterSample())
//			{
//				Assert.Throws<IdentityNotFoundException>(() => _ = elem.Identity);
//			}
//		}




//		[Trait("TestLayer", nameof(TypeElement))]
//		[Fact]
//		public void IdentityTest()
//		{
//			GenerateSample().IsNotEmpty();

//			foreach (var (sample, expected, _, _, _, _, _) in GenerateSample()) AreEqual(sample.Identity, expected);
//		}

//		[Trait("TestLayer", nameof(TypeElement))]
//		[Fact]
//		public void IsUnsafeGetterTest()
//		{
//			GenerateSample().IsNotEmpty();

//			foreach (var (sample, _, expected, _, _, _, _) in GenerateSample()) sample.IsUnsafe.Is(expected);
//		}

//		[Trait("TestLayer", nameof(TypeElement))]
//		[Fact]
//		public void IsPartialGetterTest()
//		{
//			GenerateSample().IsNotEmpty();

//			foreach (var (sample, _, _, expected, _, _, _) in GenerateSample()) sample.IsPartial.Is(expected);
//		}

//		[Trait("TestLayer", nameof(TypeElement))]
//		[Fact]
//		public void IsStaticGetterTest()
//		{
//			GenerateSample().IsNotEmpty();

//			foreach (var (sample, _, _, _, expected, _, _) in GenerateSample()) sample.IsStatic.Is(expected);
//		}

//		[Trait("TestLayer", nameof(TypeElement))]
//		[Fact]
//		public void ScopeGetterTest()
//		{
//			GenerateSample().IsNotEmpty();

//			foreach (var (sample, _, _, _, _, expected, _) in GenerateSample()) sample.Scope.Is(expected);
//		}
//	}
//}