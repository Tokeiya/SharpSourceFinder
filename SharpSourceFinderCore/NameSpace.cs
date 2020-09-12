using System;
using System.Collections.Generic;
using System.Globalization;
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
			foreach (var elem in AncestorsAndSelf().OfType<NameSpace>())
			{
				buffer.Push(elem);
			}
		}

		public QualifiedName GetFullQualifiedName()
		{
			var buff = StackPool.Get();
			try
			{
				var root = Ancestors().OfType<SourceFile>().First();

				var ret = new QualifiedName(root);

				CollectNameAncestorsNameSpace(buff);

		
				while (buff.Count != 0)
				{
					var piv = buff.Pop();

					foreach (var elem in piv.Name.Children().Cast<IdentityName>())
					{
						ret.Add(elem.Identity);
					}
				}

				return ret;
			}
			finally
			{
				buff.Clear();
				StackPool.Return(buff);
			}


		}



		public override void Describe(StringBuilder stringBuilder)
		{
			stringBuilder.Append("namespace ");
			Name.Describe(stringBuilder);
			stringBuilder.Append("\n{\n");

			foreach (var elem in Children().Where(x=>!ReferenceEquals(x,Name)))
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
