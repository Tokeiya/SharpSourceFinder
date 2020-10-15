using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class EnumElementTest:TypedElementTest<EnumElement>
	{

		public EnumElementTest(ITestOutputHelper output):base(output )
		{
		}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) => actual.IsSameReferenceAs(expected);

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			actual.IsSameReferenceAs(expected);

		static (QualifiedElement,IReadOnlyList<IdentityElement>) AttachName(IDiscriminatedElement element, params string[] names)
		{
			var q = new QualifiedElement(element);
			var id = names.Select(n => new IdentityElement(q, n)).ToArray();
			return (q, id);

		}
		protected override IEnumerable<(EnumElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			
			var sample = new EnumElement(ns, ScopeCategories.Public);

			yield return (sample, ns);

		}

		protected override IEnumerable<(EnumElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
			var expected = new PhysicalStorage(PathA);
			var sample = new EnumElement(new NameSpace(expected), ScopeCategories.Public);

			yield return (sample, expected);
		}

		protected override IEnumerable<(EnumElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateGetAncestorsSample()
		{
			var list = new List<IDiscriminatedElement>();
			IDiscriminatedElement parent = new NameSpace(new PhysicalStorage(PathA));
			AttachName(parent, NameSpaceA);
			list.Add(parent);

			parent = new ClassElement(parent, ScopeCategories.Public, false, false, false, false, false);
			AttachName(parent, "Envelope");
			list.Add(parent);

			var sample = new EnumElement(parent, ScopeCategories.Public);
			AttachName(sample, "Sample");

			list.Reverse();

			yield return (sample, list);

		}

		[Trait("TestLayer", nameof(EnumElement))]
		[Fact]
		public void NoAddTest()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new EnumElement(ns, ScopeCategories.Public);

			Assert.Throws<ArgumentException>(() => _ = new EnumElement(sample, ScopeCategories.Public));

		}



		protected override IEnumerable<(EnumElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new EnumElement(ns, ScopeCategories.Public);
			AttachName(sample, "Sample");

			yield return (sample, Array.Empty<IDiscriminatedElement>());
		}

		protected override IEnumerable<(EnumElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateDescendantsSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new EnumElement(ns, ScopeCategories.Public);
			AttachName(sample, "Sample");

			yield return (sample, Array.Empty<IDiscriminatedElement>());
		}

		protected override IEnumerable<(EnumElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new EnumElement(ns, ScopeCategories.Public); 
			AttachName(sample, "Sample");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected,IdentityCategories.Namespace, NameSpaceA);
			_ = new IdentityElement(expected, IdentityCategories.Enum, "Sample");

			yield return (sample, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected) => actual.IsEquivalentTo(expected).IsTrue();


		protected override IEnumerable<(EnumElement sample, Stack<(IdentityCategories category, string identity)> expected)> GenerateAggregateIdentitiesSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new EnumElement(ns, ScopeCategories.Public);
			AttachName(sample, "Sample");

			var stack = new Stack<(IdentityCategories, string)>();
			stack.Push((IdentityCategories.Enum, "Sample"));

			yield return (sample, stack);
		}

		protected override IEnumerable<(EnumElement sample, IReadOnlyList<IDiscriminatedElement> expected, Action<EnumElement> registerAction)> GenerateRegisterChildSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new EnumElement(ns, ScopeCategories.Public);
			AttachName(sample, "Sample");

			yield return (sample, Array.Empty<IDiscriminatedElement>(), (dmy) => { });
		}

		protected override IEnumerable<(EnumElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, NameSpaceA);

			var sample = new EnumElement(ns, ScopeCategories.Public);
			AttachName(sample, "Sample");

			yield return (sample, ns);
		}

		protected override IEnumerable<EnumElement> GenerateIdentityErrorGetterSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			AttachName(ns, "Sample");

			yield return new EnumElement(ns, ScopeCategories.Public);
		}

		protected override bool TryGenerate(string path, string nameSpace, ScopeCategories scope, bool isAbstract, bool isSealed, bool isUnsafe,
			bool isPartial, bool isStatic, string identity,
			out (IPhysicalStorage expectedStorage, NameSpace expectedNameSpace, IQualified expectedIdentity, EnumElement sample)
				generated)
		{
			generated = default;

			if (isAbstract || !isSealed || isUnsafe || isPartial || isStatic) return false;

			var storage = new PhysicalStorage(path);
			var ns = new NameSpace(storage);
			AttachName(ns, nameSpace);

			var sample = new EnumElement(ns, scope);
			var (q,_)=AttachName(sample, identity);


			generated.sample = sample;
			generated.expectedNameSpace = ns;
			generated.expectedIdentity = q;
			generated.expectedStorage = storage;

			return true;
		}
	}
}
