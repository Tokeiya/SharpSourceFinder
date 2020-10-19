using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tokeiya3.SharpSourceFinderCore
{
	public static class UnitOfDiscriminatedElementMapper
	{
		private static (ScopeCategories scope, bool isUnsafe, bool isPartial, bool isStatic, bool isAbstract,bool isSealed) ParseModifiers(
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
			bool isSealed = false;


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
				else if (elem.Text == "sealed") isSealed = true;
			}

			ScopeCategories scope;

			if (isPublic) scope = ScopeCategories.Public;
			else if (isInternal)
			{
				scope = isProtected ? ScopeCategories.ProtectedInternal : ScopeCategories.Internal;
			}
			else if (isProtected)
			{
				scope = isPrivate ? ScopeCategories.PrivateProtected : ScopeCategories.Protected;
			}
			else scope = ScopeCategories.Private;

			return (scope, isUnsafe, isPartial, isStatic, isAbstract,isSealed);

		}

		private static void AttachName(IDiscriminatedElement target, NameSyntax syntax)
		{
			var qualified = new QualifiedElement(target);

			static void rec(QualifiedElement q, QualifiedNameSyntax name)
			{
				if (name.Left is QualifiedNameSyntax qs) rec(q, qs);
				else if (name.Left is SimpleNameSyntax sn) _ = new IdentityElement(q, sn.Identifier.Text);
				else throw new InvalidOperationException($"{name.Left.GetType().Name} is unexpected");

				_ = new IdentityElement(q, name.Right.Identifier.Text);
			}

			if (syntax is QualifiedNameSyntax qualifiedSyntax) rec(qualified, qualifiedSyntax);
			else if (syntax is SimpleNameSyntax simple) _ = new IdentityElement(qualified, simple.Identifier.Text);
			else throw new InvalidOperationException($"{syntax.GetType().Name} is unexpected.");
		}

		private static void AttachName(IDiscriminatedElement target, SyntaxToken identifier)
		{
			var q = new QualifiedElement(target);
			_ = new IdentityElement(q, identifier.Text);
		}

		public static NameSpace Map(NameSpace parent, NamespaceDeclarationSyntax syntax)
		{
			var ns = new NameSpace(parent);
			AttachName(ns, syntax.Name);

			return ns;
		}
		public static EnumElement Map(IDiscriminatedElement parent, EnumDeclarationSyntax syntax)
		{
			var (scope,_,_,_,_,_) = ParseModifiers(syntax.Modifiers);
			var ret =new  EnumElement(parent, scope);
			AttachName(ret, syntax.Identifier);

			return ret;
		}

		public static StructElement Map(IDiscriminatedElement parent, StructDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, _, _,_) = ParseModifiers(syntax.Modifiers);
			var ret= new StructElement(parent, scope, isUnsafe, isPartial);
			AttachName(ret, syntax.Identifier);

			return ret;
		}

		public static DelegateElement Map(IDiscriminatedElement parent, DelegateDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, _, _, _,_) = ParseModifiers(syntax.Modifiers);
			var ret = new DelegateElement(parent, scope, isUnsafe);
			AttachName(ret, syntax.Identifier);

			return ret;
		}

		public static InterfaceElement Map(IDiscriminatedElement parent, InterfaceDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, _, _,_) = ParseModifiers(syntax.Modifiers);
			var ret = new InterfaceElement(parent, scope, isUnsafe, isPartial);
			AttachName(ret, syntax.Identifier);

			return ret;
		}

		public static ClassElement Map(IDiscriminatedElement parent, ClassDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, isStatic, isAbstract, isSealed) = ParseModifiers(syntax.Modifiers);
			var ret = new ClassElement(parent, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic);
			AttachName(ret, syntax.Identifier);

			return ret;
		}
	}
}