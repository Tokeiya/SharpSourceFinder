using System;
using System.Collections.Generic;
using System.Reflection;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests
{
	public class NameSpaceTest:NonTerminalElementTest<IDiscriminatedElement>
	{
		private readonly PhysicalStorage _storageA = new PhysicalStorage("C:\\Hoge\\Piyo.cs");
		private readonly PhysicalStorage _storageB = new PhysicalStorage(@"D:\Foo\Bar.cs");

		public NameSpaceTest(ITestOutputHelper output) :base(output){}

		protected override void AreEqual(IDiscriminatedElement actual, IDiscriminatedElement expected) => ReferenceEquals(actual, expected).IsTrue();


		protected override void AreEqual(IPhysicalStorage actual, IPhysicalStorage expected) =>
			ReferenceEquals(actual, expected).IsTrue();

		protected override IEnumerable<(DiscriminatedElement sample, IPhysicalStorage expected)>
			GenerateStorageGetTestSamples()
		{
			var s = new NameSpace(_storageA);
			yield return (s, _storageA);

			var ss = new NameSpace(s);
			yield return (ss, _storageA);

			yield return (new NameSpace(StorageNotAvailable.NotAvailable), StorageNotAvailable.NotAvailable);

		}

		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)>
			GenerateLogicallyTransitiveSample()
		{
			var x = new NameSpace(_storageA);
			var q = new QualifiedElement(x);
			_=new IdentityElement(q, "Tokeiya3");
			_=new IdentityElement(q, "SharpSourceFinder");

			var y = new NameSpace(_storageA);
			q = new QualifiedElement(y);
			_=new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");
			

			var z = new NameSpace(_storageB);
			q = new QualifiedElement(z);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");
			
			yield return (x, y, z);
		}

		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)>
			GenerateLogicallyInEquivalentSample()
		{
			var x = new NameSpace(_storageA);
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");

			var y = new NameSpace(_storageA);
			_ = new IdentityElement(new QualifiedElement(y), "tokeiya3");

			yield return (x, y);


		}

		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y, IDiscriminatedElement z)> GeneratePhysicallyTransitiveSample()
		{
			var x = new NameSpace(_storageA);
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			var y = new NameSpace(_storageA);
			q = new QualifiedElement(y);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");


			var z = new NameSpace(_storageA);
			q = new QualifiedElement(z);
			_ = new IdentityElement(q, "Tokeiya3");
			_ = new IdentityElement(q, "SharpSourceFinder");

			yield return (x, y, z);
		}

		protected override IEnumerable<(IDiscriminatedElement x, IDiscriminatedElement y)> GeneratePhysicallyInEqualitySample()
		{
			var x = new NameSpace(_storageA);
			var q = new QualifiedElement(x);
			_ = new IdentityElement(q, "Tokeiya3");

			var y = new NameSpace(_storageA);
			_ = new IdentityElement(new QualifiedElement(y), "tokeiya3");

			yield return (x, y);

			x = new NameSpace(_storageA);
			_ = new IdentityElement(new QualifiedElement(x), "Tokeiya3");

			y = new NameSpace(_storageB);
			_ = new IdentityElement(new QualifiedElement(y), "Tokeiya3");

			yield return (x, y);
		}

		protected override IEnumerable<(IDiscriminatedElement sample, IDiscriminatedElement expected)> GenerateParentSample()
		{
			var expected = new NameSpace(_storageA);
			var sample = new NameSpace(expected);

			yield return (sample, expected);
		}

		protected override IEnumerable<(IDiscriminatedElement sample, IPhysicalStorage expected)> GeneratePhysicalStorageSample()
		{
			var expected = new NameSpace(_storageA);
			var sample = new NameSpace(expected);

			yield return (sample, _storageA);

		}

		protected override IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)>
			GenerateGetAncestorsSample(bool isContainSelf)
		{
			var a = new NameSpace(_storageA);
			var b = new NameSpace(a);
			var c = new NameSpace(b);

			yield return (c, new[] { a, b });

		}

		protected override IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateChildrenSample()
		{
			var sample = new NameSpace(_storageA);
			var expected = new[] {new NameSpace(sample), new NameSpace(sample), new NameSpace(sample)};

			yield return (sample, expected);
		}

		protected override IEnumerable<(IDiscriminatedElement sample, IReadOnlyList<IDiscriminatedElement> expected)> GenerateDescendantsSample(bool isContainSelf)
		{
			var expected = new List<IDiscriminatedElement>();

			var sample = new NameSpace(_storageA);
			var name = new QualifiedElement(sample);
			var elem = new IdentityElement(name,"Tokeiya3");

			expected.Add(name);
			expected.Add(elem);


			var a=new NameSpace(sample);
			name = new QualifiedElement(a);
			elem = new IdentityElement(name,"SharpSourceFinder");

			expected.Add(a);
			expected.Add(name);
			expected.Add(elem);

			yield return (sample, expected);

		}

		protected override IEnumerable<(IDiscriminatedElement sample, QualifiedElement expected)> GenerateQualifiedNameSample()
		{

			throw new System.NotImplementedException();
		}

		protected override IEnumerable<(IDiscriminatedElement sample, Stack<(IdentityCategories category, string identity)> expected)> GenerateAggregateIdentitiesSample()
		{
			throw new System.NotImplementedException();
		}

		protected override IEnumerable<(NonTerminalElement<IDiscriminatedElement> sample, IReadOnlyList<IDiscriminatedElement> expected,Action<NonTerminalElement<IDiscriminatedElement>> registerAction)> GenerateRegisterChildSample()
		{
#warning GenerateRegisterChildSample_Is_NotImpl
			throw new NotImplementedException("GenerateRegisterChildSample is not implemented");
		}

		protected override IEnumerable<(NonTerminalElement<IDiscriminatedElement> sample, IDiscriminatedElement errSample)> GenerateErrSample()
		{
#warning GenerateErrSample_Is_NotImpl
			throw new NotImplementedException("GenerateErrSample is not implemented");
		}
	}
}
