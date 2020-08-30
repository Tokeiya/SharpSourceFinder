using System;
using ChainingAssertion;
using Xunit;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class NameSpaceTests
	{
		private const string SamplePath = "G:\\Hoge\\Piyo.cs";

		static SourceFile CreateStandardScr() => new SourceFile(SamplePath);

		static QualifiedName CreateName(IDiscriminatedElement parent, params string[] identities)
		{
			var ret = new QualifiedName(parent);

			foreach (var elem in identities) ret.Add(elem);

			return ret;
		}

		[Fact]
		public void NameTest()
		{
			var scr = CreateStandardScr();
			var tokeiya = new NameSpace(scr);

			var expected = CreateName(tokeiya, "Tokeiya3");
			var unexpected = CreateName(tokeiya, "Unexpected");

			Assert.Throws<InvalidOperationException>(() => tokeiya.Name);


			tokeiya.SetName(expected);
			tokeiya.Name.Is(expected);
			tokeiya.Name.IsNot(unexpected);
		}

		[Fact]
		public void DescribeTest()
		{
			const string expectedOuterDescription = @"namespace Outer.Outer
{
	namespace Inner
	{
	}
}";

			const string expectedInnerDescription = @"namespace Inner
{
}";

			var scr = CreateStandardScr();
			var outer = new NameSpace(scr);
			outer.SetName(CreateName(outer, "Outer", "Outer"));

			var inner = new NameSpace(outer);
			inner.SetName(CreateName(inner, "Inner"));


			outer.Describe().Is(expectedOuterDescription);
			inner.Describe().Is(expectedInnerDescription);
		}

		[Fact]
		public void GetFullQualifiedName()
		{
			var scr = CreateStandardScr();
			var outer = new NameSpace(scr);
			var outerName = CreateName(outer, "Outer", "Outer");
			outer.SetName(outerName);

			outer.GetFullQualifiedName().Is(outerName);


			var inner = new NameSpace(outer);
			var innerName = CreateName(inner, "Inner");
			inner.SetName(innerName);

			inner.GetFullQualifiedName().Is(CreateName(inner, "Outer", "Outer", "Inner"));
		}


		[Fact]
		public void SetNameTest()
		{
			var scr = CreateStandardScr();
			var outer = new NameSpace(scr);

			var expected = CreateName(outer, "Tokeiya3");
			outer.SetName(expected);

			outer.Name.Is(expected);

			Assert.Throws<InvalidOperationException>(() => outer.SetName(CreateName(outer, "identity")));
			outer.Name.Is(expected);
		}
	}
}