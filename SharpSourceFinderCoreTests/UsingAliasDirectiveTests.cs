using ChainingAssertion;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class UsingAliasDirectiveTests
	{
		static UsingAliasDirective CreateSample()
		{
			var sf = new SourceFile("G:\\Hoge\\Piyo.cs");
			return new UsingAliasDirective(sf, "io", "System","IO");

		}
		[Fact()]
		public void UsingAliasDirectiveTest()
		{
			var root = new SourceFile("G:\\Hoge\\Piyo.cs");

			var invalidInputs = new[] { "", " ", "\t" };

			foreach (var input in invalidInputs)
			{
				Assert.Throws<ArgumentException>(() => new UsingAliasDirective(root, input, "System"));
				Assert.Throws<ArgumentException>(() => new UsingAliasDirective(root, "alias", input));
			}
		}

		[Fact()]
		public void ChildrenTest()
		{
			var sample = CreateSample();
			var actual = sample.Children().ToArray();

			(actual[0] is IdentityName).IsTrue();
			((IdentityName)actual[0]).Identity.Is("io");

			(actual[1] is QualifiedName).IsTrue();

		}

		[Fact()]
		public void DescendantsTest()
		{
			var actual = CreateSample().Descendants().ToArray();
			actual.Length.Is(4);

			(actual[0] is IdentityName).IsTrue();
			((IdentityName)actual[0]).Identity.Is("io");

			(actual[1] is QualifiedName).IsTrue();

			(actual[2] is IdentityName).IsTrue();
			((IdentityName)actual[2]).Identity.Is("System");

			(actual[3] is IdentityName).IsTrue();
			((IdentityName)actual[3]).Identity.Is("IO");
		}

		[Fact()]
		public void DescribeTest()
		{
			var actual = CreateSample();
			var bld = new StringBuilder();

			actual.Describe().Is("using io=System.IO;");
			
			actual.Describe(bld);
			bld.ToString().Is("using io=System.IO;");
		}
	}
}