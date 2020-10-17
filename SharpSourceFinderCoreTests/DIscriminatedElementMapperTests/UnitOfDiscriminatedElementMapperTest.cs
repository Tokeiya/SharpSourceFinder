using System.IO;
using System.Linq;
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

		static string Load(string fileName) => File.ReadAllText($"Samples\\{fileName}");

		static CompilationUnitSyntax Parse(string fileName) =>
			CSharpSyntaxTree.ParseText(Load(fileName)).GetCompilationUnitRoot();


		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapIdentityElementTest()
		{
			var root = Parse("NameSpaceOnly.cs");
			var identifiers = root.DescendantNodes().OfType<IdentifierNameSyntax>().Memoize();
			identifiers.Any().IsTrue();

			var ns = new NameSpace(new PhysicalStorage("Path"));
			var q = new QualifiedElement(ns);

			var actual = identifiers.Select(syntax => UnitOfDiscriminatedElementMapper.Map(q, syntax)).ToArray();

			actual.Length.Is(3);

			actual[0].Name.Is("NameSpace");
			actual[1].Name.Is("Hoge");
			actual[2].Name.Is("Moge");
		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapQualifiedNameElementTest()
		{
			var root = Parse("NameSpaceOnly.cs");

			var ns = new NameSpace(new PhysicalStorage("Path"));

			var syntax = root.DescendantNodes().OfType<QualifiedNameSyntax>().ToArray();

			syntax.Length.Is(2);
			var actual = UnitOfDiscriminatedElementMapper.Map(ns, syntax[0]);

			foreach (var s in syntax.Skip(1))
			{
				var a = UnitOfDiscriminatedElementMapper.Map(actual, s);
				actual.IsSameReferenceAs(a);
				actual = a;
			}
		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapNamespaceTest()
		{
			var root = Parse("NestedNamespace.cs");

			var syntax = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().ToArray();
			syntax.Length.Is(2);

			var storage = new PhysicalStorage("Path");
			var outer = UnitOfDiscriminatedElementMapper.Map(storage, syntax[0]);
			outer.Storage.IsSameReferenceAs(storage);

			var nested = UnitOfDiscriminatedElementMapper.Map(outer, syntax[1]);
			nested.Parent.IsSameReferenceAs(outer);
		}


		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapEnumTest()
		{
			void areEqual(EnumElement actual, ScopeCategories expectedCategory, IDiscriminatedElement expectedParent)
			{
				actual.IsAbstract.IsFalse();
				actual.IsPartial.IsFalse();
				actual.IsSealed.IsTrue();
				actual.IsStatic.IsFalse();
				actual.IsUnsafe.IsFalse();

				actual.Scope.Is(expectedCategory);
				actual.Parent.IsSameReferenceAs(expectedParent);
			}

			var root = Parse("EnumSamples.cs");
			using var samples = root.DescendantNodes().OfType<EnumDeclarationSyntax>().Memoize();

			var storage = new PhysicalStorage("Path");
			var ns = new NameSpace(new PhysicalStorage("Path"));

			var sample = samples.First(x => x.Identifier.Text == "PublicEnum");
			var actual = UnitOfDiscriminatedElementMapper.Map(ns, sample);
			areEqual(actual, ScopeCategories.Public, ns);

			sample = samples.First(x => x.Identifier.Text == "InternalEnum");
			actual = UnitOfDiscriminatedElementMapper.Map(ns, sample);
			areEqual(actual, ScopeCategories.Internal, ns);

			var envelope = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);

			sample = samples.First(x => x.Identifier.Text == "ProtectedEnum");
			actual = UnitOfDiscriminatedElementMapper.Map(envelope, sample);
			areEqual(actual, ScopeCategories.Protected, envelope);

			sample = samples.First(x => x.Identifier.Text == "ProtectedInternalEnum");
			actual = UnitOfDiscriminatedElementMapper.Map(envelope, sample);
			areEqual(actual, ScopeCategories.ProtectedInternal, envelope);

			sample = samples.First(x => x.Identifier.Text == "PrivateProtectedEnum");
			actual = UnitOfDiscriminatedElementMapper.Map(envelope, sample);
			areEqual(actual, ScopeCategories.PrivateProtected, envelope);
		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapStructTest()
		{
			static void areEqual(StructElement actual, ScopeCategories scope, IDiscriminatedElement parent,
				bool isPartial,
				bool isUnsafe)
			{
				actual.IsAbstract.IsFalse();
				actual.IsSealed.IsTrue();
				actual.IsPartial.Is(isPartial);
				actual.IsUnsafe.Is(isUnsafe);
				actual.IsSealed.IsTrue();

				actual.Scope.Is(scope);
				actual.Parent.IsSameReferenceAs(parent);
			}

			var root = Parse("StructSamples.cs");
			using var samples = root.DescendantNodes().OfType<StructDeclarationSyntax>().Memoize();

			var ns = new NameSpace(new PhysicalStorage("Path"));

			var sample = samples.First(x => x.Identifier.Text == "Public");
			var actual = UnitOfDiscriminatedElementMapper.Map(ns, sample);
			areEqual(actual, ScopeCategories.Public, ns, false, false);

			sample = samples.First(x => x.Identifier.Text == "Unsafe");
			actual = UnitOfDiscriminatedElementMapper.Map(ns, sample);
			areEqual(actual, ScopeCategories.Public, ns, false, true);

			sample = samples.First(x => x.Identifier.Text == "UnsafePartial");
			actual = UnitOfDiscriminatedElementMapper.Map(ns, sample);
			areEqual(actual, ScopeCategories.Public, ns, true, true);

			sample = samples.First(x => x.Identifier.Text == "Internal");
			actual = UnitOfDiscriminatedElementMapper.Map(ns, sample);
			areEqual(actual, ScopeCategories.Internal, ns, false, false);

			var envelope = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);

			sample = samples.First(x => x.Identifier.Text == "Private");
			actual = UnitOfDiscriminatedElementMapper.Map(envelope, sample);
			areEqual(actual, ScopeCategories.Private, envelope, false, false);


			sample = samples.First(x => x.Identifier.Text == "Protected");
			actual = UnitOfDiscriminatedElementMapper.Map(envelope, sample);
			areEqual(actual, ScopeCategories.Protected, envelope, false, false);

			sample = samples.First(x => x.Identifier.Text == "ProtectedInternal");
			actual = UnitOfDiscriminatedElementMapper.Map(envelope, sample);
			areEqual(actual, ScopeCategories.ProtectedInternal, envelope, false, false);

			sample = samples.First(x => x.Identifier.Text == "PrivateProtected");
			actual = UnitOfDiscriminatedElementMapper.Map(envelope, sample);
			areEqual(actual, ScopeCategories.PrivateProtected, envelope, false, false);
		}


		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapDelegateTest()
		{
			var root = Parse("DelegateSamples.cs");
			using var samples = root.DescendantNodes().OfType<DelegateDeclarationSyntax>().Memoize();
			IDiscriminatedElement parent = new NameSpace(new PhysicalStorage("Path"));


			void areEqual(string name, ScopeCategories scope, bool isUnsafe = false)
			{
				var syntax = samples.First(s => s.Identifier.Text == name);
				var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

				actual.Parent.IsSameReferenceAs(parent);
				actual.Scope.Is(scope);
				actual.IsUnsafe.Is(isUnsafe);

				actual.IsAbstract.IsFalse();
				actual.IsPartial.IsFalse();
				actual.IsSealed.IsTrue();
				actual.IsStatic.IsFalse();
			}

			areEqual("Public", ScopeCategories.Public);
			areEqual("Internal", ScopeCategories.Internal);
			areEqual("Unsafe", ScopeCategories.Public, true);

			parent = new ClassElement(parent, ScopeCategories.Public, false, false, false, false, false);
			areEqual("Protected", ScopeCategories.Protected);
			areEqual("ProtectedInternal", ScopeCategories.ProtectedInternal);
			areEqual("PrivateProtected", ScopeCategories.PrivateProtected);
			areEqual("Private", ScopeCategories.Private);
		}

		[Trait("TestLayer", nameof(UnitOfDiscriminatedElementMapper))]
		[Fact]
		public void MapInterfaceTest()
		{
			var root = Parse("InterfaceSamples.cs");
			using var samples = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().Memoize();
			IDiscriminatedElement parent = new NameSpace(new PhysicalStorage("Path"));

			void areEqual(string name, ScopeCategories scope, bool isPartial=false,bool isUnsafe = false)
			{
				var syntax = samples.First(x => x.Identifier.Text == name);
				var actual = UnitOfDiscriminatedElementMapper.Map(parent, syntax);

				actual.Parent.IsSameReferenceAs(parent);
				actual.Scope.Is(scope);
				actual.IsUnsafe.Is(isUnsafe);
				actual.IsPartial.Is(isPartial);

				actual.IsAbstract.IsTrue();
				actual.IsStatic.IsFalse();
				actual.IsSealed.IsFalse();

			}

			areEqual("IPublic", ScopeCategories.Public);
			areEqual("IInternal", ScopeCategories.Internal);
			areEqual("IUnsafe",ScopeCategories.Public,isUnsafe:true);
			areEqual("IPartial", ScopeCategories.Public, isPartial: true);

			areEqual("IProtected", ScopeCategories.Protected);
			areEqual("IProtectedInternal", ScopeCategories.ProtectedInternal);
			areEqual("IPrivateProtected", ScopeCategories.PrivateProtected);
			areEqual("IPrivate", ScopeCategories.Private);
		}

	}
}