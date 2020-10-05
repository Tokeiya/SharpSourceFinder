using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using FastEnumUtility;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
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


			foreach (var isUnsafe in boolAry)
			foreach (var isPartial in boolAry)
			foreach (var isStatic in boolAry)
			foreach (var scope in FastEnum.GetMembers<ScopeCategories>().Select(x => x.Value))
			{
				var ns = new NameSpace(new PhysicalStorage(PathA));
				var q = new QualifiedElement();
				_ = new IdentityElement(q, NsA);

				var x = new ClassElement(ns, scope, isUnsafe, isPartial, isStatic);
				q = new QualifiedElement();
				_ = new IdentityElement(q, "Class");


				ns = new NameSpace(new PhysicalStorage(PathB));
				q = new QualifiedElement();
				_ = new IdentityElement(q, NsA);

				var y = new ClassElement(ns, scope, isUnsafe, isPartial, isStatic);
				q = new QualifiedElement();
				_ = new IdentityElement(q, "Class");

				ns = new NameSpace(new PhysicalStorage(PathA));
				q = new QualifiedElement();
				_ = new IdentityElement(q, NsA);

				var z = new ClassElement(ns, scope, isUnsafe, isPartial, isStatic);
				q = new QualifiedElement();
				_ = new IdentityElement(q, "Class");

				yield return (x, y, z);



			}
		}

		static ClassElement Generate(NameSpace ns, ScopeCategories scope, bool isUnsafe, bool isPartial, bool isStatic,
			string identity)
		{
			var ret = new ClassElement(ns, scope, isUnsafe, isPartial, isStatic);
			_ = new IdentityElement(new QualifiedElement(ret), identity);
			return ret;
		}

		protected override IEnumerable<(TypeElement x, TypeElement y)> GenerateLogicallyInEquivalentSample()
		{
			var boolAry = new[] { true, false };

			static NameSpace nsA()
			{
				var ns = new NameSpace(new PhysicalStorage(PathA));
				var q = new QualifiedElement();
				_ = new IdentityElement(q, NsA);

				return ns;
			}

			static NameSpace nsB()
			{
				var ns = new NameSpace(new PhysicalStorage(PathA));
				var q = new QualifiedElement();
				_ = new IdentityElement(q, NsB);

				return ns;
			}

			{
				foreach (var isUnsafe in boolAry)
				foreach (var isPartial in boolAry)
				foreach (var isStatic in boolAry)
				foreach (var scope in FastEnum.GetMembers<ScopeCategories>().Select(x => x.Value))
				{
					var x = new ClassElement(nsA(), scope, isUnsafe, isPartial, isStatic);
					var q = new QualifiedElement();
					_ = new IdentityElement(q, "ClassA");

					var y = new ClassElement(nsA(), scope, isUnsafe, isPartial, isStatic);
					q = new QualifiedElement();
					_ = new IdentityElement(q, "ClassN");

					yield return (x, y);
				}
			}

			yield return (Generate(nsA(), ScopeCategories.Public, true, true, true, "Hoge"),
				Generate(nsB(), ScopeCategories.Public, true, true, true, "HOge"));

			yield return (Generate(nsA(), ScopeCategories.Public, true, true, true, "Hoge"),
					Generate(nsA(), ScopeCategories.Private, true, true, true, "Hoge"));

			yield return (Generate(nsA(), ScopeCategories.Public, true, true, true, "Hoge"),
					Generate(nsA(), ScopeCategories.Public, false, true, true, "Hoge"));

			yield return (Generate(nsA(), ScopeCategories.Public, true, true, true, "Hoge"),
				Generate(nsA(), ScopeCategories.Public, true, false, true, "Hoge"));

			yield return (Generate(nsA(), ScopeCategories.Public, true, true, true, "Hoge"),
				Generate(nsA(), ScopeCategories.Public, true, true, false, "Hoge"));


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
