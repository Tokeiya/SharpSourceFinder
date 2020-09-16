using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;

namespace SharpSourceFinderCoreTests
{
	public class QualifiedNameTests : EquatabilityTester<QualifiedName>
	{
		private readonly SourceFile _rootA = new SourceFile(@"C:\Hoge\Piyo.cs");
		private readonly SourceFile _rootB = new SourceFile(@"G:\Foo\Bar.cs");


		[Fact]
		public void AddQualifiedNameTest()
		{
			var scr=new QualifiedName(_rootA);
			scr.Add("A");
			scr.Add("B");

			var actual = new QualifiedName(_rootB);
			actual.Add("AA");
			actual.Add("BB");
			actual.Add(scr);

			var expected = new QualifiedName(_rootB);
			expected.Add("AA");
			expected.Add("BB");
			expected.Add("A");
			expected.Add("B");


			actual.Is(expected);
		}

		[Fact]
		public void GetIdentitiesTest()
		{
			var name = new QualifiedName(_rootB);
			name.Add("System");
			name.Add("Collections");
			name.Add("Generic");

			var expected = new[]
			{
				new IdentityName(_rootB, "System"), new IdentityName(_rootB, "Collections"),
				new IdentityName(_rootB, "Generic")
			};

			var actual = name.GetIdentityies().ToArray();

			actual.Length.Is(expected.Length);

			for (int i = 0; i < expected.Length; i++)
			{
				actual[i].Is(expected[i]);
			}
		}



		[Fact]
		public void AddTest()
		{
			var names = new QualifiedName(_rootA);

			names.Add("System");
			names.Add("Collections");
			names.Add("Generics");


			var actual = names.Descendants().Cast<IdentityName>().ToArray();
			actual.Length.Is(3);

			actual[0].Identity.Is("System");
			actual[1].Identity.Is("Collections");
			actual[2].Identity.Is("Generics");
		}

		[Fact]
		public void DescribeTest()
		{
			var names = new QualifiedName(_rootA);

			names.Add("System");
			names.Add("Collections");
			names.Add("Generics");

			var bld = new StringBuilder();
			names.Describe(bld);
			bld.ToString().Is("System.Collections.Generics");
			names.Describe().Is("System.Collections.Generics");
		}


		protected override IEnumerable<(QualifiedName x, QualifiedName y, QualifiedName z)>
			CreateTransitivelyTestSamples()
		{
			var x = new QualifiedName(_rootA);
			var y = new QualifiedName(_rootB);
			var z = new QualifiedName(_rootA);

			x.Add("System");
			y.Add("System");
			z.Add("System");

			x.Add("Linq");
			y.Add("Linq");
			z.Add("Linq");

			yield return (x, y, z);
		}

		protected override IEnumerable<(QualifiedName x, QualifiedName y)> CreateReflexivelyTestSamples()
		{
			var x = new QualifiedName(_rootA);
			var y = new QualifiedName(_rootB);

			x.Add("System");
			y.Add("System");

			x.Add("IO");
			y.Add("IO");

			yield return (x, y);
		}

		protected override IEnumerable<QualifiedName> CreateSymmetricallyTestSamples()
		{
			var x = new QualifiedName(_rootA);
			x.Add("System");
			x.Add("Runtime");

			yield return x;
		}

		protected override IEnumerable<(QualifiedName x, QualifiedName y)> CreateInEqualTestSamples()
		{
			var x = new QualifiedName(_rootA);
			var y = new QualifiedName(_rootA);

			x.Add("System");
			y.Add("System");

			x.Add("Data");
			y.Add("Dynamic");

			yield return (x, y);
		}

		protected override IEnumerable<(object x, object y)> CreateObjectEqualSamples() =>
			CreateReflexivelyTestSamples().Select(tup => ((object) tup.x, (object) tup.y));

		protected override IEnumerable<(object x, object y)> CreateObjectInEqualSamples() =>
			CreateInEqualTestSamples().Select(tup => ((object) tup.x, (object) tup.y));
	}
}