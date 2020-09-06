using System;
using System.Collections.Generic;
using Tokeiya3.SharpSourceFinderCore;
using ChainingAssertion;
using System.Linq;
using System.Text;
using SharpSourceFinderCoreTests;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class QualifiedNameTests:EquatabilityTester<QualifiedName>
	{
		private readonly SourceFile _rootA = new SourceFile(@"C:\Hoge\Piyo.cs");
		private readonly SourceFile _rootB = new SourceFile(@"G:\Foo\Bar.cs");


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

		protected override IEnumerable<(object x, object y)> CreateObjectEqualSamples() => CreateReflexivelyTestSamples().Select(tup => ((object)tup.x, (object)tup.y));

		protected override IEnumerable<(object x, object y)> CreateObjectInEqualSamples() =>
			CreateInEqualTestSamples().Select(tup => ((object) tup.x, (object) tup.y));

	}
}