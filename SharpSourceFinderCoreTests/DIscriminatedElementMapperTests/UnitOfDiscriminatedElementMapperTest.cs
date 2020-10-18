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
			CSharpSyntaxTree.ParseText(File.ReadAllText($"Sample\\{path}")).GetCompilationUnitRoot();

		static T Extract<T>(CompilationUnitSyntax syntax, string name) where T : BaseTypeDeclarationSyntax
			=> syntax.DescendantNodes().OfType<T>().First(x => x.Identifier.Text == name);

		static IReadOnlyList<string> GetName(NameSyntax syntax)
		{
			static void recursion(QualifiedNameSyntax qualified, List<string> accum)
			{
				if (qualified.Left is QualifiedNameSyntax q)
				{
					recursion(q,accum);
				}
				else if(qualified.Left is IdentifierNameSyntax id)
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
			else if(syntax is QualifiedNameSyntax qualified)
			{
				recursion(qualified,accumulator);
			}

			return accumulator;
		}

		static NamespaceDeclarationSyntax ExtractNameSpace(CompilationUnitSyntax syntax, params string[] name) => syntax.DescendantNodes().OfType<NamespaceDeclarationSyntax>()
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
		public void SimpleNameSpaceTest()
		{
			using var samples = Parse("NameSpaceOnly.cs").DescendantNodes().OfType<NamespaceDeclarationSyntax>().Memoize();
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
		public void NestedNameSpaceTest()
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
		public void DelegateTest()
		{
			var root = Parse("DelegateSamples.cs");
			var parent = GenerateRoot("DelegateSamples.cs");

			void areEqual(string name, ScopeCategories scope,bool isUnsafe=false)
			{
				var syntax = ExtractDelegate(root, "Public");
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
		public void EnumTest()
		{
			var root = Parse("EnumSample.cs");
			var parent = GenerateRoot("EnumSamples.cs");

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

	}
}