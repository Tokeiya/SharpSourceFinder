using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class ClassElementTest : TypedElementTest<ClassElement>
	{
		public ClassElementTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) =>
			actual.IsSameReferenceAs(expected);

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			actual.IsSameReferenceAs(expected);


		static QualifiedElement AttachName(IDiscriminatedElement target, params string[] names)
		{
			var ret = new QualifiedElement(target);

			foreach (var name in names) _ = new IdentityElement(ret, name);

			return ret;
		}

		[Trait("TestLayer", nameof(ClassElement))]
		[Fact]
		public void GetQualifiedTest()
		{
			static void areEquivalent(IIdentity actual, IdentityCategories expectedCategory, string expectedName,
				IQualified expectedQualified, int expectedOrder)
			{
				actual.Category.Is(expectedCategory);
				actual.Name.Is(expectedName);
				actual.From.IsSameReferenceAs(expectedQualified);
				actual.Order.Is(expectedOrder);
			}

			IDiscriminatedElement parent = new NameSpace(new PhysicalStorage(PathA));
			AttachName(parent, "Hoge", "Piyo");

			parent = new ClassElement(parent, ScopeCategories.Public, false, false, false, false, false);
			AttachName(parent, "Foo");

			var sample = new ClassElement(parent, ScopeCategories.Public, false, false, false, false, false);
			AttachName(sample, "Bar");
			var actual = sample.GetQualifiedName();

			actual.Identities.Count.Is(4);

			areEquivalent(actual.Identities[0], IdentityCategories.Namespace, "Hoge", actual, 1);
			areEquivalent(actual.Identities[1], IdentityCategories.Namespace, "Piyo", actual, 2);
			areEquivalent(actual.Identities[2], IdentityCategories.Class, "Foo", actual, 3);
			areEquivalent(actual.Identities[3], IdentityCategories.Class, "Bar", actual, 4);
		}


		protected override IEnumerable<(ClassElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			IDiscriminatedElement expected = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(expected);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(expected, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "SampleClass");
			yield return (sample, expected);

			expected = sample;
			sample = new ClassElement(expected, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "InnerClass");
			yield return (sample, expected);
		}

		protected override IEnumerable<(ClassElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
			var expected = new PhysicalStorage(PathA);
			var ns = new NameSpace(expected);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "Hoge");

			yield return (sample, expected);
		}

		protected override IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "Hoge");

			yield return (sample, new[] {ns});
		}

		protected override IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "Hoge");

			yield return (sample, new[] {q});
		}

		protected override IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			var i = new IdentityElement(q, "Hoge");

			yield return (sample, new IDiscriminatedElement[] {q, i});
		}

		protected override IEnumerable<(ClassElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "Hoge");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected, IdentityCategories.Namespace, "Foo");
			_ = new IdentityElement(expected, IdentityCategories.Class, "Hoge");

			yield return (sample, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			actual.IsEquivalentTo(expected).IsTrue();


		protected override
			IEnumerable<(ClassElement sample, Stack<(IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, "Hoge");

			var expected = new Stack<(IdentityCategories, string)>();
			expected.Push((IdentityCategories.Class, "Hoge"));

			yield return (sample, expected);
		}

		protected override
			IEnumerable<(ClassElement sample, IReadOnlyList<IDiscriminatedElement> expected, Action<ClassElement>
				registerAction)> GenerateRegisterChildSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			var expected = new List<IDiscriminatedElement>();


			void act(ClassElement elem)
			{
				q = new QualifiedElement(elem);
				expected.Add(q);
				_ = new IdentityElement(q, "Hoge");
			}

			yield return (sample, expected, act);
		}

		protected override IEnumerable<(ClassElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected, IdentityCategories.Class, "Error");

			yield return (sample, expected);
		}

		protected override IEnumerable<ClassElement> GenerateIdentityErrorGetterSample()
		{
			var storage = new PhysicalStorage(PathA);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);

			yield return sample;
		}

		protected override bool TryGenerate(string path, string nameSpace, ScopeCategories scope, bool isAbstract,
			bool isSealed, bool isUnsafe,
			bool isPartial, bool isStatic, string identity,
			out (IPhysicalStorage expectedStorage, NameSpace expectedNameSpace, IQualified expectedIdentity,
				ClassElement sample
				) generated)
		{
			if (isAbstract && (isStatic || isSealed))
			{
				generated = default;
				return false;
			}

			if (isStatic && isSealed)
			{
				generated = default;
				return false;
			}

			var storage = new PhysicalStorage(path);
			var ns = new NameSpace(storage);
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, nameSpace);

			var sample = new ClassElement(ns, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic);
			q = new QualifiedElement(sample);
			_ = new IdentityElement(q, identity);


			generated.sample = sample;
			generated.expectedIdentity = q;
			generated.expectedNameSpace = ns;
			generated.expectedStorage = storage;

			return true;
		}
	}
}