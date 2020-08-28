using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class UsingStaticDirective:UsingDirective
	{
		public UsingStaticDirective(IDiscriminatedElement parent, params string[] names) : base(parent, names)
		{
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			stringBuilder.Append("using static ");
			_qualifiedNames.Describe(stringBuilder);
			stringBuilder.Append(';');

		}
	}

}
