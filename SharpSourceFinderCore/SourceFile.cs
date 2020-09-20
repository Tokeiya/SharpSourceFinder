using System;
using System.Collections.Generic;
using System.Text;


namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class SourceFile : MultiDescendantsElement<IDiscriminatedElement>
	{
		public SourceFile(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException($"{nameof(path)} is invalid.");
			Path = path;
		}

		public string Path { get; }




		public override void Describe(StringBuilder stringBuilder, string indent, int depth)
		{
			foreach (var elem in ChildElements) elem.Describe(stringBuilder, indent, 0);
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
		}

		public override bool IsIndividualEquivalentTo(IDiscriminatedElement element)
		{
#warning IsIndividualEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsIndividualEquivalentTo is not implemented");
		}
	}
}