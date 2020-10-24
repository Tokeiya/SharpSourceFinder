using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class NameSpaceTest : NonTerminalElementTest<NameSpaceElement, IDiscriminatedElement>
	{
		private const string PathA = "C:\\Hoge\\Piyo.cs";
		private const string PathB = "D:\\Foo\\Bar.cs";


		public NameSpaceTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) =>
			ReferenceEquals(actual, expected).IsTrue();


		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();


		protected override IEnumerable<(NameSpaceElement x, NameSpaceElement y, NameSpaceElement z)>
			GenerateLogicallyTransitiveSample()
		{
			var storage = new PhysicalStorage(PathA);

			var x = new NameSpaceElement(storage);
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			var y = new NameSpaceElement(storage);
			q = new QualifiedElement(y);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");


			storage = new PhysicalStorage(PathB);

			var z = new NameSpaceElement(storage);
			q = new QualifiedElement(z);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			yield return (x, y, z);
		}

		protected override IEnumerable<(NameSpaceElement x, NameSpaceElement y)>
			GenerateLogicallyInEquivalentSample()
		{
			var storage = new PhysicalStorage(PathA);

			var x = new NameSpaceElement(storage);
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");

			var y = new NameSpaceElement(storage);
			_ = new IdentityElement(new QualifiedElement(y), "tokeiya3");

			yield return (x, y);
		}

		protected override IEnumerable<(NameSpaceElement x, NameSpaceElement y, NameSpaceElement z)>
			GeneratePhysicallyTransitiveSample()
		{
			var x = new NameSpaceElement(new PhysicalStorage(PathA));
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");


			var y = new NameSpaceElement(new PhysicalStorage(PathA));
			q = new QualifiedElement(y);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");


			var z = new NameSpaceElement(new PhysicalStorage(PathA));
			q = new QualifiedElement(z);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			yield return (x, y, z);
		}

		protected override IEnumerable<(NameSpaceElement x, NameSpaceElement y)>
			GeneratePhysicallyInEqualitySample()
		{
			var x = new NameSpaceElement(new PhysicalStorage(PathA));
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");

			var y = new NameSpaceElement(new PhysicalStorage(PathA));
			_ = new IdentityElement(new QualifiedElement(y), "tokeiya3");

			yield return (x, y);

			x = new NameSpaceElement(new PhysicalStorage(PathA));
			_ = new IdentityElement(new QualifiedElement(x), "Tokeiya3");

			y = new NameSpaceElement(new PhysicalStorage(PathB));
			_ = new IdentityElement(new QualifiedElement(y), "Tokeiya3");

			yield return (x, y);
		}

		protected override IEnumerable<(NameSpaceElement sample, IDiscriminatedElement expected)>
			GenerateParentSample()
		{
			var expected = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new NameSpaceElement(expected);

			yield return (sample, expected);
		}

		protected override IEnumerable<(NameSpaceElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample()
		{
			var storage = new PhysicalStorage(PathA);
			var expected = new NameSpaceElement(storage);
			var sample = new NameSpaceElement(expected);

			yield return (sample, storage);
		}

		protected override IEnumerable<(NameSpaceElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var a = new NameSpaceElement(new PhysicalStorage(PathA));
			var b = new NameSpaceElement(a);
			var c = new NameSpaceElement(b);

			yield return (c, new[] {b, a});
		}

		protected override IEnumerable<(NameSpaceElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var sample = new NameSpaceElement(new PhysicalStorage(PathA));
			var expected = new[] {new NameSpaceElement(sample), new NameSpaceElement(sample), new NameSpaceElement(sample)};

			yield return (sample, expected);
		}

		protected override IEnumerable<(NameSpaceElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample()
		{
			var expected = new List<IDiscriminatedElement>();

			var sample = new NameSpaceElement(new PhysicalStorage(PathA));
			var name = new QualifiedElement(sample);
			var elem = new IdentityElement(name, "Tokeiya3");

			expected.Add(name);
			expected.Add(elem);


			var a = new NameSpaceElement(sample);
			name = new QualifiedElement(a);
			elem = new IdentityElement(name, "SharpSourceFinder");

			expected.Add(a);
			expected.Add(name);
			expected.Add(elem);

			yield return (sample, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected)
		{
			actual.Identities.Count.Is(expected.Identities.Count);

			for (int i = 0; i < expected.Identities.Count; i++)
			{
				actual.Identities[i].Category.Is(expected.Identities[i].Category);
				actual.Identities[i].Name.Is(expected.Identities[i].Name);
			}
		}

		protected override IEnumerable<(NameSpaceElement sample, IQualified expected)>
			GenerateQualifiedNameSample()
		{
			var sample = new NameSpaceElement(new PhysicalStorage(PathA));
			var expected = new QualifiedElement(sample);

			yield return (sample, expected);
			_ = new IdentityElement(expected, "Foo");
			_ = new IdentityElement(expected, "Bar");

			yield return (sample, expected);


			sample = new NameSpaceElement(sample);
			expected = new QualifiedElement(sample);
			_ = new IdentityElement(expected, "Hoge");
			_ = new IdentityElement(expected, "Piyo");

			expected = sample.GetQualifiedName();

			expected.Identities.Count.Is(4);

			foreach (var elem in expected.Identities)
			{
				elem.Category.Is(IdentityCategories.Namespace);
				elem.From.Is(expected);
			}

			expected.Identities[0].Name.Is("Foo");
			expected.Identities[1].Name.Is("Bar");
			expected.Identities[2].Name.Is("Hoge");
			expected.Identities[3].Name.Is("Piyo");

			yield return (sample, expected);
		}


		protected override
			IEnumerable<(NameSpaceElement sample, Stack<(IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
			var sample = new NameSpaceElement();
			var nme = new QualifiedElement(sample);

			var expected = new Stack<(IdentityCategories category, string identity)>();

			yield return (sample, expected);

			_ = new IdentityElement(nme, "Hoge");
			_ = new IdentityElement(nme, "Piyo");

			expected.Push((IdentityCategories.Namespace, "Piyo"));
			expected.Push((IdentityCategories.Namespace, "Hoge"));

			yield return (sample, expected);

			sample = new NameSpaceElement(sample);
			nme = new QualifiedElement(sample);

			_ = new IdentityElement(nme, "Foo");
			_ = new IdentityElement(nme, "Bar");

			expected.Count.Is(0);

			expected.Push((IdentityCategories.Namespace, "Bar"));
			expected.Push((IdentityCategories.Namespace, "Foo"));

			yield return (sample, expected);
		}

		protected override
			IEnumerable<(NameSpaceElement sample, IReadOnlyList<IDiscriminatedElement> expected
				, Action<NameSpaceElement> registerAction)> GenerateRegisterChildSample()
		{
			var expected = new List<IDiscriminatedElement>();
			var sample = new NameSpaceElement(new PhysicalStorage(PathA));


			void act(NonTerminalElement<IDiscriminatedElement> scr)
			{
				expected.Add(new QualifiedElement(scr));
				expected.Add(new NameSpaceElement(scr));
			}

			yield return (sample, expected, act);
		}

		protected override
			IEnumerable<(NameSpaceElement sample, IDiscriminatedElement errSample)>
			GenerateErrSample()
		{
			var sample = new NameSpaceElement(new PhysicalStorage(PathA));

			var q = new QualifiedElement();

			yield return (sample, q);
		}
	}
}