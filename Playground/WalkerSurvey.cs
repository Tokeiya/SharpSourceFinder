using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tokeiya3.SharpSourceFinderCore;

namespace Playground
{
	public class WalkerSurvey : CSharpSyntaxWalker
	{
		private readonly Stack<IDiscriminatedElement> _stack = new Stack<IDiscriminatedElement>();

		public override void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
		{
			var element = new QualifiedName(new Dummy());
			_stack.Push(element);
			base.VisitAliasQualifiedName(node);
		}


		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			base.VisitClassDeclaration(node);
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			base.VisitNamespaceDeclaration(node);
		}

		public override void VisitUsingDirective(UsingDirectiveSyntax node)
		{
			base.VisitUsingDirective(node);
		}

		private class Dummy : DiscriminatedElement
		{
			public override void RegisterChild(IDiscriminatedElement child)
			{
				throw new NotImplementedException();
			}

			public override void Describe(StringBuilder stringBuilder)
			{
				throw new NotImplementedException();
			}

			public override IEnumerable<IDiscriminatedElement> Children()
			{
				throw new NotImplementedException();
			}

			public override IEnumerable<IDiscriminatedElement> Descendants()
			{
				throw new NotImplementedException();
			}
		}
	}
}