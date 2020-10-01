using ChainingAssertion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class NameSpaceTest : NonTerminalElementTest<IDiscriminatedElement>
	{
		private const string PathA = "C:\\Hoge\\Piyo.cs";
		private const string PathB = "D:\\Foo\\Bar.cs";


		public NameSpaceTest(ITestOutputHelper output) : base(output) { }

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) => ReferenceEquals(actual, expected).IsTrue();


		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override IEnumerable<(DiscriminatedElement sample, IPhysicalStorage expected)>
			GenerateStorageGetTestSamples()
		{
			var storage = new PhysicalStorage(PathA);

			var s = new NameSpace(storage);
			yield return (s, storage);

			var ss = new NameSpace(s);
			yield return (ss, storage);

			yield return (new NameSpace(StorageNotAvailable.NotAvailable), StorageNotAvailable.NotAvailable);

		}


		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)>
			GenerateLogicallyTransitiveSample()
		{
			var storage = new PhysicalStorage(PathA);

			var x = new NameSpace(storage);
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			var y = new NameSpace(storage);
			q = new QualifiedElement(y);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");


			storage = new PhysicalStorage(PathB);

			var z = new NameSpace(storage);
			q = new QualifiedElement(z);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			yield return (x, y, z);
		}

		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
			GenerateLogicallyInEquivalentSample()
		{
			var storage = new PhysicalStorage(PathA);

			var x = new NameSpace(storage);
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");

			var y = new NameSpace(storage);
			_ = new IdentityElement(new QualifiedElement(y), "tokeiya3");

			yield return (x, y);


		}

		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)> GeneratePhysicallyTransitiveSample()
		{
			var x = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");


			var y = new NameSpace(new PhysicalStorage(PathA));
			q = new QualifiedElement(y);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");


			var z = new NameSpace(new PhysicalStorage(PathA));
			q = new QualifiedElement(z);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			yield return (x, y, z);
		}

		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)> GeneratePhysicallyInEqualitySample()
		{
			var x = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");

			var y = new NameSpace(new PhysicalStorage(PathA));
			_ = new IdentityElement(new QualifiedElement(y), "tokeiya3");

			yield return (x, y);

			x = new NameSpace(new PhysicalStorage(PathA));
			_ = new IdentityElement(new QualifiedElement(x), "Tokeiya3");

			y = new NameSpace(new PhysicalStorage(PathB));
			_ = new IdentityElement(new QualifiedElement(y), "Tokeiya3");

			yield return (x, y);
		}

		protected override IEnumerable<(IDiscriminatedElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var expected = new NameSpace(new PhysicalStorage(PathA));
			var sample = new NameSpace(expected);

			yield return (sample, expected);
		}

		protected override IEnumerable<(IDiscriminatedElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
			var storage = new PhysicalStorage(PathA);
			var expected = new NameSpace(storage);
			var sample = new NameSpace(expected);

			yield return (sample, storage);

		}

		protected override IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample(bool isContainSelf)
		{
			var a = new NameSpace(new PhysicalStorage(PathA));
			var b = new NameSpace(a);
			var c = new NameSpace(b);

			yield return (c, new[] { a, b });

		}

		protected override IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateChildrenSample()
		{
			var sample = new NameSpace(new PhysicalStorage(PathA));
			var expected = new[] { new NameSpace(sample), new NameSpace(sample), new NameSpace(sample) };

			yield return (sample, expected);
		}

		protected override IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateDescendantsSample(bool isContainSelf)
		{
			var expected = new List<IDiscriminatedElement>();

			var sample = new NameSpace(new PhysicalStorage(PathA));
			var name = new QualifiedElement(sample);
			var elem = new IdentityElement(name, "Tokeiya3");

			expected.Add(name);
			expected.Add(elem);


			var a = new NameSpace(sample);
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

		protected override IEnumerable<(IDiscriminatedElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var sample = new NameSpace(new PhysicalStorage(PathA));
			var expected = new QualifiedElement(sample);

			yield return (sample, expected);
			_ = new IdentityElement(expected, "Foo");
			_ = new IdentityElement(expected, "Bar");

			yield return (sample, expected);


			sample = new NameSpace(sample);
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


		protected override IEnumerable<(IDiscriminatedElement sample, Stack<(IdentityCategories category, string identity)> expected)> GenerateAggregateIdentitiesSample()
		{
			var sample=new NameSpace();
			var nme = new QualifiedElement(sample);

			var expected=new Stack<(IdentityCategories category,string identity)>();

			yield return (sample, expected);

			_ = new IdentityElement(nme, "Hoge");
			_ = new IdentityElement(nme, "Piyo");

			expected.Push((IdentityCategories.Namespace, "Piyo"));
			expected.Push((IdentityCategories.Namespace, "Hoge"));

			yield return (sample, expected);

			sample = new NameSpace(sample);
			nme = new QualifiedElement(sample);

			_ = new IdentityElement(nme, "Foo");
			_ = new IdentityElement(nme, "Bar");

			expected.Count.Is(0);

			expected.Push((IdentityCategories.Namespace, "Bar"));
			expected.Push((IdentityCategories.Namespace, "Foo"));
			expected.Push((IdentityCategories.Namespace, "Piyo"));
			expected.Push((IdentityCategories.Namespace, "Hoge"));

			yield return (sample, expected);

		}

		protected override IEnumerable<(NonTerminalElement<IDiscriminatedElement> sample, IReadOnlyList<IDiscriminatedElement> expected, Action<NonTerminalElement<IDiscriminatedElement>> registerAction)> GenerateRegisterChildSample()
		{
			var expected = new List<IDiscriminatedElement>();
			var sample = new NameSpace(new PhysicalStorage(PathA));


			void act(NonTerminalElement<IDiscriminatedElement> scr)
			{
				expected.Add(new QualifiedElement(scr));
				expected.Add(new NameSpace(scr));
			}

			yield return (sample, expected, act);

		}

		protected override
			IEnumerable<(NonTerminalElement<IDiscriminatedElement> sample, IDiscriminatedElement errSample)>
			GenerateErrSample()
		{
#warning GenerateErrSample_Is_NotImpl
			throw new NotImplementedException("GenerateErrSample is not implemented");

		}

	}
}
