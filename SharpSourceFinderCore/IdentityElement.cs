using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using FastEnumUtility;

namespace Tokeiya3.SharpSourceFinderCore
{
	[Serializable]
	public class CategoryNotFoundException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//



		public CategoryNotFoundException(string name) : base($"{nameof(name)} category not found.")
		{
		}

		protected CategoryNotFoundException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
	public sealed class IdentityElement : TerminalElement, IIdentity
	{
		public IdentityElement(QualifiedElement from, IdentityCategories category, string name) : base(from)
		{
			if (!category.IsDefined()) throw new ArgumentException($"{nameof(category)} is unexpected value");
			Category = category;
			Name = name;
		}

		public IdentityElement(QualifiedElement parent, string name) : base(parent)
		{
			var piv = parent.Parent;
			IdentityCategories? cat=default;

			while (!(piv is ImaginaryRoot))
			{
				cat = piv switch
				{
					NameSpace _ => IdentityCategories.Namespace,
					ClassElement _ => IdentityCategories.Class,
					StructElement _ => IdentityCategories.Struct,
					InterfaceElement _ => IdentityCategories.Interface,
					EnumElement _ => IdentityCategories.Enum,
					DelegateElement _ => IdentityCategories.Delegate,
					_ => null
				};
				
				piv = piv.Parent;

				if(!(cat is null)) continue;
				break;
			}

			Category = cat ?? throw new CategoryNotFoundException(name);
		}


		public override QualifiedElement GetQualifiedName()
		{
#warning GetQualifiedName_Is_NotImpl
			throw new NotImplementedException("GetQualifiedName is not implemented");
		}

		public override void AggregateIdentities(Stack<(IdentityCategories category, string identity)> accumulator) => accumulator.Push((Category, Name));
		public override bool IsLogicallyEquivalentTo(IDiscriminatedElement other)
		{
#warning IsLogicallyEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsLogicallyEquivalentTo is not implemented");
		}

		public int Order { get; internal set; }

		public IdentityCategories Category { get; }
		public string Name { get; }
		public IQualified From { get; }
		public bool IsEquivalentTo(IIdentity identity)
		{
#warning IsEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsEquivalentTo is not implemented");
		}

		public bool IsPhysicallyEquivalentTo(IIdentity identity)
		{
#warning IsPhysicallyEquivalentTo_Is_NotImpl
			throw new NotImplementedException("IsPhysicallyEquivalentTo is not implemented");
		}
	}
}