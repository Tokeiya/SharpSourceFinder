
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tokeiya3.SharpSourceFinderCore;
using Tokeiya3.SharpSourceFinderCore.DataControl;
using Console = System.Console;

namespace Playground
{
	class Program
	{
		static void Main()
		{

			LinkedList<int> list = new LinkedList<int>();
			var ary = Enumerable.Range(0, 4).Select(i => new OrderControlElement<int> { Value = i }).ToArray();

			ary[0].MoveToAhead(ary[1]);
			ary[1].MoveToAhead(ary[2]);
			ary[2].MoveToAhead(ary[3]);



		}

		private static void NewMethod()
		{
			var builder = new DiscriminatedElementTreeBuilder();

			foreach (var file in Directory.EnumerateFiles("H:\\DotNEt", "*.cs", SearchOption.AllDirectories))
			{
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
}