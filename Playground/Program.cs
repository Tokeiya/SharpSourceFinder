
using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Console = System.Console;

namespace Playground
{
	public class MyWalker : CSharpSyntaxWalker
	{
		
	}
	public enum Hoge
	{

	}
	class Program
	{


		static void Main()
		{
			var tree = CSharpSyntaxTree.ParseText(@"
namespace NameSpace
{
	public struct Public
	{

	}

	public unsafe struct Unsafe
	{

	}

	public unsafe partial struct UnsafePartial
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

");
			var root = tree.GetCompilationUnitRoot();
			var sample = root.DescendantNodes().OfType<StructDeclarationSyntax>()
				.First(x => x.Identifier.Text == "Unsafe");

			sample.Modifiers.Select(x => x.Text).ForEach(x=> Console.WriteLine(x));
			
			Console.WriteLine(sample.Modifiers.Count);
		}
	}
}