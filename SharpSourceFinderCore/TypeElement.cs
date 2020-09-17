//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using Microsoft.Extensions.ObjectPool;

//namespace Tokeiya3.SharpSourceFinderCore
//{
//	[Flags]
//	public enum TypeAttributes
//	{
//		None=0x00,
//		Partial=0x01,
//		Unsafe=0x02,
//		UnsafePartial=Partial|Unsafe
//	}

//	public abstract class TypeElement:MultiDescendantsElement<IDiscriminatedElement>,IEquatable<TypeElement>
//	{
//		private static readonly ObjectPool<Stack<IDiscriminatedElement>> StackPool = new DefaultObjectPool<Stack<IDiscriminatedElement>>(new DefaultPooledObjectPolicy<Stack<IDiscriminatedElement>>());

//		private TypeElement(IDiscriminatedElement parent, Accessibilities accessibility,TypeAttributes attribute) : base(parent)
//		{
//			if (!EnumChecker<TypeAttributes>.Verify(attribute)) throw new ArgumentOutOfRangeException(nameof(attribute));

//			TypeAttribute = TypeAttribute;


//			if(!EnumChecker<Accessibilities>.Verify(accessibility)) throw new ArgumentException($"{nameof(accessibility)} is unexpected value.");

//			Accessibility = accessibility;
//		}

//		protected TypeElement(NameSpace parent, Accessibilities accessibility, TypeAttributes attribute) : this((IDiscriminatedElement)parent,accessibility,attribute)
//		{

//		}

//		protected TypeElement(TypeElement parent, Accessibilities accessibility, TypeAttributes attribute) : this((IDiscriminatedElement)parent,accessibility,attribute)
//		{

//		}


//		public QualifiedName GetFullQualifiedName()
//		{
//			IDiscriminatedElement findOrigin()
//			{
//				IDiscriminatedElement recent=this;

//				foreach (var elem in Ancestors())
//				{
//					if (IDiscriminatedElement.IsImaginaryRoot(elem)) break;
//					recent = elem;
//				}

//				return recent;
//			}

//			var storage = StackPool.Get();

//			try
//			{
//				foreach (var elem in AncestorsAndSelf().Where(x => x is NameSpace || x is TypeElement))
//				{
//					storage.Push(elem);
//				}

//				var ret = new QualifiedName(findOrigin());

//				while (storage.Count != 0)
//				{
//					switch (storage.Pop())
//					{
//						case NameSpace ns:
//							ret.Add(ns.Name);
//							break;

//						case TypeElement te:
//							ret.Add(te.Name);
//							break;

//						default:
//							throw new InvalidOperationException();
//					}
//				}

//				return ret;
//			}
//			finally
//			{
//				Trace.Assert(storage.Count == 0);
//				StackPool.Return(storage);
//			}
//		}

//		public IdentityName Name { get; }
//		public Accessibilities Accessibility { get; }

//		public TypeAttributes TypeAttribute{ get; }

//		public override int GetHashCode() => GetFullQualifiedName().GetHashCode();

//		public override bool Equals(object obj) => obj switch
//		{
//			TypeElement elem => elem == this,
//			_ => false
//		};

//		public bool Equals(TypeElement? other) => other switch
//		{
//			null=>false,
//			{} =>other==this
//		};

//		public static bool operator ==(TypeElement x, TypeElement y) =>
//			x.GetFullQualifiedName() == y.GetFullQualifiedName();

//		public static bool operator !=(TypeElement x, TypeElement y) => !(x == y);
//	}
//}

