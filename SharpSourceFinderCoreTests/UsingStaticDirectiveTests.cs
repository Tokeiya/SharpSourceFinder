﻿using Xunit;
using Tokeiya3.SharpSourceFinderCore;
using System;
using System.Collections.Generic;
using System.Text;
using ChainingAssertion;

namespace Tokeiya3.SharpSourceFinderCore.Tests
{
	public class UsingStaticDirectiveTests
	{
		[Fact()]
		public void UsingStaticDirectiveTest()
		{
			var root = new SourceFile("G:\\Hoge\\Piyo.cs");

			var invalidInputs = new[] { "", " ", "\t" };

			foreach (var input in invalidInputs)
			{
				Assert.Throws<ArgumentException>(() => new UsingStaticDirective(root, input));
			}
		}

		[Fact()]
		public void DescribeTest()
		{
			var root = new SourceFile("G:\\Hoge\\Piyo.cs");
			var sample = new UsingStaticDirective(root, "System", "Linq", "Expressions");

			var bld = new StringBuilder();
			sample.Describe(bld);
			bld.ToString().Is("using static System.Linq.Expressions;");
		}
	}
}