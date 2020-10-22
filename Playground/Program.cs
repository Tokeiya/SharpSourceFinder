using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Tokeiya3.SharpSourceFinderCore;
using Tokeiya3.SharpSourceFinderCore.DataControl;

namespace Playground
{
	class Program
	{
		static void Main()
		{
			LinkedList<int> list = new LinkedList<int>();
			var ary = Enumerable.Range(0, 4).Select(i => new OrderControlElement<int> {Value = i}).ToArray();

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