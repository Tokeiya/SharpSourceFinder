using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tokeiya3.SharpSourceFinderCore
{
	public static class UnitOfDiscriminatedElementMapper
	{
#pragma warning disable IDE0060 // 未使用のパラメーターを削除します

		public static IdentityElement Map(QualifiedElement parent, IdentifierNameSyntax syntax) =>
			new IdentityElement(parent, syntax.Identifier.Text);

		public static QualifiedElement Map(IDiscriminatedElement parent, QualifiedNameSyntax syntax) =>
#pragma warning restore IDE0060 // 未使用のパラメーターを削除します
			parent switch
			{
				QualifiedElement q => q,
				_ => new QualifiedElement(parent)
			};


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
#pragma warning disable IDE0060 // 未使用のパラメーターを削除します
		public static NameSpace Map(NameSpace parent, NamespaceDeclarationSyntax syntax) => new NameSpace(parent);

		public static NameSpace Map(IPhysicalStorage storage, NamespaceDeclarationSyntax syntax) =>
#pragma warning restore IDE0060 // 未使用のパラメーターを削除します
			new NameSpace(storage);

		public static EnumElement Map(IDiscriminatedElement parent, EnumDeclarationSyntax syntax)
		{
			var (scope,_,_,_,_,_) = ParseModifiers(syntax.Modifiers);
			return new EnumElement(parent, scope);
		}

		public static StructElement Map(IDiscriminatedElement parent, StructDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, _, _,_) = ParseModifiers(syntax.Modifiers);
			return new StructElement(parent, scope, isUnsafe, isPartial);
		}

		public static DelegateElement Map(IDiscriminatedElement parent, DelegateDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, _, _, _,_) = ParseModifiers(syntax.Modifiers);
			return new DelegateElement(parent, scope, isUnsafe);
		}

		public static InterfaceElement Map(IDiscriminatedElement parent, InterfaceDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, _, _,_) = ParseModifiers(syntax.Modifiers);
			return new InterfaceElement(parent, scope, isUnsafe, isPartial);
		}

		public static ClassElement Map(IDiscriminatedElement parent, ClassDeclarationSyntax syntax)
		{
			var (scope, isUnsafe, isPartial, isStatic, isAbstract, isSealed) = ParseModifiers(syntax.Modifiers);

			return new ClassElement(parent, scope, isAbstract, isSealed, isUnsafe, isPartial, isStatic);

		}
	}
}