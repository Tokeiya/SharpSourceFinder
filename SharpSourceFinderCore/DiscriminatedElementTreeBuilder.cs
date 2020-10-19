using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class DiscriminatedElementTreeBuilder : CSharpSyntaxWalker
	{
		private readonly Stack<IDiscriminatedElement> _parentStack = new Stack<IDiscriminatedElement>();

		internal int InternalStackCount => _parentStack.Count;

		public NameSpace Build(CompilationUnitSyntax syntax, IPhysicalStorage storage)
		{
			var global = new NameSpace(storage);
			_ = new QualifiedElement(global);

			_parentStack.Push(global);
			Visit(syntax);
			return (NameSpace)_parentStack.Pop();
		}

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
#warning VisitClassDeclaration_Is_NotImpl
			throw new NotImplementedException("VisitClassDeclaration is not implemented");
		}

		public override void VisitStructDeclaration(StructDeclarationSyntax node)
		{
#warning VisitStructDeclaration_Is_NotImpl
			throw new NotImplementedException("VisitStructDeclaration is not implemented");
		}

		public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
#warning VisitInterfaceDeclaration_Is_NotImpl
			throw new NotImplementedException("VisitInterfaceDeclaration is not implemented");
		}

		public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
#warning VisitDelegateDeclaration_Is_NotImpl
			throw new NotImplementedException("VisitDelegateDeclaration is not implemented");
		}

		public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
#warning VisitEnumDeclaration_Is_NotImpl
			throw new NotImplementedException("VisitEnumDeclaration is not implemented");
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
#warning VisitNamespaceDeclaration_Is_NotImpl
			throw new NotImplementedException("VisitNamespaceDeclaration is not implemented");
		}



	}
}
