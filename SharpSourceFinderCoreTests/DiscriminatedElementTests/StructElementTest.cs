using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class StructElementTest : TypedElementTest<StructElement>
	{
		public StructElementTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) =>
			actual.IsSameReferenceAs(expected);

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			actual.IsSameReferenceAs(expected);

		static IQualified AttachName(IDiscriminatedElement target, params string[] name)
		{
			var q = new QualifiedElement(target);

			foreach (var s in name) _ = new IdentityElement(q, s);

			return q;
		}

		[Trait("TestLayer", nameof(StructElement))]
		[Fact]
		public void InvalidChildTest()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new StructElement(ns, ScopeCategories.Public, false, false);
			AttachName(sample, "Hoge");

			Assert.Throws<ArgumentException>(() => new NameSpaceElement(sample));
		}


		[Trait("TestLayer", nameof(StructElement))]
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

			IDiscriminatedElement parent = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(parent, "Hoge", "Piyo");

			parent = new ClassElement(parent, ScopeCategories.Public, false, false, false, false, false);
			AttachName(parent, "Foo");

			var sample = new StructElement(parent, ScopeCategories.Public, false, false);
			AttachName(sample, "Bar");
			var actual = sample.GetQualifiedName();

			actual.Identities.Count.Is(4);

			areEquivalent(actual.Identities[0], IdentityCategories.Namespace, "Hoge", actual, 1);
			areEquivalent(actual.Identities[1], IdentityCategories.Namespace, "Piyo", actual, 2);
			areEquivalent(actual.Identities[2], IdentityCategories.Class, "Foo", actual, 3);
			areEquivalent(actual.Identities[3], IdentityCategories.Struct, "Bar", actual, 4);
		}

		protected override IEnumerable<(StructElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, "Hoge", "Piyo");

			var sample = new StructElement(ns, ScopeCategories.Public, false, false);
			yield return (sample, ns);
		}

		protected override IEnumerable<(StructElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample()
		{
			var storage = new PhysicalStorage(PathB);
			var ns = new NameSpaceElement(storage);
			AttachName(ns, "Hoge");

			var sample = new StructElement(ns, ScopeCategories.Public, false, false);
			yield return (sample, storage);
		}

		protected override IEnumerable<(StructElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var upper = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			AttachName(upper, "Upper");

			var sample = new StructElement(upper, ScopeCategories.Public, false, false);

			yield return (sample, new IDiscriminatedElement[] {upper, ns});
		}

		protected override IEnumerable<(StructElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new StructElement(ns, ScopeCategories.Public, false, false);
			var q = AttachName(sample, "Hoge");

			yield return (sample, new IDiscriminatedElement[] {(QualifiedElement) q});
		}

		protected override IEnumerable<(StructElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new StructElement(ns, ScopeCategories.Public, false, false);
			var q = AttachName(sample, "Foo");

			yield return (sample, new[] {(IDiscriminatedElement) q, (IDiscriminatedElement) q.Identities[0]});
		}

		protected override IEnumerable<(StructElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, "Hoge");

			var sample = new StructElement(ns, ScopeCategories.Public, false, false);
			AttachName(sample, "Foo");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(expected, IdentityCategories.Struct, "Foo");

			yield return (sample, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			actual.IsEquivalentTo(expected).IsTrue();


		protected override
			IEnumerable<(StructElement sample, Stack<(IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
			IDiscriminatedElement ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, "Upper");

			ns = new NameSpaceElement(ns);
			AttachName(ns, "Foo", "Bar");

			ns = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			AttachName(ns, "Class");

			var sample = new StructElement(ns, ScopeCategories.Public, false, false);
			AttachName(sample, "ValueType");


			var expected = new Stack<(IdentityCategories, string)>();

			expected.Push((IdentityCategories.Struct, "ValueType"));

			yield return (sample, expected);
		}

		protected override
			IEnumerable<(StructElement sample, IReadOnlyList<IDiscriminatedElement> expected, Action<StructElement>
				registerAction)> GenerateRegisterChildSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new StructElement(ns, ScopeCategories.Public, false, false);

			var expected = new List<IDiscriminatedElement>();

			void act(StructElement element)
			{
				var q = new QualifiedElement(element);
				expected.Add(q);
			}

			yield return (sample, expected, act);
		}

		protected override IEnumerable<(StructElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new StructElement(ns, ScopeCategories.Public, false, false);

			var another = new StructElement(ns, ScopeCategories.Public, false, false);

			yield return (sample, another);
		}

		protected override IEnumerable<StructElement> GenerateIdentityErrorGetterSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, "Hoge");

			var sample = new StructElement(ns, ScopeCategories.Public, false, false);

			yield return sample;
		}

		protected override bool TryGenerate(string path, string nameSpace, ScopeCategories scope, bool isAbstract,
			bool isSealed, bool isUnsafe,
			bool isPartial, bool isStatic, string identity,
			out (IPhysicalStorage expectedStorage, NameSpaceElement expectedNameSpace, IQualified expectedIdentity,
				StructElement
				sample) generated)
		{
			if (!isSealed || isStatic || isAbstract)
			{
				generated = default;
				return false;
			}

			var storage = new PhysicalStorage(path);
			var ns = new NameSpaceElement(storage);
			AttachName(ns, nameSpace);

			var sample = new StructElement(ns, scope, isUnsafe, isPartial);
			var q = AttachName(sample, identity);

			generated.sample = sample;
			generated.expectedIdentity = q;
			generated.expectedNameSpace = ns;
			generated.expectedStorage = storage;

			return true;
		}
	}
}