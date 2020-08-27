using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public class UsingDirective : DiscriminatedElement
	{
		protected readonly QualifiedName _qualifiedNames;

		public UsingDirective(IDiscriminatedElement parent, params string[] names) : base(parent, string.Empty)
		{
			_qualifiedNames = new QualifiedName(this);

			foreach (var name in names)
			{
				_qualifiedNames.Add(name);
			}
		}
		public override void Describe(StringBuilder stringBuilder)
		{
			stringBuilder.Append("using ");
			_qualifiedNames.Describe(stringBuilder);
			stringBuilder.Append(";");
		}

		public override IEnumerable<IDiscriminatedElement> Children()
		{
			yield return _qualifiedNames;
		}

		public override IEnumerable<IDiscriminatedElement> Descendants() => _qualifiedNames.DescendantsAndSelf();
	}
}
