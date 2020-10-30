using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Tokeiya3.SharpSourceFinderCore;
using Tokeiya3.StringManipulator;

namespace Playground
{
	class Program
	{
		static void Main()
		{
			NewMethod();
		}

		static StringBuilder bld = new StringBuilder();



		static IEnumerable<(string parent,string current)> GenerateRecord(IQualified qualified)
		{
			bld.Clear();
			var parent = "";

			foreach (var id in qualified.Identities)
			{
				bld.Append(id.Name);
				yield return (parent, bld.ToString());
				parent = bld.ToString();
				bld.Append('.');
			}

			yield return (parent, bld.ToString());
		}

		private static void NewMethod()
		{
			var builder = new DiscriminatedElementTreeBuilder();
			using var wtr = new StreamWriter(@"C:\Users\net_s\OneDrive\LinqPad\RuntimeNamespace.tsv");

			foreach (var file in Directory.EnumerateFiles(@"C:\Repos\runtime", "*.cs", SearchOption.AllDirectories))
				try
				{
					Console.WriteLine(file);
					var root = CSharpSyntaxTree.ParseText(File.ReadAllText(file)).GetCompilationUnitRoot();
					var tree = builder.Build(root, new PhysicalStorage(file));

					var q = tree.Descendants().Where(x => x is NameSpaceElement).Select(x => x.GetQualifiedName());

					foreach (var(p,c) in q.Select(x=>GenerateRecord(x)).SelectMany(x=>x))
					{
						wtr.WriteLine($"{c}\t{p}");
					}
				}
				catch (Exception e)
				{
					Console.WriteLine();
					Console.WriteLine(e);
					Console.ReadLine();
				}
		}
	}
}