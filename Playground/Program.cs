
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Console = System.Console;

namespace Playground
{

	class Program
	{
		static IReadOnlyList<string> GetName(NameSyntax syntax)
		{
			static void recursion(QualifiedNameSyntax qualified, List<string> accum)
			{
				if (qualified.Left is QualifiedNameSyntax q)
				{
					recursion(q, accum);
				}
				else if (qualified.Left is IdentifierNameSyntax id)
				{
					accum.Add(id.Identifier.Text);
				}
				accum.Add(qualified.Right.Identifier.Text);
			}

			var accumulator = new List<string>();

			if (syntax is IdentifierNameSyntax id)
			{
				accumulator.Add(id.Identifier.Text);
			}
			else if (syntax is QualifiedNameSyntax qualified)
			{
				recursion(qualified, accumulator);
			}

			return accumulator;
		}
		static void Main()
		{
			var hoge = new [] { "Hello", "World" };
			var piyo = new[] { "Hello", "World","Foo" };

			Console.WriteLine(hoge.SequenceEqual(piyo));

		}
	}
}