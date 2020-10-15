using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using FastEnumUtility;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public abstract class TypedElementTest<T> : NonTerminalElementTest<T, IDiscriminatedElement> where T : TypeElement
	{
		protected const string PathA = @"C:\Hoge\Piyo.cs";
		protected const string PathB = @"D:\Foo\Bar.cs";
		protected const string NameSpaceA = "Tokeiya3";
		protected const string NameSpaceB = "時計屋";

		protected TypedElementTest(ITestOutputHelper output) : base(output)
		{
		}

		private IReadOnlyList<(bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
			ScopeCategories scope)> Combination { get; } =
			(from scope in FastEnum.GetMembers<ScopeCategories>().Select(x => x.Value)
				let boolAry = new[] {true, false}
				from isAbstract in boolAry
				from isSealed in boolAry
				from isUnsafe in boolAry
				from isPartial in boolAry
				from isStatic in boolAry
				where !((isAbstract && (isSealed || isStatic)) || (isSealed && isStatic))
				select (isAbstract, isSealed, isUnsafe, isPartial, isStatic, scope)).ToArray();

		protected abstract IEnumerable<T> GenerateIdentityErrorGetterSample();

		protected abstract bool TryGenerate(string path, string nameSpace, ScopeCategories scope, bool isAbstract,
			bool isSealed, bool isUnsafe, bool isPartial, bool isStatic, string identity,
			out (IPhysicalStorage expectedStorage, NameSpace expectedNameSpace, IQualified expectedIdentity, T sample)
				generated);


		protected override IEnumerable<(T x, T y, T z)> GenerateLogicallyTransitiveSample()
		{
			foreach (var (isAbstract, isSealed, isUnsafe, isPartial, isStatic, scope) in Combination)
				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var x) &&
				    TryGenerate(PathB, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var y) &&
				    TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var z))
					yield return (x.sample, y.sample, z.sample);
		}

		protected override IEnumerable<(T x, T y, T z)> GeneratePhysicallyTransitiveSample()
		{
			foreach (var (isAbstract, isSealed, isUnsafe, isPartial, isStatic, scope) in Combination)
				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var x) &&
				    TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var y) &&
				    TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var z))
					yield return (x.sample, y.sample, z.sample);
		}

		static ScopeCategories Offset(ScopeCategories value)
		{
			var ret = (ScopeCategories) ((int) value + 1);
			return FastEnum.IsDefined(ret) ? ret : FastEnum.GetMinValue<ScopeCategories>()!.Value;
		}

		protected override IEnumerable<(T x, T y)> GenerateLogicallyInEquivalentSample()
		{

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
			{
				void write()
				{
					Output.WriteLine($"{isAbstract},{isSealed},{isUnsafe},{isPartial},{isStatic},{scope}");
				}

				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var x) &&
				    TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic, "Hoge",
					    out var y))
					yield return (x.sample, y.sample);


				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out x) &&
				    TryGenerate(PathA, NameSpaceB, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity",
					    out y))
					yield return (x.sample, y.sample);

				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out x) &&
				    TryGenerate(PathA, NameSpaceA, Offset(scope), isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out y))
					yield return (x.sample, y.sample);

				if (TryGenerate(PathA, NameSpaceA, scope, !isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out x) &&
				    TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out y))
				{
					write();
					yield return (x.sample, y.sample);
				}


				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, !isSealed, isUnsafe, isPartial, isStatic,
					"Identity", out x) && TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe,
					isPartial, isStatic, "Identity", out y))
				{
					write();
					yield return (x.sample, y.sample);
				}

				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, !isUnsafe, isPartial, isStatic,
					"Identity", out x) && TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe,
					isPartial, isStatic, "Identity", out y))
				{

					write();
					yield return (x.sample, y.sample);
				}

				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, !isPartial, isStatic,
					"Identity", out x) && TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe,
					isPartial, isStatic, "Identity", out y))
				{

					write();
					
					yield return (x.sample, y.sample);
				}

				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, !isStatic,
					"Identity", out x) && TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe,
					isPartial, isStatic, "Identity", out y))
				{
					write();

					yield return (x.sample, y.sample);
				}
			}
		}

		protected override IEnumerable<(T x, T y)> GeneratePhysicallyInEqualitySample()
		{
			foreach (var elem in GenerateLogicallyInEquivalentSample()) yield return elem;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var x) &&
				    TryGenerate(PathB, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					    "Identity", out var y))
					yield return (x.sample, y.sample);
		}

		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void AddNameSpaceAsAChildTest()
		{
			var flg = false;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic, ScopeCategories scope)  in Combination)
			{
				if(TryGenerate(PathA,NameSpaceA,scope,isAbstract,isSealed,isUnsafe,isPartial,isStatic,"Identity",out var gen))
				{
					flg = true;
					Assert.Throws<ArgumentException>(() => new NameSpace(gen.sample));
				}
			}

			flg.IsTrue();
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IdentityErrorTest()
		{
			GenerateIdentityErrorGetterSample().IsNotEmpty();

			foreach (var elem in GenerateIdentityErrorGetterSample())
				Assert.Throws<IdentityNotFoundException>(() => _ = elem.Identity);
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IdentityGetTest()
		{
			var isRun = false;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic, "Hoge",
					out var generated))
				{
					isRun = true;

					AreEqual(generated.sample.Identity, generated.expectedIdentity);
				}

			isRun.IsTrue();
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsUnsafeGetterTest()
		{
			var isRun = false;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic, "Hoge",
					out var generated))
				{
					isRun = true;
					generated.sample.IsUnsafe.Is(isUnsafe);
				}

			isRun.IsTrue();
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsPartialGetterTest()
		{
			var isRun = true;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathB, NameSpaceB, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic, "Piyo",
					out var generated))
				{
					isRun = true;
					generated.sample.IsPartial.Is(isPartial);
				}

			isRun.IsTrue();
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsStaticGetterTest()
		{
			var isRun = true;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathB, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic, "Foo",
					out var generated))
				{
					isRun = true;
					generated.sample.IsStatic.Is(isStatic);
				}

			isRun.IsTrue();
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void ScopeGetterTest()
		{
			var isRun = true;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathA, NameSpaceB, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic, "Bar",
					out var generated))
				{
					isRun = true;
					generated.sample.Scope.Is(scope);
				}

			isRun.IsTrue();
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsAbstractGetterTest()
		{
			var isRun = true;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathB, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic, "ClassA",
					out var generated))
				{
					isRun = true;
					generated.sample.IsAbstract.Is(isAbstract);
				}

			isRun.IsTrue();
		}


		[Trait("TestLayer", nameof(TypeElement))]
		[Fact]
		public void IsSealedGetterTest()
		{
			var isRun = true;

			foreach ((bool isAbstract, bool isSealed, bool isUnsafe, bool isPartial, bool isStatic,
				ScopeCategories scope) in Combination)
				if (TryGenerate(PathA, NameSpaceA, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic,
					"Identity", out var generated))
				{
					isRun = true;
					generated.sample.IsSealed.Is(isSealed);
				}

			isRun.IsTrue();
		}
	}
}