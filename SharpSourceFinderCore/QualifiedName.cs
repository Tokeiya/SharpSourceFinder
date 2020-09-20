using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokeiya3.StringManipulator;

namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IQualified
	{
		IReadOnlyList<IIdentity> Identities { get; }
	}
	public sealed class QualifiedName : MultiDescendantsElement<IdentityName>,IQualified
	{
		internal QualifiedName()
		{
		}
		internal QualifiedName(IDiscriminatedElement parent) : base(parent)
		{
		}

		public override void Describe(StringBuilder stringBuilder, string indent, int depth)
		{
			AppendIndent(stringBuilder, indent, depth);

			foreach (var elem in ChildElements)
			{
				elem.Describe(stringBuilder, string.Empty, 0);
				stringBuilder.Append('.');
			}

			stringBuilder.Extract(..(stringBuilder.Length - 1));
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator)
		{
			foreach (var elem in ChildElements.Reverse())
			{
				elem.AggregateIdentities(accumulator);
			}
		}

		public IReadOnlyList<IIdentity> Identities => ChildElements;
	}
}