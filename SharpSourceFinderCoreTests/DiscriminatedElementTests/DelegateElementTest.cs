using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class DelegateElementTest : TypedElementTest<DelegateElement>
	{
		public DelegateElementTest(ITestOutputHelper output) : base(output)
		{
		}

		static (QualifiedElement, IReadOnlyList<IdentityElement>) AttachName(IDiscriminatedElement element,
			params string[] names)
		{
			var list = new List<IdentityElement>();
			var ret = new QualifiedElement(element);

			foreach (var name in names) list.Add(new IdentityElement(ret, name));

			return (ret, list);
		}


		[Trait("TestLayer", nameof(DelegateElement))]
		[Fact]
		public void InvalidRegisterChildTest()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			AttachName(sample, "BinOp");

			Assert.Throws<ArgumentException>(() =>
				_ = new InterfaceElement(sample, ScopeCategories.Public, false, false));
			Assert.Throws<ArgumentException>(() =>
				_ = new ClassElement(sample, ScopeCategories.Public, false, false, false, false, false));
		}


		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) =>
			actual.IsSameReferenceAs(expected);

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			actual.IsSameReferenceAs(expected);

		protected override IEnumerable<(DelegateElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			AttachName(sample, "Hoge");

			yield return (sample, ns);
		}

		protected override IEnumerable<(DelegateElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample()
		{
			var expected = new PhysicalStorage(PathA);
			var ns = new NameSpaceElement(expected);
			AttachName(ns, NameSpaceA);

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			yield return (sample, expected);
		}

		protected override IEnumerable<(DelegateElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var list = new List<IDiscriminatedElement>();

			IDiscriminatedElement parent = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(parent, "Outer");
			list.Add(parent);

			parent = new NameSpaceElement(parent);
			AttachName(parent, "Inner");
			list.Add(parent);

			parent = new InterfaceElement(parent, ScopeCategories.Public, false, false);
			AttachName(parent, "IOuter");
			list.Add(parent);

			var sample = new DelegateElement(parent, ScopeCategories.Public, false);
			AttachName(sample, "BinaryOp");

			list.Reverse();
			yield return (sample, list);
		}

		protected override IEnumerable<(DelegateElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			var (expected, _) = AttachName(sample, "Hoge");

			yield return (sample, new[] {(IDiscriminatedElement) expected});
		}

		protected override IEnumerable<(DelegateElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			var (expected, id) = AttachName(sample, "Hoge");

			yield return (sample, new[] {(IDiscriminatedElement) expected, (IDiscriminatedElement) id[0]});
		}

		protected override IEnumerable<(DelegateElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, "Hoge");

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			AttachName(sample, "Foo");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected,ScopeCategories.Public, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(expected,ScopeCategories.Public ,IdentityCategories.Delegate, "Foo");


			yield return (sample, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			actual.IsEquivalentTo(expected).IsTrue();

		protected override
			IEnumerable<(DelegateElement sample, Stack<(ScopeCategories scope,IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			AttachName(sample, "Hoge");

			var expected = new Stack<(ScopeCategories,IdentityCategories, string)>();
			expected.Push((ScopeCategories.Public,IdentityCategories.Delegate, "Hoge"));

			yield return (sample, expected);
		}

		protected override
			IEnumerable<(DelegateElement sample, IReadOnlyList<IDiscriminatedElement> expected, Action<DelegateElement>
				registerAction)> GenerateRegisterChildSample()
		{
			var expected = new List<IDiscriminatedElement>();
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);

			void act(DelegateElement elem)
			{
				expected.Add(AttachName(elem, "Hoge").Item1);
			}

			yield return (sample, expected, act);
		}

		protected override IEnumerable<(DelegateElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var q = AttachName(ns, NameSpaceA);
			var errSample = new IdentityElement(q.Item1, "Err");

			var sample = new DelegateElement(ns, ScopeCategories.Public, false);
			AttachName(sample, "Name");

			yield return (sample, errSample);
		}

		protected override IEnumerable<DelegateElement> GenerateIdentityErrorGetterSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			yield return new DelegateElement(ns, ScopeCategories.Public, false);
		}

		protected override bool TryGenerate(string path, string nameSpace, ScopeCategories scope, bool isAbstract,
			bool isSealed, bool isUnsafe,
			bool isPartial, bool isStatic, string identity,
			out (IPhysicalStorage expectedStorage, NameSpaceElement expectedNameSpace, IQualified expectedIdentity,
				DelegateElement
				sample) generated)
		{
			generated = default;
			if (isUnsafe || !isSealed || isPartial || isStatic || isAbstract) return false;

			generated.expectedStorage = new PhysicalStorage(path);
			generated.expectedNameSpace = new NameSpaceElement(generated.expectedStorage);
			AttachName(generated.expectedNameSpace, nameSpace);

			generated.sample = new DelegateElement(generated.expectedNameSpace, scope, isUnsafe);
			generated.expectedIdentity = AttachName(generated.sample, identity).Item1;

			return true;
		}
	}
}