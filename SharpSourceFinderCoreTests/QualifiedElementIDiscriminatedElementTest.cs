using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
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

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(z, IdentityCategories.Namespace, "Hoge");

			yield return (x, y, z);

			x = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));
			y = new QualifiedElement(new NameSpace(new PhysicalStorage(PathB)));
			z = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));

			_ = new IdentityElement(x, "Foo");
			_ = new IdentityElement(y, "Foo");
			_ = new IdentityElement(z, "Foo");

			yield return (x, y, z);
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y)> GenerateLogicallyInEquivalentSample()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y, IdentityCategories.Class, "Hoge");
			yield return (x, y);

			x = new QualifiedElement();
			y = new QualifiedElement();

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y, IdentityCategories.Namespace, "hoge");
			yield return (x, y);


			x = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));
			y = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));

			_ = new IdentityElement(x, "Hoge");
			_ = new IdentityElement(y, "Piyo");
			yield return (x, y);

			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Foo");

			x = new QualifiedElement(new NameSpace(ns));
			_ = new IdentityElement(x, "Bar");

			y = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));
			_ = new IdentityElement(y, "Bar");

			yield return (x, y);
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y, QualifiedElement z)>
			GeneratePhysicallyTransitiveSample()
		{
			var x = new QualifiedElement();
			var y = new QualifiedElement();
			var z = new QualifiedElement();

			_ = new IdentityElement(x, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(y, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(z, IdentityCategories.Namespace, "Hoge");

			yield return (x, y, z);

			x = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));
			y = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));
			z = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));

			_ = new IdentityElement(x, "Foo");
			_ = new IdentityElement(y, "Foo");
			_ = new IdentityElement(z, "Foo");

			yield return (x, y, z);
		}

		protected override IEnumerable<(QualifiedElement x, QualifiedElement y)> GeneratePhysicallyInEqualitySample()
		{
			var x = new QualifiedElement(new NameSpace(new PhysicalStorage(PathA)));
			var y = new QualifiedElement(new NameSpace(new PhysicalStorage(PathB)));
			yield return (x, y);
		}

		protected override IEnumerable<(QualifiedElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var expected = new NameSpace(new PhysicalStorage(PathA));
			var sample = new QualifiedElement(expected);

			yield return (sample, expected);
		}

		protected override IEnumerable<(QualifiedElement sample, IPhysicalStorage expected)>
			GeneratePhysicalStorageSample()
		{
			var expected = new PhysicalStorage(PathA);
			var ns = new NameSpace(expected);
			var qa = new QualifiedElement(ns);

			yield return (qa, expected);

			ns = new NameSpace(ns);
			qa = new QualifiedElement(ns);

			yield return (qa, expected);
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample()
		{
			var ns = new NameSpace(new PhysicalStorage(PathA));
			var sample = new QualifiedElement(ns);

			yield return (sample, new[] {ns});

			var nsa = new NameSpace(ns);
			sample = new QualifiedElement(nsa);

			yield return (sample, new[] {nsa, ns});
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateChildrenSample()
		{
			var sample = new QualifiedElement();
			var expected = new[]
			{
				new IdentityElement(sample, IdentityCategories.Namespace, "Foo"),
				new IdentityElement(sample, IdentityCategories.Namespace, "Bar")
			};

			yield return (sample, expected);
		}

		protected override IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateDescendantsSample() => GenerateChildrenSample();

		protected override IEnumerable<(QualifiedElement sample, IQualified expected)> GenerateQualifiedNameSample()
		{
			var sample = new QualifiedElement();
			_ = new IdentityElement(sample, IdentityCategories.Namespace, "Foo");
			_ = new IdentityElement(sample, IdentityCategories.Namespace, "Bar");

			yield return (sample, sample);

			var ns = new NameSpace(new PhysicalStorage(PathA));
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Upper");

			ns = new NameSpace(ns);
			q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Bottom");

			var expected = new QualifiedElement();
			_ = new IdentityElement(expected, IdentityCategories.Namespace, "Upper");
			_ = new IdentityElement(expected, IdentityCategories.Namespace, "Bottom");

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
			IEnumerable<(QualifiedElement sample, Stack<(IdentityCategories category, string identity)> expected)>
			GenerateAggregateIdentitiesSample()
		{
#warning GenerateAggregateIdentitiesSample_Is_NotImpl
			throw new NotImplementedException("GenerateAggregateIdentitiesSample is not implemented");
			throw new NotImplementedException();
		}

		protected override
			IEnumerable<(QualifiedElement sample, IReadOnlyList<IDiscriminatedElement> expected,
				Action<QualifiedElement> registerAction)> GenerateRegisterChildSample()
		{
#warning GenerateRegisterChildSample_Is_NotImpl
			throw new NotImplementedException("GenerateRegisterChildSample is not implemented");
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedElement sample, IdentityElement errSample)> GenerateErrSample()
		{
#warning GenerateErrSample_Is_NotImpl
			throw new NotImplementedException("GenerateErrSample is not implemented");
			throw new NotImplementedException();
		}
	}
}