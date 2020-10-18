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
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map((NameSpace)_parentStack.Peek(), node));
			base.VisitNamespaceDeclaration(node);
			_parentStack.Pop();
		}

		public override void VisitQualifiedName(QualifiedNameSyntax node)
		{
			_parentStack.Push(UnitOfDiscriminatedElementMapper.Map(_parentStack.Peek(), node));
			base.VisitQualifiedName(node);
			_parentStack.Pop();
		}
		public override void VisitIdentifierName(IdentifierNameSyntax node)
		{
			var parent = _parentStack.Peek();

			var ret = parent switch
			{
				TypeElement type => UnitOfDiscriminatedElementMapper.Map(type, node),
				QualifiedElement qualified => UnitOfDiscriminatedElementMapper.Map(qualified, node),
				_ => throw new InvalidOperationException($"{parent.GetType().Name} is unexpected parent")
			};

			_parentStack.Push(ret);
			base.VisitIdentifierName(node);
			_parentStack.Pop();
		}




	}
}
