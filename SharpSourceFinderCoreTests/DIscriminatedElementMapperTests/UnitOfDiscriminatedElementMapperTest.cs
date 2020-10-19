using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using ChainingAssertion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;

namespace SharpSourceFinderCoreTests.DiscriminatedElementMapperTests
{
	public class UnitOfDiscriminatedElementMapperTest
	{
		// ReSharper disable once NotAccessedField.Local
		private readonly ITestOutputHelper _output;

		public UnitOfDiscriminatedElementMapperTest(ITestOutputHelper output) => _output = output;

		static CompilationUnitSyntax Parse(string path) =>
			CSharpSyntaxTree.ParseText(File.ReadAllText($"Samples/{path}")).GetCompilationUnitRoot();

		static T Extract<T>(CompilationUnitSyntax syntax, string name) where T : BaseTypeDeclarationSyntax
			=> syntax.DescendantNodes().OfType<T>().First(x => x.Identifier.Text == name);

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

		static NamespaceDeclarationSyntax ExtractNameSpace(CompilationUnitSyntax syntax, params string[] name) => syntax
			.DescendantNodes().OfType<NamespaceDeclarationSyntax>()
			.First(x => GetName(x.Name).SequenceEqual(name));

		static DelegateDeclarationSyntax ExtractDelegate(CompilationUnitSyntax syntax, string name) => syntax
			.DescendantNodes().OfType<DelegateDeclarationSyntax>()
			.First(x => x.Identifier.Text == name);

