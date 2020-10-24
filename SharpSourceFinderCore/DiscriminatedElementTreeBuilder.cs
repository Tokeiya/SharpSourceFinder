using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class DiscriminatedElementTreeBuilder : CSharpSyntaxWalker
	{
		private readonly Stack<IDiscriminatedElement> _parentStack = new Stack<IDiscriminatedElement>();

		internal int InternalStackCount => _parentStack.Count;

		public NameSpaceElement Build(CompilationUnitSyntax syntax, IPhysicalStorage storage)
		{
			try
			{
				var global = new NameSpaceElement(storage);
				_ = new QualifiedElement(global);

				_parentStack.Push(global);
				Visit(syntax);
				Debug.Assert(_parentStack.Count == 1);
				return (NameSpaceElement) _parentStack.Pop();
			}
			finally
			{
				_parentStack.Clear();
			}
		}

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map(_parentStack.Peek(), node));
			base.VisitClassDeclaration(node);
			_parentStack.Pop();
		}

		public override void VisitStructDeclaration(StructDeclarationSyntax node)
		{
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map(_parentStack.Peek(), node));
			base.VisitStructDeclaration(node);
			_parentStack.Pop();
		}

		public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map(_parentStack.Peek(), node));
			base.VisitInterfaceDeclaration(node);
			_parentStack.Pop();
		}

		public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map(_parentStack.Peek(), node));
			base.VisitDelegateDeclaration(node);
			_parentStack.Pop();
		}

		public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map(_parentStack.Peek(), node));
			base.VisitEnumDeclaration(node);
			_parentStack.Pop();
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map((NameSpaceElement) _parentStack.Peek(), node));
			base.VisitNamespaceDeclaration(node);
			_parentStack.Pop();
		}
	}
}