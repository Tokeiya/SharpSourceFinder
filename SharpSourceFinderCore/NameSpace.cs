using System;
using System.Linq;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameSpace : MultiDescendantsElement<IDiscriminatedElement>, IEquatable<NameSpace>
	{


		public QualifiedName Name { get; }



		public NameSpace(IDiscriminatedElement parent) : base(parent) => Name = new QualifiedName(this);


		public QualifiedName GetFullQualifiedName()
		{
			var root = Ancestors().OfType<SourceFile>().FirstOrDefault() ?? DiscriminatedElement.Root;
			var ret = new QualifiedName(root);

			foreach (var elem in Ancestors().OfType<NameSpace>())
			{
				foreach (var identityName in elem.Name.Children().Cast<IdentityName>())
				{
					ret.Add(identityName.Identity);
				}
			}

			return ret;
		}

		public override void Describe(StringBuilder stringBuilder)
		{
			stringBuilder.Append("namespace ");
			Name.Describe(stringBuilder);
			stringBuilder.Append("\n{\n");

			foreach (var elem in ChildElements)
			{
				elem.Describe(stringBuilder);
			}

			stringBuilder.Append("}\n");
		}

		public bool Equals(NameSpace? other) => other switch
		{
			null => false,
			{ } when ReferenceEquals(this, other) => true,
			_ => this == other
		};


		public override bool Equals(object? obj) => obj switch
		{
			null => false,
			{ } when ReferenceEquals(this, obj) => true,
			NameSpace other => this == other,
			_ => false
		};

		public override int GetHashCode()
		{
			var ret = Name.GetHashCode();

			foreach (var elem in Ancestors().OfType<NameSpace>())
			{
				ret ^= elem.GetHashCode();
			}

			return ret;
		}

		public static bool operator ==(NameSpace x, NameSpace y)
		{
			var xx = x.GetFullQualifiedName();
			var yy = y.GetFullQualifiedName();

			return xx == yy;
		}

		public static bool operator !=(NameSpace x, NameSpace y) => !(x == y);

	}
}
