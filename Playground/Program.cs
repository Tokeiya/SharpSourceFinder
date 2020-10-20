
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tokeiya3.SharpSourceFinderCore;
using Console = System.Console;

namespace Playground
{
	class Sample : CSharpSyntaxWalker
	{
		private int cnt = 0;

		void WriteLine(string value)
		{
			Console.WriteLine(new string(' ',cnt*2)+value);
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			++cnt;
			WriteLine("EnterNameSpace");
			base.VisitNamespaceDeclaration(node);
			WriteLine("ExitNameSpace");
			--cnt;
		}

		public override void VisitStructDeclaration(StructDeclarationSyntax node)
		{
			++cnt;
			WriteLine("EnterStruct");
			base.VisitStructDeclaration(node);
			WriteLine("ExitStruct");
			--cnt;
		}

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			++cnt;
			WriteLine("EnterClass");
			base.VisitClassDeclaration(node);
			WriteLine("ExitClass");
			--cnt;
		}
	}
	class Program
	{
		static void Main()
		{
			var root = CSharpSyntaxTree.ParseText(@"
namespace NameSpace
{
	public struct Public
	{
		public Public(int value) : this()
		{ }

		public int Field;
		public int Prop { get; set; }

		public override string ToString()
		{
			return ""Hello world"";
		}
	}

	public unsafe struct Unsafe
	{

	}

	public unsafe partial struct UnsafePartial
	{

	}

	public partial struct Partial
	{

	}

	internal struct Internal
	{

	}

	public class Envelope
	{
		private struct Private
		{

		}

		protected struct Protected
		{

		}

		protected internal struct ProtectedInternal
		{

		}

		private protected struct PrivateProtected
		{

		}
	}
}



").GetCompilationUnitRoot();

			var walker = new Sample();
			walker.Visit(root);

		}
	}
}