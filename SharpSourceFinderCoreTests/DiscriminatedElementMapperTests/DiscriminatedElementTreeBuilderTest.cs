using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using ChainingAssertion;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SharpSourceFinderCoreTests.DiscriminatedElementMapperTests
{
	public class DiscriminatedElementTreeBuilderTest
	{
		private readonly ITestOutputHelper _output;

		public DiscriminatedElementTreeBuilderTest(ITestOutputHelper output) => _output = output;

		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void NameSpaceOnlyTest()
		{
			var root = CSharpSyntaxTree.ParseText(File.ReadAllText("Samples\\NameSpaceOnly.cs")).GetCompilationUnitRoot();
			var builder = new DiscriminatedElementTreeBuilder();
			var storage = new PhysicalStorage("Samples\\NameSpaceOnly.cs");


			var sample = builder.Build(root, storage);

			sample.Storage.IsSameReferenceAs(storage);
			ImaginaryRoot.IsImaginaryRoot(sample.Parent).IsTrue();
			sample.Identity.Identities.Count.Is(0);

			var children = sample.Children().ToArray();
			children.Length.Is(2);

			sample = (NameSpace)children[1];
			var actual = sample.GetQualifiedName();

			actual.Identities.Count.Is(3);

			actual.Identities[0].Name.Is("NameSpace");
			actual.Identities[1].Name.Is("Hoge");
			actual.Identities[2].Name.Is("Moge");
		}


		[Trait("TestLayer", nameof(DiscriminatedElementTreeBuilder))]
		[Fact]
		public void ClassBuildTest()
		{
			var root = CSharpSyntaxTree.ParseText(File.ReadAllText("Samples\\CLassSamples.cs")).GetCompilationUnitRoot();
			var builder = new DiscriminatedElementTreeBuilder();
			var storage = new PhysicalStorage("Samples\\ClassSamples.cs");

			var sample = builder.Build(root, storage);

			sample.DescendantsAndSelf().OfType<NameSpace>().Count().Is(2);

			var result = sample.DescendantsAndSelf().OfType<ClassElement>().ToArray();
			result.Length.Is(11);

			var envelope = result.First(elem => elem.Identity.Identities[0].Name == "Envelope");
			envelope.Children().Count().Is(4);

			var nested = envelope.Children().OfType<ClassElement>()
				.First(c => c.Identity.Identities[0].Name == "Protected");

			nested.Scope.Is(ScopeCategories.Protected);





		}


	}
}
