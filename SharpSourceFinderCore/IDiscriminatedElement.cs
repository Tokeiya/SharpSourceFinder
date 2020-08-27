using System.Collections.Generic;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IDiscriminatedElement
	{
		string Representation { get; }

		IDiscriminatedElement Parent { get; }

		public static bool IsImaginaryRoot(IDiscriminatedElement element) => element is ImaginaryRoot;

		void Describe(StringBuilder stringBuilder);
		string Describe();

		IEnumerable<IDiscriminatedElement> Ancestors();
		IEnumerable<IDiscriminatedElement> AncestorsAndSelf();

		IEnumerable<IDiscriminatedElement> Children();
		IEnumerable<IDiscriminatedElement> Descendants();
		IEnumerable<IDiscriminatedElement> DescendantsAndSelf();
	}
}