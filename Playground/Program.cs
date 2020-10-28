using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;

namespace Playground
{
	class Program
	{
		static void Main()
		{
			var ns = new NameSpaceElement();
			var q = new QualifiedElement(ns);
			_ = new IdentityElement(q, "Name");

			var cls = new ClassElement(ns, ScopeCategories.Internal, false, false, false, false, false);
			q = new QualifiedElement(cls);
			_ = new IdentityElement(q, "InternalClass");


			var qualified = cls.GetQualifiedName();

		}

		private static void NewMethod()
		{
			var builder = new DiscriminatedElementTreeBuilder();

			foreach (var file in Directory.EnumerateFiles("H:\\DotNEt", "*.cs", SearchOption.AllDirectories))
				try
				{
					Console.WriteLine(file);
					var root = CSharpSyntaxTree.ParseText(File.ReadAllText(file)).GetCompilationUnitRoot();
					var tree = builder.Build(root, new PhysicalStorage(file));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
		}
	}
}