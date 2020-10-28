using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Tokeiya3.SharpSourceFinderCore;

namespace Playground
{
	class Program
	{
		static void Main()
		{
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