		static NameSpace GenerateRoot(string path)
		{
			var ns = new NameSpace(new PhysicalStorage(path));
			_ = new QualifiedElement();
			return ns;
		}


		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapToSimpleNameSpace()
		{
			using var samples = Parse("NameSpaceOnly.cs").DescendantNodes().OfType<NamespaceDeclarationSyntax>()
				.Memoize();
			samples.Count().Is(1);

			var parent = new NameSpace(new PhysicalStorage("NameSpaceOnly.cs"));
			_ = new QualifiedElement(parent);

			var actual = UnitOfDiscriminatedElementMapper.Map(parent, samples.First());

			var names = actual.Identity.Identities.Select(x => x.Name).ToArray();
			names.Length.Is(3);

			names[0].Is("NameSpace");
			names[1].Is("Hoge");
			names[2].Is("Moge");

			actual.Parent.IsSameReferenceAs(parent);
			actual.Children().Count().Is(1);


		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapToNestedNameSpace()
		{
			var root = Parse("NestedNameSpace.cs");
			var syntax = ExtractNameSpace(root, "Outer");
			var parent = GenerateRoot("NestedNameSpace.cs");

			var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

			actual.Identity.Identities.Count.Is(1);
			actual.Identity.Identities[0].Name.Is("Outer");

			syntax = ExtractNameSpace(root, "Inner");
			actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

			actual.Identity.Identities.Count.Is(1);
			actual.Identity.Identities[0].Name.Is("Inner");
		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapToDelegate()
		{
			var root = Parse("DelegateSamples.cs");
			var parent = GenerateRoot("DelegateSamples.cs");

			void areEqual(string name, ScopeCategories scope, bool isUnsafe = false)
			{
				var syntax = ExtractDelegate(root, name);
				var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

				actual.Identity.Identities.Count.Is(1);
				actual.Identity.Identities[0].Name.Is(name);

				actual.Scope.Is(scope);
				actual.IsUnsafe.Is(isUnsafe);

				actual.IsSealed.IsTrue();
				actual.IsAbstract.IsFalse();
				actual.IsStatic.IsFalse();
				actual.IsPartial.IsFalse();
			}

			areEqual("Public", ScopeCategories.Public, false);
			areEqual("Internal", ScopeCategories.Internal, false);
			areEqual("Unsafe", ScopeCategories.Public, true);
			areEqual("Protected", ScopeCategories.Protected, false);
			areEqual("ProtectedInternal", ScopeCategories.ProtectedInternal, false);
			areEqual("PrivateProtected", ScopeCategories.PrivateProtected);
			areEqual("Private", ScopeCategories.Private);

		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapToEnum()
		{
			var root = Parse("EnumSamples.cs");
			var parent = GenerateRoot("EnumSampleS.cs");

			void areEqual(string name, ScopeCategories scope)
			{
				var syntax = Extract<EnumDeclarationSyntax>(root, name);
				var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

				actual.Scope.Is(scope);

				actual.IsAbstract.IsFalse();
				actual.IsPartial.IsFalse();
				actual.IsSealed.IsTrue();
				actual.IsStatic.IsFalse();
				actual.IsUnsafe.IsFalse();

			}

			areEqual("PublicEnum", ScopeCategories.Public);
			areEqual("InternalEnum", ScopeCategories.Internal);
			areEqual("ProtectedEnum", ScopeCategories.Protected);
			areEqual("ProtectedInternalEnum", ScopeCategories.ProtectedInternal);
			areEqual("PrivateProtectedEnum", ScopeCategories.PrivateProtected);
		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapToInterface()
		{
			var root = Parse("InterfaceSamples.cs");
			var parent = GenerateRoot("InterfaceSamples.cs");

			void areEqual(string name, ScopeCategories scope, bool isUnsafe = false, bool isPartial = false)
			{
				var syntax = Extract<InterfaceDeclarationSyntax>(root, name);
				var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

				actual.Scope.Is(scope);

				actual.IsUnsafe.Is(isUnsafe);
				actual.IsPartial.Is(isPartial);

				actual.IsAbstract.IsTrue();
				actual.IsSealed.IsFalse();
				actual.IsStatic.IsFalse();
				actual.IsSealed.IsFalse();

			}

			areEqual("IPublic", ScopeCategories.Public);
			areEqual("IInternal", ScopeCategories.Internal);
			areEqual("IUnsafe", ScopeCategories.Public, isUnsafe: true);
			areEqual("IProtected", ScopeCategories.Protected);
			areEqual("IPrivateProtected", ScopeCategories.PrivateProtected);
			areEqual("IProtectedInternal", ScopeCategories.ProtectedInternal);
			areEqual("IPrivate", ScopeCategories.Private);

		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapToStruct()
		{
			var root = Parse("StructSamples.cs");
			var parent = GenerateRoot("StructSamples.cs");

			void areEqual(string name, ScopeCategories scope, bool isUnsafe = false, bool isPartial = false)
			{
				var syntax = Extract<StructDeclarationSyntax>(root, name);
				var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

				actual.Scope.Is(scope);

				actual.IsUnsafe.Is(isUnsafe);
				actual.IsPartial.Is(isPartial);

				actual.IsAbstract.IsFalse();
				actual.IsStatic.IsFalse();
				actual.IsSealed.IsTrue();
			}

			areEqual("Public", ScopeCategories.Public);
			areEqual("Unsafe", ScopeCategories.Public, true);
			areEqual("UnsafePartial", ScopeCategories.Public, true, true);
			areEqual("Partial", ScopeCategories.Public, isPartial: true);
			areEqual("Internal", ScopeCategories.Internal);
			areEqual("Private", ScopeCategories.Private);
			areEqual("Protected", ScopeCategories.Protected);
			areEqual("ProtectedInternal", ScopeCategories.ProtectedInternal);
			areEqual("PrivateProtected", ScopeCategories.PrivateProtected);

		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapToClass()
		{
			var root = Parse("ClassSamples.cs");
			var parent = GenerateRoot("ClassSamples.cs");

			void areEqual(string name, ScopeCategories scope, bool isUnsafe = false, bool isPartial = false,
				bool isSealed = false, bool isAbstract = false)
			{
				var syntax = Extract<ClassDeclarationSyntax>(root, name);
				var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

				actual.Scope.Is(scope);
				actual.IsUnsafe.Is(isUnsafe);
				actual.IsPartial.Is(isPartial);
				actual.IsSealed.Is(isSealed);
				actual.IsAbstract.Is(isAbstract);

			}

			areEqual("Public", ScopeCategories.Public);
			areEqual("Internal", ScopeCategories.Internal);
			areEqual("Unsafe", ScopeCategories.Public, isUnsafe: true);
			areEqual("Partial", ScopeCategories.Public, isPartial: true);
			areEqual("Abstract", ScopeCategories.Public, isAbstract: true);
			areEqual("Sealed", ScopeCategories.Public, isSealed: true);
			areEqual("UnsafePartialSealed", ScopeCategories.Public, isUnsafe: true, isPartial: true, isSealed: true);

			areEqual("Private", ScopeCategories.Private);
			areEqual("PrivateProtected", ScopeCategories.PrivateProtected);
			areEqual("ProtectedInternal", ScopeCategories.ProtectedInternal);
			areEqual("Protected", ScopeCategories.Protected);
		}


	}
}