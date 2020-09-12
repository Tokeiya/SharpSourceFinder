using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Tokeiya3.SharpSourceFinderCore
{
	public sealed class NameSpace : MultiDescendantsElement<IDiscriminatedElement>, IEquatable<NameSpace>
	{
		private static readonly ObjectPool<Stack<NameSpace>> StackPool = new DefaultObjectPool<Stack<NameSpace>>(new DefaultPooledObjectPolicy<Stack<NameSpace>>());


		public QualifiedName Name { get; }



		public NameSpace(IDiscriminatedElement parent) : base(parent) => Name = new QualifiedName(this);

		private void CollectNameAncestorsNameSpace(Stack<NameSpace> buffer)
		{
#warning CollectNameAncestorsNameSpace_Is_NotImpl
			throw new NotImplementedException("CollectNameAncestorsNameSpace is not implemented");
		}

		public QualifiedName GetFullQualifiedName()
		{
#warning GetFullQualifiedName_Is_NotImpl
			throw new NotImplementedException("GetFullQualifiedName is not implemented");
		}

		public override void Describe(StringBuilder stringBuilder)
		{
#warning Describe_Is_NotImpl
			throw new NotImplementedException("Describe is not implemented");
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
