using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;
using Xunit.Abstractions;
using static Xunit.Assert;

namespace SharpSourceFinderCoreTests.DiscriminatedElementTests
{
	public class ImaginaryRootTest
	{
		private readonly ITestOutputHelper _output;

		public ImaginaryRootTest(ITestOutputHelper output) => _output = output;


		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void ParentGetterTest() => Throws<NotSupportedException>(() => ImaginaryRoot.Root.Parent);

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void StorageGetterTest() => Throws<NotSupportedException>(() => ImaginaryRoot.Root.Storage);

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void RegisterChildTest()
		{
			var sample = new NameSpaceElement(new PhysicalStorage("hoge"));
			Throws<NotSupportedException>(() => ImaginaryRoot.Root.RegisterChild(sample));
		}


		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void AncestorsTest() => ImaginaryRoot.Root.Ancestors().IsEmpty();

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void AncestorsAndSelfTest() =>
			Throws<NotSupportedException>(() => ImaginaryRoot.Root.AncestorsAndSelf().Any());

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void ChildrenTest() => Throws<NotSupportedException>(() => ImaginaryRoot.Root.Children().Any());

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void DescendantsTest() => Throws<NotSupportedException>(() => ImaginaryRoot.Root.Descendants().Any());

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void DescendantsAndSelfTest() =>
			Throws<NotSupportedException>(() => ImaginaryRoot.Root.DescendantsAndSelf().Any());

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void GetQualifiedNameTest()
		{
			object? dmy = null;
			Throws<NotSupportedException>(() => dmy = ImaginaryRoot.Root.GetQualifiedName());

			dmy.IsNull();
		}

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void AggregateIdentitiesTest()
		{
			var stack = new Stack<(ScopeCategories, IdentityCategories, string)>();
			ImaginaryRoot.Root.AggregateIdentities(stack);

			stack.IsEmpty();
		}

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void IsLogicallyEquivalentToTest()
		{
			var x = ImaginaryRoot.Root;
			var y = ImaginaryRoot.Root;
			var z = ImaginaryRoot.Root;


			//Symmetrically
			x.IsLogicallyEquivalentTo(x).IsTrue();


			//Transitively
			x.IsLogicallyEquivalentTo(y).IsTrue();
			y.IsLogicallyEquivalentTo(z).IsTrue();
			x.IsLogicallyEquivalentTo(z).IsTrue();

			//Reflexively
			x.IsLogicallyEquivalentTo(x).IsTrue();

			//Inequivalent
			var a = new NameSpaceElement();
			x.IsLogicallyEquivalentTo(a).IsFalse();
			x.IsPhysicallyEquivalentTo(a).IsFalse();

			a.IsLogicallyEquivalentTo(x).IsFalse();
			a.IsPhysicallyEquivalentTo(x).IsFalse();
		}

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void IsPhysicallyEquivalentToTest()
		{
			var x = ImaginaryRoot.Root;
			var y = ImaginaryRoot.Root;
			var z = ImaginaryRoot.Root;


			//Symmetrically
			x.IsLogicallyEquivalentTo(x).IsTrue();
			x.IsPhysicallyEquivalentTo(x).IsTrue();

			//Transitively
			x.IsLogicallyEquivalentTo(y).IsTrue();
			x.IsPhysicallyEquivalentTo(y).IsTrue();

			y.IsLogicallyEquivalentTo(z).IsTrue();
			y.IsPhysicallyEquivalentTo(z).IsTrue();

			x.IsLogicallyEquivalentTo(z).IsTrue();
			y.IsPhysicallyEquivalentTo(z).IsTrue();

			//Reflexively
			x.IsLogicallyEquivalentTo(x).IsTrue();
			x.IsPhysicallyEquivalentTo(x).IsTrue();

			//Inequivalent
			var a = new NameSpaceElement();
			x.IsLogicallyEquivalentTo(a).IsFalse();
			a.IsLogicallyEquivalentTo(x).IsFalse();
		}

		[Trait("TestLayer", nameof(ImaginaryRoot))]
		[Fact]
		public void IsImaginaryRootTest()
		{
			IDiscriminatedElement sample = ImaginaryRoot.Root;
			ImaginaryRoot.IsImaginaryRoot(sample).IsTrue();

			sample = new NameSpaceElement();
			ImaginaryRoot.IsImaginaryRoot(sample).IsFalse();
		}
	}
}