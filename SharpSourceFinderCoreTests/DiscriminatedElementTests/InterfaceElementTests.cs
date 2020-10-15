using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class InterfaceElementTests : TypedElementTest<InterfaceElement>
	{
		public InterfaceElementTests(ITestOutputHelper output) : base(output)
		{
		}

		static QualifiedElement AttachName(IDiscriminatedElement element, params string[] names)
		{
			var q = new QualifiedElement(element);

			foreach (var name in names) _ = new IdentityElement(q, name);

			return q;
		}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) =>
			actual.IsSameReferenceAs(expected);

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			actual.IsSameReferenceAs(expected);


		protected override IEnumerable<(InterfaceElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var expected = new NameSpace(new PhysicalStorage(PathA));
			AttachName(expected, NameSpaceA);

			var sample = new InterfaceElement(expected, ScopeCategories.Public, false, false);
			AttachName(sample, "ISome");


			yield return (sample, expected);
		}

		protected override IEnumerable<(InterfaceElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample()
		{
			var expected = new PhysicalStorage(PathA);
			var ns = new NameSpace(expected);
			AttachName(ns, NameSpaceA);

			var sample = new InterfaceElement(ns, ScopeCategories.Public, false, false);
			AttachName(sample, "ISome");

			yield return (sample, expected);
		}

		protected override IEnumerable<(InterfaceElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var list = new List<IDiscriminatedElement>();

			IDiscriminatedElement parent = new NameSpace(new PhysicalStorage(PathA));
			AttachName(parent, "Outer");
			list.Add(parent);

			parent = new NameSpace(parent);
			AttachName(parent, "Inner");
			list.Add(parent);

			parent = new InterfaceElement(parent, ScopeCategories.Public, false, false);
			AttachName(parent, "IOuter");
			list.Add(parent);

			var sample = new InterfaceElement(parent, ScopeCategories.Public, false, false);
			AttachName(sample, "ISample");

			list.Reverse();
			yield return (sample, list);
		}

		protected override IEnumerable<(InterfaceElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var list = new List<IDiscriminatedElement>();

			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new InterfaceElement(ns, ScopeCategories.Public, false, false);
			list.Add(AttachName(sample, "ISample"));

			IDiscriminatedElement tmp = new InterfaceElement(sample, ScopeCategories.Public, false, false);
			AttachName(tmp, "IInner");
			list.Add(tmp);

			tmp = new StructElement(sample, ScopeCategories.Public, false, false);
			AttachName(tmp, "StructInner");
			list.Add(tmp);

			tmp = new ClassElement(sample, ScopeCategories.Public, false, false, false, false, false);
			AttachName(tmp, "ReferenceInner");
			list.Add(tmp);

			yield return (sample, list);
		}

		protected override IEnumerable<(InterfaceElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample()
		{
			var list = new List<IDiscriminatedElement>();

			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new InterfaceElement(ns, ScopeCategories.Public, false, false);
			var q = new QualifiedElement(sample);
			list.Add(q);
			list.Add(new IdentityElement(q, "ISample"));

			IDiscriminatedElement child =
				new ClassElement(sample, ScopeCategories.Public, false, false, false, false, false);
			q = new QualifiedElement(child);
			list.Add(child);
			list.Add(q);
			list.Add(new IdentityElement(q, "InnerClass"));

			child = new StructElement(sample, ScopeCategories.Public, false, false);
			q = new QualifiedElement(child);
			list.Add(child);
			list.Add(q);
			list.Add(new IdentityElement(q, "InnerStruct"));

			yield return (sample, list);


			child = new StructElement(child, ScopeCategories.Public, false, false);
			q = new QualifiedElement(child);
			list.Add(child);
			list.Add(q);
			list.Add(new IdentityElement(q, "InnerInner"));

			yield return (sample, list);
		}

		protected override IEnumerable<(InterfaceElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			IDiscriminatedElement parent = new NameSpace(new PhysicalStorage(PathA));
			AttachName(parent, "NameSpace");

			parent = new ClassElement(parent, ScopeCategories.Public, false, false, false, false, false);
			AttachName(parent, "Class");

			var sample = new InterfaceElement(parent, ScopeCategories.Public, false, false);
			AttachName(sample, "ISample");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected, IdentityCategories.Namespace, "NameSpace");
			_ = new IdentityElement(expected, IdentityCategories.Class, "Class");
			_ = new IdentityElement(expected, IdentityCategories.Interface, "ISample");


			yield return (sample, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected) =>
			actual.IsEquivalentTo(expected).IsTrue();

		protected override
			IEnumerable<(InterfaceElement sample, Stack<(IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new InterfaceElement(ns, ScopeCategories.Public, false, false);
			AttachName(sample, "ISample");

			var expected = new Stack<(IdentityCategories, string)>();
			expected.Push((IdentityCategories.Interface, "ISample"));

			yield return (sample, expected);
		}

		protected override
			IEnumerable<(InterfaceElement sample, IReadOnlyList<IDiscriminatedElement> expected,
				Action<InterfaceElement> registerAction)> GenerateRegisterChildSample()
		{
			var expected = new List<IDiscriminatedElement>();

			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new InterfaceElement(ns, ScopeCategories.Public, false, false);

			void act(InterfaceElement elem)
			{
				expected.Add(AttachName(elem, "ISample"));

				var child = new InterfaceElement(elem, ScopeCategories.Public, false, false);
				expected.Add(child);
				AttachName(child, "Child");
			}

			yield return (sample, expected, act);
		}

		protected override IEnumerable<(InterfaceElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = AttachName(ns, NameSpaceA);
			var errSample = new IdentityElement(q, "Err");

			var sample = new InterfaceElement(ns, ScopeCategories.Public, false, false);
			AttachName(sample, "Name");

			yield return (sample, errSample);
		}

		protected override IEnumerable<InterfaceElement> GenerateIdentityErrorGetterSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			yield return new InterfaceElement(ns, ScopeCategories.Public, false, false);
		}

		protected override bool TryGenerate(string path, string nameSpace, ScopeCategories scope, bool isAbstract,
			bool isSealed, bool isUnsafe,
			bool isPartial, bool isStatic, string identity,
			out (IPhysicalStorage expectedStorage, NameSpace expectedNameSpace, IQualified expectedIdentity,
				InterfaceElement
				sample) generated)
		{
			generated = default;
			if (!isAbstract || isSealed || isStatic) return false;

			var storage = new PhysicalStorage(path);
			var ns = new NameSpace(storage);
			AttachName(ns, nameSpace);

			var sample = new InterfaceElement(ns, scope, isUnsafe, isPartial);
			var q = AttachName(sample, identity);

			generated.sample = sample;
			generated.expectedIdentity = q;
			generated.expectedNameSpace = ns;
			generated.expectedStorage = storage;

			return true;
		}
	}
}