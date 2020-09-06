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
		[Fact]
		public void AddTest()
		{
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new QualifiedName(root);

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
			var root = new MultiDescendantsElementTests.TestSample("root");
			var names = new QualifiedName(root);

			names.Add("System");
			names.Add("Collections");
			names.Add("Generics");

			var bld = new StringBuilder();
			names.Describe(bld);
			bld.ToString().Is("System.Collections.Generics");
			names.Describe().Is("System.Collections.Generics");
		}


		protected override IEnumerable<(QualifiedName x, QualifiedName y, QualifiedName z)> CreateTransitivelyTestSamples()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedName x, QualifiedName y)> CreateReflexivelyTestSamples()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<QualifiedName> CreateSymmetricallyTestSamples()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(QualifiedName x, QualifiedName y)> CreateInEqualTestSamples()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(object x, object y)> CreateObjectEqualSamples()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<(object x, object y)> CreateObjectInEqualSamples()
		{
			throw new NotImplementedException();
		}
	}
}