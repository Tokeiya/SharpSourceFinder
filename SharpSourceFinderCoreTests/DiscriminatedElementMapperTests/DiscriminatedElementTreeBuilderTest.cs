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
	public class DiscriminatedElementTreeBuilderTest
	{
		private readonly ITestOutputHelper _output;

		public DiscriminatedElementTreeBuilderTest(ITestOutputHelper output) => _output = output;

		(CompilationUnitSyntax root, PhysicalStorage storage) BuildSyntaxTree(string fileName)
		{
			var storage = new PhysicalStorage($"Samples/{fileName}");
			var root = CSharpSyntaxTree.ParseText(File.ReadAllText(storage.Path)).GetCompilationUnitRoot();

			return (root, storage);
		}

		static void Verify(NameSpaceElement actual)
		{
			ImaginaryRoot.IsImaginaryRoot(actual.Parent).IsTrue();
			actual.Identity.Identities.IsEmpty();
		}

		static void Verify(IIdentity actual, string name, IdentityCategories category, IQualified from, int order)
		{
			actual.Name.Is(name);
			actual.Category.Is(category);
			actual.From.Is(from);
			actual.Order.Is(order);
		}

		static void Verify(NameSpaceElement actual, IDiscriminatedElement parent, params string[] names)
		{
			actual.Parent.IsSameReferenceAs(parent);
			var id = actual.Identity;
			id.Identities.Count.Is(names.Length);

			for (int i = 0; i < names.Length; i++)
				Verify(id.Identities[i], names[i], IdentityCategories.Namespace, id, i + 1);
		}

		static T Extract<T>(NameSpaceElement tree, string name) where T : TypeElement
		{
			var seq = tree.DescendantsAndSelf().OfType<T>().Where(x => x.Identity.Identities[0].Name == name);
			seq.Count().Is(1);
			return seq.First();
		}

		static NameSpaceElement ExtractNameSpace(NameSpaceElement tree, params string[] names)
		{
			bool pred(NameSpaceElement ns)
			{
				var tmp = ns.Identity.Identities.Select(x => x.Name);
				return tmp.SequenceEqual(names);
			}

			var seq = tree.DescendantsAndSelf().OfType<NameSpaceElement>().Where(x => pred(x));
			seq.Count().Is(1);
			return seq.First();
		}


		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void NamespaceOnly()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("NameSpaceOnly.cs");

			var actual = mapper.Build(root, storage);
			Verify(actual);

			var ns = actual.Descendants().OfType<NameSpaceElement>();
			ns.Count().Is(1);

			var name = ns.First().Identity;
			name.Identities.Count.Is(3);

			foreach (var (nme, idx) in new[] {"NameSpace", "Hoge", "Moge"}.Select((s, i) => (s, i)))
				Verify(name.Identities[idx], nme, IdentityCategories.Namespace, name, idx + 1);
		}


		void Verify(EnumElement actual, IDiscriminatedElement parent, string name)
		{
			actual.Parent.IsSameReferenceAs(parent);
			actual.Identity.Identities.First().Name.Is(name);
		}

		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void NestedNameSpace()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("NestedNameSpace.cs");

			var tree = mapper.Build(root, storage);
			Verify(tree);

			var ns = tree.Descendants().OfType<NameSpaceElement>();
			ns.Count().Is(2);

			var actual = ns.First();
			Verify(actual, tree, "Outer");

			var parent = actual;
			actual = ns.Skip(1).First();
			Verify(actual, parent, "Inner");
		}

		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void MultiNamespace()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("MultiNameSpace.cs");

			var tree = mapper.Build(root, storage);
			Verify(tree);

			tree.Children().Count().Is(3);

			var actual = (NameSpaceElement) tree.Children().Skip(1).First();
			Verify(actual, tree, "OuterA");
			Verify((NameSpaceElement) tree.Children().Skip(2).First(), tree, "OuterB");

			var parent = (NameSpaceElement) tree.Children().Skip(1).First();
			parent.Children().Count().Is(2);
			Verify(ExtractNameSpace(tree, "InnerA"), parent, "InnerA");

			parent = (NameSpaceElement) tree.Children().Skip(2).First();
			parent.Children().Count().Is(2);
			Verify(ExtractNameSpace(tree, "InnerB"), parent, "InnerB");
		}


		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void Enum()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("EnumSamples.cs");

			var tree = mapper.Build(root, storage);
			Verify(tree);

			Verify((NameSpaceElement) tree.Children().Skip(1).First(), tree, "EnumSamples");

			var enums = tree.Descendants().OfType<EnumElement>();
			enums.Count().Is(5);

			IDiscriminatedElement parent = ExtractNameSpace(tree, "EnumSamples");
			EnumElement actual = Extract<EnumElement>(tree, "PublicEnum");
			Verify(actual, parent, "PublicEnum");

			Verify(Extract<EnumElement>(tree, "InternalEnum"), parent, "InternalEnum");

			parent = Extract<ClassElement>(tree, "Envelope");
			Verify(Extract<EnumElement>(tree, "ProtectedEnum"), parent, "ProtectedEnum");
			Verify(Extract<EnumElement>(tree, "ProtectedInternalEnum"), parent, "ProtectedInternalEnum");
			Verify(Extract<EnumElement>(tree, "PrivateProtectedEnum"), parent, "PrivateProtectedEnum");
		}

		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void Delegate()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("DelegateSamples.cs");

			var tree = mapper.Build(root, storage);
			Verify(tree);

			IDiscriminatedElement parent = ExtractNameSpace(tree, "SharpSourceFinderCoreTests", "Samples");

			void verify(string name)
			{
				var actual = Extract<DelegateElement>(tree, name);
				actual.Parent.IsSameReferenceAs(parent);

				actual.Identity.Identities.First().Name.Is(name);
			}

			verify("Public");
			verify("Internal");
			verify("Unsafe");

			parent = Extract<ClassElement>(tree, "DelegateSamples");
			verify("Protected");
			verify("ProtectedInternal");
			verify("PrivateProtected");
			verify("Private");
		}

		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void Interface()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("InterfaceSamples.cs");

			var tree = mapper.Build(root, storage);
			Verify(tree);
			IDiscriminatedElement parent = tree.Children().Skip(1).First();

			void verify(string name)
			{
				var actual = Extract<InterfaceElement>(tree, name);
				actual.Parent.IsSameReferenceAs(parent);

				actual.Identity.Identities.First().Name.Is(name);
			}

			verify("IPublic");
			verify("IInternal");
			verify("IUnsafe");
			verify("IPartial");

			parent = Extract<ClassElement>(tree, "Envelope");
			verify("IProtected");
			verify("IPrivateProtected");
			verify("IProtectedInternal");
			verify("IPrivate");
		}

		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void Struct()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("StructSamples.cs");

			var tree = mapper.Build(root, storage);
			Verify(tree);
			IDiscriminatedElement parent = tree.Children().OfType<NameSpaceElement>().First();

			void verify(string name)
			{
				var actual = Extract<StructElement>(tree, name);
				actual.Parent.Is(parent);
				actual.Identity.Identities.First().Name.Is(name);
			}

			verify("Public");
			verify("UnsafePartial");
			verify("Partial");
			verify("Internal");

			parent = Extract<ClassElement>(tree, "Envelope");
			verify("Private");
			verify("Protected");
			verify("ProtectedInternal");
			verify("PrivateProtected");
		}

		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void Class()
		{
			var mapper = new DiscriminatedElementTreeBuilder();
			var (root, storage) = BuildSyntaxTree("ClassSamples.cs");
			var tree = mapper.Build(root, storage);
			Verify(tree);

			IDiscriminatedElement parent = tree.Children().Skip(1).First();

			void verify(string name)
			{
				var actual = Extract<ClassElement>(tree, name);
				actual.Parent.Is(parent);
				actual.Identity.Identities.First().Name.Is(name);
			}

			verify("Public");
			verify("Internal");
			verify("Unsafe");
			verify("Partial");
			verify("Abstract");
			verify("Sealed");

			parent = Extract<ClassElement>(tree, "Envelope");
			verify("Private");
			verify("ProtectedInternal");
			verify("PrivateProtected");
			verify("Protected");
		}
	}
}