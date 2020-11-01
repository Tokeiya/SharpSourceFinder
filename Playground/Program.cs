using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using FastEnumUtility;
using Microsoft.CodeAnalysis.CSharp;
using Tokeiya3.SharpSourceFinderCore;
using Tokeiya3.StringManipulator;

namespace Playground
{
	class Program
	{
		public static string Encode(byte value)
		{
			if((value&8)!=0)
			{
				if((value&4)!=0)
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "++++";
						else return "+++-";
					}
					else
					{
						if ((value & 1) != 0) return "++-+";
						else return "++--";
					}
				}
				else
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "+-++";
						else return "+-+-";
					}
					else
					{
						if ((value & 1) != 0) return "+--+";
						else return "+---";
					}
				}
			}
			else
			{
				if ((value & 4) != 0)
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "-+++";
						else return "-++-";
					}
					else
					{
						if ((value & 1) != 0) return "-+-+";
						else return "-+--";
					}
				}
				else
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "--++";
						else return "--+-";
					}
					else
					{
						if ((value & 1) != 0) return "---+";
						else return "----";
					}
				}
			}

		}


		static void Main()
		{
			for (byte i = 0; i < 16; i++)
			{
				Console.WriteLine($"{i:00}:{Encode(i)}");
			}
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

			//yield return (parent, bld.Extract(..^1).ToString());
		}

		private static void NewMethod()
		{
			var builder = new DiscriminatedElementTreeBuilder();
			using var wtr = new StreamWriter(@"C:\Users\net_s\OneDrive\LinqPad\WinformsNamespace.tsv");

			foreach (var file in Directory.EnumerateFiles(@"C:\Repos\winforms", "*.cs", SearchOption.AllDirectories))
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
					//Console.ReadLine();
				}
		}
	}
}