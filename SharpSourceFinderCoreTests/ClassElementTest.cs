using ChainingAssertion;
using FastEnumUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class ClassElementTest : TypedElementTest
	{
		const string PathA = @"C:\Hoge\Piyo.cs";
		const string PathB = @"D:\Foo\Bar.cs";

		const string NsA = "Tokeiya3";
		const string NsB = "時計屋";


		public ClassElementTest(ITestOutputHelper output) : base(output)
		{

		}
		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) => actual.IsSameReferenceAs(expected);

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) => actual.IsSameReferenceAs(expected);

		protected override IEnumerable<(TypeElement x, TypeElement y, TypeElement z)> GenerateLogicallyTransitiveSample()
		{
			var boolAry = new[] { true, false };

			var seq = from isUnsafe in boolAry
					  from isPartial in boolAry
					  from isStatic in boolAry
					  from scope in FastEnum.GetMembers<ScopeCategories>().Select(x=>x.Value)
					  select (isUnsafe, isPartial, isStatic, scope);

			foreach (var (isUnsafe, isPartial, isStatic, scope) in seq)
			{
				var x = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class");
				var y = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class");
				var z = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class");

				yield return (x, y, z);
			}
		}

		static ClassElement Generate(string path, string nameSpace, ScopeCategories scope, bool isUnsafe, bool isPartial, bool isStatic, string identity)
		{
			var ns = new NameSpace(new PhysicalStorage(path));
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, nameSpace);

			var ret = new ClassElement(ns, scope, isUnsafe, isPartial, isStatic);
			q = new QualifiedElement(ret);
			_ = new IdentityElement(q, identity);
			return ret;
		}

		protected override IEnumerable<(TypeElement x, TypeElement y)> GenerateLogicallyInEquivalentSample()
		{
			var boolAry = new[] { true, false };

			var seq = from isUnsafe in boolAry
					  from isPartial in boolAry
					  from isStatic in boolAry
					  from scope in FastEnum.GetMembers<ScopeCategories>().Select(x => x.Value)
					  select (isUnsafe, isPartial, isStatic, scope);

			{
				foreach (var (isUnsafe, isPartial, isStatic, scope) in seq)

				{
					var x = Generate(PathA,NsA, scope, isUnsafe, isPartial, isStatic, "ClassA");
					var y = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "ClassN");
					yield return (x, y);

					x = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class");
					y = Generate(PathA, NsB, scope, isUnsafe, isPartial, isStatic, "Class");
					yield return (x, y);

				}
			}

			yield return (Generate(PathB,NsA, ScopeCategories.Public, true, true, true, "Hoge"),
					Generate(PathB,NsA, ScopeCategories.Private, true, true, true, "Hoge"));

			yield return (Generate(PathA,NsB, ScopeCategories.Public, true, true, true, "Hoge"),
					Generate(PathA,NsB, ScopeCategories.Public, false, true, true, "Hoge"));

			yield return (Generate(PathB,NsB, ScopeCategories.Public, true, true, true, "Hoge"),
				Generate(PathB,NsB, ScopeCategories.Public, true, false, true, "Hoge"));

			yield return (Generate(PathB,NsA, ScopeCategories.Public, true, true, true, "Hoge"),
				Generate(PathB,NsA, ScopeCategories.Public, true, true, false, "Hoge"));
		}

		protected override IEnumerable<(TypeElement x, TypeElement y, TypeElement z)> GeneratePhysicallyTransitiveSample()
		{
#warning GeneratePhysicallyTransitiveSample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicallyTransitiveSample is not implemented");
		}

		protected override IEnumerable<(TypeElement x, TypeElement y)> GeneratePhysicallyInEqualitySample()
		{
#warning GeneratePhysicallyInEqualitySample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicallyInEqualitySample is not implemented");
		}

		protected override IEnumerable<(TypeElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
#warning GenerateParentSample_Is_NotImpl
			throw new NotImplementedException("GenerateParentSample is not implemented");
		}

		protected override IEnumerable<(TypeElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
#warning GeneratePhysicalStorageSample_Is_NotImpl
			throw new NotImplementedException("GeneratePhysicalStorageSample is not implemented");
		}

		protected override IEnumerable<(TypeElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateGetAncestorsSample()
		{
#warning GenerateGetAncestorsSample_Is_NotImpl
			throw new NotImplementedException("GenerateGetAncestorsSample is not implemented");
		}

		protected override IEnumerable<(TypeElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(TypeElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateDescendantsSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(TypeElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			throw new NotImplementedException();
		}

		protected override void AreEqual(IQualified actual, IQualified expected)
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(TypeElement sample, Stack<(IdentityCategories category, string identity)> expected)> GenerateAggregateIdentitiesSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(TypeElement sample, IReadOnlyList<IDiscriminatedElement> expected, Action<TypeElement> registerAction)> GenerateRegisterChildSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(TypeElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(TypeElement sample, IQualified expectedIdentity, bool expectedIsUnsafe, bool expectedIsPartial, bool expectedIsStatic, ScopeCategories expectedScope, IPhysicalStorage expectedStorage)> GenerateSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<TypeElement> GenerateIdentityErrorGetterSample()
		{
			throw new NotImplementedException();
		}

		protected override void AreEqual(IdentityElement actual, IQualified expected)
		{
			throw new NotImplementedException();
		}
	}
}
