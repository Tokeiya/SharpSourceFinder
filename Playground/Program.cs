using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tokeiya3.SharpSourceFinderCore;

namespace Playground
{
	class Hoge : CSharpSyntaxVisitor<IDiscriminatedElement>
	{
		public override IDiscriminatedElement Visit(SyntaxNode? node)
		{
			return base.Visit(node);
		}

		public override IDiscriminatedElement VisitUsingDirective(UsingDirectiveSyntax node)
		{

			return base.VisitUsingDirective(node);
		}
	}

	class Walker : CSharpSyntaxWalker
	{
		public override void VisitUsingDirective(UsingDirectiveSyntax node)
		{
			var tmp = node.Name.ChildTokens().First();

			base.VisitUsingDirective(node);
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			var scr = File.ReadAllText("ParseSample.cs");

			var tree = CSharpSyntaxTree.ParseText(scr);

			var list = tree.GetCompilationUnitRoot().ChildNodes().OfType<UsingDirectiveSyntax>().Select(x=>x.Name).ToList();
		}
	}
}