using System;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;
using static Xunit.Assert;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class PhysicalStorageTest
	{
		private const string SampleA = @"C:\Hoge\Piyo.cs";
		private const string SampleB = @"D:\Foo\Bar.cs";


		private readonly ITestOutputHelper _output;
		public PhysicalStorageTest(ITestOutputHelper output) => _output = output;

		[Trait("TestLayer", nameof(PhysicalStorage))]
		[Fact]
		public void CtorTest()
		{
			Throws<ArgumentException>(() => new PhysicalStorage(string.Empty));
			Throws<ArgumentException>(() => new PhysicalStorage(" "));
			Throws<ArgumentException>(() => new PhysicalStorage("\n"));
		}

		[Trait("TestLayer", nameof(PhysicalStorage))]
		[Fact]
		public void GetPathTest()
		{
			var sample = new PhysicalStorage(SampleA);
			sample.Path.Is(SampleA);

			sample = new PhysicalStorage(SampleB);
			sample.Path.Is(SampleB);
		}

		[Trait("TestLayer", nameof(PhysicalStorage))]
		[Fact]
		public void SymmetricallyTest()
		{
			var sample = new PhysicalStorage(SampleA);
			sample.IsEquivalentTo(sample);

			sample = new PhysicalStorage(SampleB);
			sample.IsEquivalentTo(sample).IsTrue();
		}

		[Trait("TestLayer", nameof(PhysicalStorage))]
		[Fact]
		public void TransitivelyTest()
		{
			var x = new PhysicalStorage(SampleA);
			var y = new PhysicalStorage(SampleA);
			var z = new PhysicalStorage(SampleA);

			x.IsEquivalentTo(y).IsTrue();
			y.IsEquivalentTo(z).IsTrue();
			x.IsEquivalentTo(z).IsTrue();

			x = new PhysicalStorage(SampleB);
			y = new PhysicalStorage(SampleB);
			z = new PhysicalStorage(SampleB);

			x.IsEquivalentTo(y).IsTrue();
			y.IsEquivalentTo(z).IsTrue();
			x.IsEquivalentTo(z).IsTrue();
		}

		[Trait("TestLayer", nameof(PhysicalStorage))]
		[Fact]
		public void ReflexivelyTest()
		{
			var x = new PhysicalStorage(SampleA);
			var y = new PhysicalStorage(SampleA);

			x.IsEquivalentTo(y).IsTrue();
			y.IsEquivalentTo(x).IsTrue();

			x = new PhysicalStorage(SampleB);
			y = new PhysicalStorage(SampleB);

			x.IsEquivalentTo(y).IsTrue();
			y.IsEquivalentTo(x).IsTrue();
		}


		[Trait("TestLayer", nameof(PhysicalStorage))]
		[Fact]
		public void InEquivalentTest()
		{
			var x = new PhysicalStorage(SampleA);
			var y = new PhysicalStorage(SampleB);

			x.IsEquivalentTo(y).IsFalse();
			y.IsEquivalentTo(x).IsFalse();
		}
	}
}