using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Tokeiya3.SharpSourceFinderCore
{
	public abstract class TypeElement:MultiDescendantsElement<IDiscriminatedElement>,IEquatable<TypeElement>
	{
		private static readonly ObjectPool<Stack<IDiscriminatedElement>> StackPool = new DefaultObjectPool<Stack<IDiscriminatedElement>>(new DefaultPooledObjectPolicy<Stack<IDiscriminatedElement>>());

		private TypeElement(IDiscriminatedElement parent, Accessibilities accessibility,bool isPartial, string name) : base(parent)
		{
			Name = new IdentityName(this, name);
			IsPartial = isPartial;


			if(!EnumChecker<Accessibilities>.Verify(accessibility)) throw new ArgumentException($"{nameof(accessibility)} is unexpected value.");

			Accessibility = accessibility;
		}

		protected TypeElement(NameSpace parent, Accessibilities accessibility, bool isPartial, string name) : this((IDiscriminatedElement)parent,accessibility,isPartial, name)
		{

		}

		protected TypeElement(TypeElement parent, Accessibilities accessibility, bool isPartial, string name) : this((IDiscriminatedElement)parent,accessibility,isPartial,name)
		{

		}


		public QualifiedName GetFullQualifiedName()
		{
#warning GetFullQualifiedName_Is_NotImpl
			throw new NotImplementedException("GetFullQualifiedName is not implemented");
		}

		public IdentityName Name { get; }
		public Accessibilities Accessibility { get; }

		public bool IsPartial { get; }

		public override int GetHashCode() => GetFullQualifiedName().GetHashCode();

		public override bool Equals(object obj) => obj switch
		{
			TypeElement elem => elem == this,
			_ => false
		};

		public bool Equals(TypeElement? other) => other switch
		{
			{} when other is null=>false,
			{} =>other==this
		};

		public static bool operator ==(TypeElement x, TypeElement y) =>
			x.GetFullQualifiedName() == y.GetFullQualifiedName();

		public static bool operator !=(TypeElement x, TypeElement y) => !(x == y);
	}
}
