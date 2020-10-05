using ChainingAssertion;
using FastEnumUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class ClassElementTest : TypedElementTest<ClassElement>
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

		protected override IEnumerable<(ClassElement x, ClassElement y, ClassElement z)> GenerateLogicallyTransitiveSample()
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

		protected override IEnumerable<(ClassElement x, ClassElement y)> GenerateLogicallyInEquivalentSample()
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

		protected override IEnumerable<(ClassElement x, ClassElement y, ClassElement z)> GeneratePhysicallyTransitiveSample()
		{
			var boolAry = new[] { true, false };

			var seq = from isUnsafe in boolAry
					  from isPartial in boolAry
					  from isStatic in boolAry
					  from scope in FastEnum.GetMembers<ScopeCategories>().Select(x => x.Value)
					  select (isUnsafe, isPartial, isStatic, scope);

			foreach (var (isUnsafe, isPartial, isStatic, scope) in seq)
			{
				yield return (Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class"),
					Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class"),
					Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class"));
			}
		}

		protected override IEnumerable<(ClassElement x, ClassElement y)> GeneratePhysicallyInEqualitySample()
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
					var x = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "ClassA");
					var y = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "ClassN");
					yield return (x, y);

					x = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class");
					y = Generate(PathA, NsB, scope, isUnsafe, isPartial, isStatic, "Class");
					yield return (x, y);

					x = Generate(PathA, NsA, scope, isUnsafe, isPartial, isStatic, "Class");
					y = Generate(PathB, NsA, scope, isUnsafe, isPartial, isStatic, "Class");
					yield return (x, y);
				}
			}

			yield return (Generate(PathB, NsA, ScopeCategories.Public, true, true, true, "Hoge"),
					Generate(PathB, NsA, ScopeCategories.Private, true, true, true, "Hoge"));

			yield return (Generate(PathA, NsB, ScopeCategories.Public, true, true, true, "Hoge"),
					Generate(PathA, NsB, ScopeCategories.Public, false, true, true, "Hoge"));

			yield return (Generate(PathB, NsB, ScopeCategories.Public, true, true, true, "Hoge"),
				Generate(PathB, NsB, ScopeCategories.Public, true, false, true, "Hoge"));

			yield return (Generate(PathB, NsA, ScopeCategories.Public, true, true, true, "Hoge"),
				Generate(PathB, NsA, ScopeCategories.Public, true, true, false, "Hoge"));
		}

		protected override IEnumerable<(ClassElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			IDiscriminatedElement expected = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(expected);
			_ = new IdentityElement(q, "Hoge");

			var sample = new ClassElement(expected, ScopeCategories.Public, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "Piyo");

			yield return (sample, expected);

			expected = sample;

			sample = new ClassElement(expected, ScopeCategories.Private, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "InternalElement");

			yield return (sample, expected);
		}

		protected override IEnumerable<(ClassElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
			var expected = new PhysicalStorage(PathA);
			var ns = new NameSpace(expected);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "NameSpace");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "Class");

			yield return (sample, expected);
		}

		protected override IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateGetAncestorsSample()
		{
			var list = new List<IDiscriminatedElement>();


			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "NameSpace");
			list.Add(ns);

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "ExtClass");
			list.Reverse();
			yield return (sample, list);

#warning NotImplemented
			throw new NotImplementedException();

		}

		protected override IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateDescendantsSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(ClassElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			throw new NotImplementedException();
		}

		protected override void AreEqual(IQualified actual, IQualified expected)
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(ClassElement sample, Stack<(IdentityCategories category, string identity)> expected)> GenerateAggregateIdentitiesSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected, Action<ClassElement> registerAction)> GenerateRegisterChildSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(ClassElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(ClassElement sample, IQualified expectedIdentity, bool expectedIsUnsafe, bool expectedIsPartial, bool expectedIsStatic, ScopeCategories expectedScope, IPhysicalStorage expectedStorage)> GenerateSample()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<ClassElement> GenerateIdentityErrorGetterSample()
		{
			throw new NotImplementedException();
		}

		protected override void AreEqual(IdentityElement actual, IQualified expected)
		{
			throw new NotImplementedException();
		}
	}
}
