using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ChainingAssertion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.ObjectPool;
using Npgsql;
using Tokeiya3.SharpSourceFinderCore;
using Tokeiya3.StringManipulator;

namespace Playground
{
	public static class Formatter
	{
		private static readonly DefaultObjectPool<StringBuilder> StringBuilderPool = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());
		private static readonly DefaultObjectPool<DiscriminatedElementTreeBuilder> TreeBuilderPool = new DefaultObjectPool<DiscriminatedElementTreeBuilder>(new DefaultPooledObjectPolicy<DiscriminatedElementTreeBuilder>());



		static string Encode(byte value)
		{
			if ((value & 8) != 0)
			{
				if ((value & 4) != 0)
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "++++";
						else return "+++-";
					}
					else
					{
						if ((value & 1) != 0) return "++-+";
						else return "++--";
					}
				}
				else
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "+-++";
						else return "+-+-";
					}
					else
					{
						if ((value & 1) != 0) return "+--+";
						else return "+---";
					}
				}
			}
			else
			{
				if ((value & 4) != 0)
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "-+++";
						else return "-++-";
					}
					else
					{
						if ((value & 1) != 0) return "-+-+";
						else return "-+--";
					}
				}
				else
				{
					if ((value & 2) != 0)
					{
						if ((value & 1) != 0) return "--++";
						else return "--+-";
					}
					else
					{
						if ((value & 1) != 0) return "---+";
						else return "----";
					}
				}
			}

		}

		static string GenerateQualifiedText(IEnumerable<IIdentity> value)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			if (!value.Any()) return string.Empty;

			var bld = StringBuilderPool.Get();
			try
			{
				foreach (var elem in value)
				{
					bld.Append(elem.Name);
					bld.Append(Encode((byte)elem.Category));
					bld.Append(Encode((byte)elem.Scope));
					bld.Append('.');
				}

				return bld.Extract(..^1).ToString();
			}
			finally
			{
				StringBuilderPool.Return(bld);
			}
		}

		static void GenerateRecord(string filepath, ScopeCategories scope, IdentityCategories entityCategory,
			string name, bool isUnsafe, bool isPartial, bool isStatic, bool isAbstract, bool isSealed,
			string fullQualified, string parent,StreamWriter writer)
		{
			static void writeField<T>(T value, StringBuilder builder) => builder.Append(value).Append('\t');

			static string encodeScope(ScopeCategories scope) => scope switch
			{
				ScopeCategories.Internal => "internal",
				ScopeCategories.Private => "private",
				ScopeCategories.PrivateProtected => "private_protected",
				ScopeCategories.Protected => "protected",
				ScopeCategories.ProtectedInternal => "protected_internal",
				ScopeCategories.Public => "public",
				_ => throw new InvalidOperationException($"{scope} is unexpected.")
			};

			static string encodeCategory(IdentityCategories category) => category switch
			{
				IdentityCategories.Class=>"class",
				IdentityCategories.Delegate=>"delegate",
				IdentityCategories.Enum=>"enum",
				IdentityCategories.Interface=>"interface",
				IdentityCategories.Namespace=>"namespace",
				IdentityCategories.Struct=>"struct",
				_=> throw new InvalidOperationException($"{category} is unexpected")
			};

			var bld = StringBuilderPool.Get();

			try
			{
				writeField(filepath, bld);
				writeField(encodeScope(scope), bld);
				writeField(encodeCategory(entityCategory), bld);
				writeField(name, bld);
				writeField(isUnsafe, bld);
				writeField(isPartial, bld);
				writeField(isStatic, bld);
				writeField(isAbstract, bld);
				writeField(isSealed, bld);
				writeField(fullQualified, bld);
				writeField(parent, bld);

				writer.WriteLine(bld.Extract(..^1).ToString());

			}
			finally
			{
				StringBuilderPool.Return(bld);
			}
		}

		static void GenerateRecord(string filePath, NameSpaceElement element, StreamWriter writer)
		{
			static void proc(string path, string name, IEnumerable<IIdentity> identity,
				IEnumerable<IIdentity> parent, StreamWriter wtr) => GenerateRecord(path, ScopeCategories.Public,
				IdentityCategories.Namespace, name, false, true, false, false, false, GenerateQualifiedText(identity),
				GenerateQualifiedText(parent), wtr);

			var identities = element.Identity.Identities;

			var parent = (element.Parent is ImaginaryRoot)
				? Array.Empty<IIdentity>()
				: element.Parent.GetQualifiedName().Identities;



			for (int i = 0; i < identities.Count; i++)
			{
				proc(filePath, identities[i].Name, parent.Concat(identities.Take(i + 1)),
					parent.Concat(identities.Take(i)), writer);
			}

		}


		static void GenerateRecord(string filePath,TypeElement element,StreamWriter writer)
		{
			var cat = element switch
			{
				ClassElement _ => IdentityCategories.Class,
				StructElement _ => IdentityCategories.Struct,
				InterfaceElement _ => IdentityCategories.Interface,
				EnumElement _ => IdentityCategories.Enum,
				DelegateElement _ => IdentityCategories.Delegate,
				_ => throw new InvalidOperationException($"{element.GetType().Name} is unexpected type.")
			};

			var tmp = element.GetQualifiedName().Identities;


			GenerateRecord(filePath, element.Scope, cat, element.Identity.Identities[0].Name, element.IsUnsafe,
				element.IsPartial, element.IsStatic, element.IsAbstract, element.IsSealed, GenerateQualifiedText(tmp), GenerateQualifiedText(tmp.Take(tmp.Count - 1)), writer);
		}



		public static void Write(string filePath,StreamWriter writer)
		{
			//storage_path	scope	type	name	is_unsafe	is_partial	is_static	is_abstract	is_sealed	full_qualified	parent_qualified
			var bld = TreeBuilderPool.Get();


			try
			{
				var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(filePath)).GetCompilationUnitRoot();
				var root = bld.Build(tree, new PhysicalStorage(filePath));

				foreach (var elem in root.Descendants())
				{
					if (elem is NameSpaceElement ns) GenerateRecord(filePath, ns, writer);
					else if (elem is TypeElement te) GenerateRecord(filePath, te, writer);
					else continue;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("");
				Console.WriteLine(ex.Message);
				Console.ReadLine();
			}
			finally
			{
				TreeBuilderPool.Return(bld);
			}
		}

	}
}
