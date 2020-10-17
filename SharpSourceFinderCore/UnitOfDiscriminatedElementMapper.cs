﻿using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tokeiya3.SharpSourceFinderCore
{
	public static class UnitOfDiscriminatedElementMapper
	{
		public static IdentityElement Map(QualifiedElement parent, IdentifierNameSyntax syntax) =>
			new IdentityElement(parent, syntax.Identifier.Text);

		public static QualifiedElement Map(IDiscriminatedElement parent, QualifiedNameSyntax syntax) =>
			parent switch
			{
				QualifiedElement q => q,
				_ => new QualifiedElement(parent)
			};


		private static (ScopeCategories scope, bool isUnsafe, bool isPartial, bool isStatic, bool isAbstract) ParseModifiers(
			SyntaxTokenList list)
		{
			bool isPublic = false;
			bool isInternal = false;
			bool isProtected = false;
			bool isPrivate = false;

			bool isUnsafe = false;
			bool isPartial = false;
			bool isStatic = false;
			bool isAbstract = false;



			foreach (var elem in list)
			{
				if (elem.Text == "public") isPublic = true;
				else if (elem.Text == "private") isPrivate = true;
				else if (elem.Text == "internal") isInternal = true;
				else if (elem.Text == "protected") isProtected = true;
				else if (elem.Text == "unsafe") isUnsafe = true;
				else if (elem.Text == "partial") isPartial = true;
				else if (elem.Text == "static") isStatic = true;
				else if (elem.Text == "abstract") isAbstract = true;
			}

			ScopeCategories scope;

			if (isPublic) scope = ScopeCategories.Public;
			else if (isInternal)
			{
				if (isProtected) scope = ScopeCategories.ProtectedInternal;
				else scope = ScopeCategories.Internal;
			}
			else if (isProtected)
			{
				if (isPrivate) scope = ScopeCategories.PrivateProtected;
				else scope = ScopeCategories.Protected;
			}
			else scope = ScopeCategories.Private;

			return (scope, isUnsafe, isPartial, isStatic, isAbstract);

		}

		public static NameSpace Map(NameSpace parent, NamespaceDeclarationSyntax syntax) => new NameSpace(parent);

		public static NameSpace Map(IPhysicalStorage storage, NamespaceDeclarationSyntax syntax) =>
			new NameSpace(storage);

		public static EnumElement Map(IDiscriminatedElement parent, EnumDeclarationSyntax syntax)
		{
			var (scope,_,_,_,_) = ParseModifiers(syntax.Modifiers);
			return new EnumElement(parent, scope);
		}

		public static StructElement Map(IDiscriminatedElement parent, StructDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, _, _) = ParseModifiers(syntax.Modifiers);
			return new StructElement(parent, scope, isUnsafe, isPartial);
		}

		public static DelegateElement Map(IDiscriminatedElement parent, DelegateDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, _, _, _) = ParseModifiers(syntax.Modifiers);
			return new DelegateElement(parent, scope, isUnsafe);
		}

		public static InterfaceElement Map(IDiscriminatedElement parent, InterfaceDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, _, _) = ParseModifiers(syntax.Modifiers);
			return new InterfaceElement(parent, scope, isUnsafe, isPartial);

		}
	}
}