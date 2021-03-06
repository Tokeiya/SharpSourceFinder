using System;
using System.Collections.Generic;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class QualifiedElementIDiscriminatedElementTest : NonTerminalElementTest<QualifiedElement, IdentityElement>
	{
		private const string PathA = @"C:\Hoge\Piyo.cs";
		private const string PathB = @"D:\Foo\Bar.cs";

		public QualifiedElementIDiscriminatedElementTest(ITestOutputHelper output) : base(output)
		{
		}


		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();


		protected override IEnumerable<(QualifiedElement x, QualifiedElement y, QualifiedElement z)>
			GenerateLogicallyTransitiveSample()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();
			var z = new QualifiedElement();

			_ = new IdentityElement(x,ScopeCategories.Public, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y, ScopeCategories.Public,IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(z, ScopeCategories.Public,IdentityCategories.Namespace, "Hoge");

			yield return (x, y, z);

			x = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));
			y = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathB)));
			z = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));

			_ = new IdentityElement(x, "Foo");
			_ = new IdentityElement(y, "Foo");
			_ = new IdentityElement(z, "Foo");

			yield return (x, y, z);
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y)> GenerateLogicallyInEquivalentSample()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();

			_ = new IdentityElement(x,ScopeCategories.Public, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y, ScopeCategories.Public, IdentityCategories.Class, "Hoge");
			yield return (x, y);

			x = new QualifiedElement();
			y = new QualifiedElement();

			_ = new IdentityElement(x,ScopeCategories.Public, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y,ScopeCategories.Public, IdentityCategories.Namespace, "hoge");
			yield return (x, y);


			x = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));
			y = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));

			_ = new IdentityElement(x, "Hoge");
			_ = new IdentityElement(y, "Piyo");
			yield return (x, y);

			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			x = new QualifiedElement(new NameSpaceElement(ns));
			_ = new IdentityElement(x, "Bar");

			y = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));
			_ = new IdentityElement(y, "Bar");

			yield return (x, y);
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y, QualifiedElement z)>
			GeneratePhysicallyTransitiveSample()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();
			var z = new QualifiedElement();

			_ = new IdentityElement(x,ScopeCategories.Public, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y,ScopeCategories.Public, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(z,ScopeCategories.Public, IdentityCategories.Namespace, "Hoge");

			yield return (x, y, z);

			x = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));
			y = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));
			z = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));

			_ = new IdentityElement(x, "Foo");
			_ = new IdentityElement(y, "Foo");
			_ = new IdentityElement(z, "Foo");

			yield return (x, y, z);
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y)> GeneratePhysicallyInEqualitySample()
		{
			var x = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));
			var y = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathB)));
			yield return (x, y);
		}

		protected override IEnumerable<(QualifiedElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var expected = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new QualifiedElement(expected);

			yield return (sample, expected);
		}

		protected override IEnumerable<(QualifiedElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample()
		{
			var expected = new PhysicalStorage(PathA);
			var ns = new NameSpaceElement(expected);
			var qa = new QualifiedElement(ns);

			yield return (qa, expected);

			ns = new NameSpaceElement(ns);
			qa = new QualifiedElement(ns);

			yield return (qa, expected);
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var sample = new QualifiedElement(ns);

			yield return (sample, new[] {ns});

			var nsa = new NameSpaceElement(ns);
			sample = new QualifiedElement(nsa);

			yield return (sample, new[] {nsa, ns});
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var sample = new QualifiedElement();
			var expected = new[]
			{
				new IdentityElement(sample,ScopeCategories.Public, IdentityCategories.Namespace, "Foo"),
				new IdentityElement(sample,ScopeCategories.Public, IdentityCategories.Namespace, "Bar")
			};

			yield return (sample, expected);
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample() => GenerateChildrenSample();

		protected override IEnumerable<(QualifiedElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var sample = new QualifiedElement();
			_ = new IdentityElement(sample,ScopeCategories.Public, IdentityCategories.Namespace, "Foo");
			_ = new IdentityElement(sample,ScopeCategories.Public, IdentityCategories.Namespace, "Bar");

			yield return (sample, sample);

			var ns = new NameSpaceElement(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Upper");

			ns = new NameSpaceElement(ns);
			q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Bottom");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected,ScopeCategories.Public, IdentityCategories.Namespace, "Upper");
			_ = new IdentityElement(expected,ScopeCategories.Public, IdentityCategories.Namespace, "Bottom");

			yield return (q, expected);
		}

		protected override void AreEqual(IQualified actual, IQualified expected)
		{
			actual.Identities.Count.Is(expected.Identities.Count);

			for (int i = 0; i < expected.Identities.Count; i++)
			{
				var a = actual.Identities[i];
				var e = actual.Identities[i];

				a.Category.Is(e.Category);
				a.Name.Is(e.Name);
				a.Order.Is(e.Order);
			}
		}

		protected override
			IEnumerable<(QualifiedElement sample, Stack<(ScopeCategories scope, IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
			var sample = new QualifiedElement();
			_ = new IdentityElement(sample,ScopeCategories.Public, IdentityCategories.Namespace, "NameSpace");
			_ = new IdentityElement(sample,ScopeCategories.Internal, IdentityCategories.Class, "InternalClass");

			var expected = new Stack<(ScopeCategories,IdentityCategories, string)>();
			expected.Push((ScopeCategories.Internal,IdentityCategories.Class, "InternalClass"));
			expected.Push((ScopeCategories.Public,IdentityCategories.Namespace, "NameSpace"));

			yield return (sample, expected);

			expected.Clear();

			sample = new QualifiedElement(new NameSpaceElement(new PhysicalStorage(PathA)));
			_ = new IdentityElement(sample, "No1");
			_ = new IdentityElement(sample, "No2");


			expected.Push((ScopeCategories.Public,IdentityCategories.Namespace, "No2"));
			expected.Push((ScopeCategories.Public,IdentityCategories.Namespace, "No1"));

			yield return (sample, expected);
		}

		protected override
			IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected,
				Action<QualifiedElement> registerAction)> GenerateRegisterChildSample()
		{
			var sample = new QualifiedElement();
			var expected = new List<IDiscriminatedElement>();

			void act(QualifiedElement parent)
			{
				expected.Add(new IdentityElement(parent,ScopeCategories.Public, IdentityCategories.Namespace, "Ns"));
				expected.Add(new IdentityElement(parent, ScopeCategories.Public,IdentityCategories.Class, "Cls"));
			}

			yield return (sample, expected, act);
		}

		protected override IEnumerable<(QualifiedElement sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
			var sample = new QualifiedElement();
			IDiscriminatedElement err =
				new IdentityElement(new QualifiedElement(),ScopeCategories.Public, IdentityCategories.Namespace, "Err");
			yield return (sample, err);

			err = new NameSpaceElement(new PhysicalStorage(PathA));
			yield return (sample, err);
		}
	}
}