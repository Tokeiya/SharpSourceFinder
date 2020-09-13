using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;

namespace SharpSourceFinderCoreTests
{
	public class NameSpaceTests : EquatabilityTester<NameSpace>
	{
		private const string SamplePath = "G:\\Hoge\\Piyo.cs";
		static SourceFile CreateStandardScr() => new SourceFile(SamplePath);


		[Fact]
		public void NameTest()
		{
			var scr = CreateStandardScr();
			var tokeiya = new NameSpace(scr);
			tokeiya.Name.Add("Tokeiya3");

			var expected = new QualifiedName(tokeiya);
			expected.Add("Tokeiya3");


			var unexpected = new QualifiedName(tokeiya);
			unexpected.Add("UnExpected");


			tokeiya.Name.Is(expected);
			tokeiya.Name.IsNot(unexpected);
		}

		[Fact]
		public void DescribeTest()
		{
			const string expectedInnerDescription = "namespace Inner\n{\n}\n";

			const string expectedOuterDescription = "namespace Outer.Outer\n{\nnamespace Inner\n{\n}\n}\n";

			var scr = CreateStandardScr();
			var outer = new NameSpace(scr);
			outer.Name.Add("Outer");
			outer.Name.Add("Outer");


			var inner = new NameSpace(outer);

			inner.Name.Add("Inner");


			outer.Describe().Is(expectedOuterDescription);
			inner.Describe().Is(expectedInnerDescription);
		}

		[Fact]
		public void GetFullQualifiedName()
		{
			var scr = CreateStandardScr();
			var outer = new NameSpace(scr);
			outer.Name.Add("Outer");
			outer.Name.Add("Outer");

			var expected = new QualifiedName(scr);
			expected.Add("Outer");
			expected.Add("Outer");

			outer.GetFullQualifiedName().Is(expected);

			var inner = new NameSpace(outer);
			inner.Name.Add("Inner");


			expected.Add("Inner");

			inner.GetFullQualifiedName().Is(expected);
		}


		protected override IEnumerable<(NameSpace x, NameSpace y, NameSpace z)> CreateTransitivelyTestSamples()
		{
			var rootA = new SourceFile("C:\\Foo\\Bar.cs");
			var rootB = new SourceFile(@"G:\Hoge\Piyo.cs");

			var x = new NameSpace(rootA);
			var name = new QualifiedName(x);
			name.Add("System");
			name.Add("Collections");

			var y = new NameSpace(rootB);
			name = new QualifiedName(y);
			name.Add("System");
			name.Add("Collections");

			var z = new NameSpace(rootA);
			name = new QualifiedName(z);
			name.Add("System");
			name.Add("Collections");

			yield return (x, y, z);
			name.Add("Generic");

			x = new NameSpace(rootA);
			name = new QualifiedName(x);
			name.Add("System");

			var child = new NameSpace(x);
			name = new QualifiedName(child);
			name.Add("Collections");
			name.Add("Generic");

			y = new NameSpace(rootB);
			name = new QualifiedName(y);
			name.Add("System");
			name.Add("Collections");

			child = new NameSpace(y);
			name = new QualifiedName(child);
			name.Add("Generic");


			yield return (x, y, z);
		}

		protected override IEnumerable<(NameSpace x, NameSpace y)> CreateReflexivelyTestSamples()
		{
			foreach (var (x, y, z) in CreateTransitivelyTestSamples())
			{
				yield return (x, y);
				yield return (y, z);
				yield return (z, x);
			}
		}

		protected override IEnumerable<NameSpace> CreateSymmetricallyTestSamples()
		{
			foreach (var (x, y, z) in CreateTransitivelyTestSamples())
			{
				yield return x;
				yield return y;
				yield return z;
			}
		}

		protected override IEnumerable<(NameSpace x, NameSpace y)> CreateInEqualTestSamples()
		{
			var root = new SourceFile(SamplePath);
			var x = new NameSpace(root);

			x.Name.Add("System");
			x.Name.Add("Collections");
			x.Name.Add("Generics");


			var y = new NameSpace(root);

			y.Name.Add("system");
			y.Name.Add("collections");
			y.Name.Add("generics");

			yield return (x, y);


			x = new NameSpace(root);
			x.Name.Add("System");
			x.Name.Add("Collections");

			var child = new NameSpace(x);
			child.Name.Add("generics");

			yield return (x, y);
		}

		protected override IEnumerable<(object x, object y)> CreateObjectEqualSamples() =>
			CreateReflexivelyTestSamples().Select(tup => ((object) tup.x, (object) tup.y));

		protected override IEnumerable<(object x, object y)> CreateObjectInEqualSamples()
			=> CreateInEqualTestSamples().Select(tup => ((object) tup.x, (object) tup.y));
	}
}