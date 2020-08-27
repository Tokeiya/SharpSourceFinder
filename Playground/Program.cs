using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;

namespace Playground
{


	class Walker : CSharpSyntaxWalker
	{

		public override void VisitUsingDirective(UsingDirectiveSyntax node)
		{
			Console.WriteLine(node);
			base.VisitUsingDirective(node);
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			var scr = File.ReadAllText("ParseSample.cs");
			var tree = CSharpSyntaxTree.ParseText(scr).GetCompilationUnitRoot();

			var walker = new Walker();
			walker.Visit(tree);
		}
	}
}