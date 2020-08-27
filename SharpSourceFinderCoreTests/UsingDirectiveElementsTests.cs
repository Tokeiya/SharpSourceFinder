using Xunit;
using Tokeiya3.SharpSourceFinderCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ChainingAssertion;
using Microsoft.CodeAnalysis;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class UsingDirectiveElementsTests
	{
		static (SourceFile root, UsingDirectiveElements element) CreateSample()
		{
			var root = new SourceFile("G:\\Hoge\\Sample.cs");
			var element = new UsingDirectiveElements(root);

			element.Add("System");
			element.Add("Linq");

			return (root, element);


		}
		[Fact()]
		public void UsingDirectiveElementsTest()
		{
			var (root, element) = CreateSample();
			element.Parent.Is(root);
		}

		[Fact()]
		public void AddTest()
		{
			var (_, actual) = CreateSample();

			var sample = actual.Add("Hoge");
			sample.Describe().Is("Hoge");

			actual.Describe().Is("using System.Linq.Hoge;");

		}

		[Fact()]
		public void DescribeTest()
		{
			var (_, actual) = CreateSample();

			var bld = new StringBuilder();
			actual.Describe(bld);
			bld.ToString().Is("using System.Linq;");

			actual.Describe().Is("using System.Linq;");
		}

		[Fact()]
		public void ChildrenTest()
		{
			var (_, actual) = CreateSample();

			var array = actual.Children().Select(x => x.Representation).ToArray();
			array.Length.Is(2);

			array[0].Is("System");
			array[1].Is("Linq");
		}

		[Fact()]
		public void DescendantsTest()
		{
			var (_, actual) = CreateSample();

			var array = actual.Descendants().Select(x => x.Representation).ToArray();
			array.Length.Is(2);

			array[0].Is("System");
			array[1].Is("Linq");
		}
	}
}