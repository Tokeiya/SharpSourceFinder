using Tokeiya3.SharpSourceFinderCore;
using ChainingAssertion;
using System;
using System.Linq;
using System.Text;
using Xunit;
using static SharpSourceFinderCoreTests.EquivalentTestHelper<Tokeiya3.SharpSourceFinderCore.IdentityName>;


namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class IdentityNameTests
	{
		const string SamplePath = @"G:\Hoge\Moge.cs";
		const string AnotherPath = @"G:\Foo\Bar.cs";

		const string SampleNameSpace = "Tokeiya3";
		const string AnotherNameSpace = "時計屋";


		static SourceFile CreateStandardSample(string path = SamplePath)
		{
			var file = new SourceFile(path);
			//return new NameSpace(file);
			return file;
		}
		[Fact]
		public void DescribeTest()
		{
			var root = CreateStandardSample();
			var name = new IdentityName(root, SampleNameSpace);

			name.Describe().Is(SampleNameSpace);

		}

		[Fact]
		public void InvalidNameTest()
		{
			var root = CreateStandardSample();

			Assert.Throws<ArgumentException>(() => new IdentityName(root,""));
			Assert.Throws<ArgumentException>(() => new IdentityName(root," "));
			Assert.Throws<ArgumentException>(() => new IdentityName(root, "\t"));
		}

		[Fact()]
		public void EqualsTest()
		{
			var ns = CreateStandardSample();

			var x = new IdentityName(ns, SampleNameSpace);
			var y = new IdentityName(ns, SampleNameSpace);

			IsObjectEqual(x, y);
			IsObjectEqual(x, x);

			IsObjectEqual(x, new IdentityName(new SourceFile(AnotherPath), SampleNameSpace));

			IsObjectNotEqual(x, SampleNameSpace);
			IsObjectNotEqual(x,new IdentityName(ns,AnotherNameSpace));
		}


		[Fact()]
		public void GetHashCodeTest()
		{
			var ns = CreateStandardSample();
			var x = new IdentityName(ns, SampleNameSpace);
			var y = new IdentityName(ns, SampleNameSpace);

			HashEqual(x, y);
			HashEqual(x, new IdentityName(new SourceFile(AnotherPath), SampleNameSpace));
			HashNotEqual(x, new IdentityName(ns, AnotherNameSpace));
		}

		[Fact]
		public void OpEqTest()
		{
			var ns = CreateStandardSample();
			var x = new IdentityName(ns, SampleNameSpace);
			var y = new IdentityName(ns, SampleNameSpace);

			IsEqual(x, y);
			IsEqual(x, x);

			IsEqual(x, new IdentityName(new SourceFile(AnotherPath), SampleNameSpace));
		}

		[Fact]
		public void OpInEqTest()
		{
			var ns = CreateStandardSample();
			var piv = new IdentityName(ns, SampleNameSpace);

			IsNotEqual(piv, new IdentityName(ns, AnotherNameSpace));

		}


	}
}