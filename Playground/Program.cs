using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp;
using Npgsql;
using NpgsqlTypes;
using Tokeiya3.SharpSourceFinderCore;


namespace Playground
{
	public class Record
	{
		public Record(string record)
		{
			var ary = record.Split('\t');
			if (ary.Length != 12) throw new ArgumentException("Unexpected record format.");

			StoragePath = ary[0];

			ScopeAsString = ary[1];
			IdentityAsString = ary[2];
			Depth = int.Parse(ary[3]);
			Name = ary[4];
			IsUnsafe = bool.Parse(ary[5]);
			IsPartial = bool.Parse(ary[6]);
			IsStatic = bool.Parse(ary[7]);
			IsAbstract = bool.Parse(ary[8]);
			IsSealed = bool.Parse(ary[9]);
			FullQualified = ary[10];
			ParentQualified = ary[11];
		}

		public string StoragePath { get; }

		public ScopeCategories Scope =>
			ScopeAsString switch
			{
				"internal" => ScopeCategories.Internal,
				"private" => ScopeCategories.Private,
				"private_protected" => ScopeCategories.PrivateProtected,
				"protected" => ScopeCategories.Protected,
				"protected_internal" => ScopeCategories.ProtectedInternal,
				"public" => ScopeCategories.Public,
				_ => throw new InvalidOperationException($"{ScopeAsString} is unexpected value.")
			};

		public string ScopeAsString { get; }

		public IdentityCategories Identity => IdentityAsString switch
		{
			"class" => IdentityCategories.Class,
			"delegate" => IdentityCategories.Delegate,
			"enum" => IdentityCategories.Enum,
			"interface" => IdentityCategories.Interface,
			"namespace" => IdentityCategories.Namespace,
			"struct" => IdentityCategories.Struct,
			_ => throw new InvalidOperationException($"{IdentityAsString} is unexpected value.")
		};


		public string IdentityAsString { get; }

		public int Depth { get; }
		public string Name { get; }
		public bool IsUnsafe { get; }
		public bool IsPartial { get; }
		public bool IsStatic { get; }
		public bool IsAbstract { get; }
		public bool IsSealed { get; }
		public string FullQualified { get; }

		public string ParentQualified { get; }

	}
	class Program
	{
		//storage_path	scope	type	depth	name	
		//is_unsafe	is_partial	is_static	is_abstract	is_sealed	full_qualified	parent_qualified

		static void WriteTsv(string sourcePath,string outputPath)
		{
			using var writer = new StreamWriter(outputPath);

			foreach (var file in Directory.EnumerateFiles(sourcePath,"*.cs", SearchOption.AllDirectories))
			{
				Console.WriteLine(file);
				Formatter.Write(file, writer);
			}
		}

		static void BulkInsert(NpgsqlConnection conn,string path)
		{
			using var writer = conn.BeginBinaryImport(
				"copy raw_runtime(storage_path,scope,entity_type,depth,name,is_unsafe,is_partial,is_static,is_abstract,is_sealed,full_qualified,parent) from stdin(format binary)");


			var cnt = 0;

			foreach (var rec in File.ReadLines(path))
			{
				if ((++cnt & 4095) == 0) Console.WriteLine($"{cnt}:{rec}");


				var cursor = new Record(rec);

				writer.StartRow();
				writer.Write(cursor.StoragePath);
				writer.Write(cursor.ScopeAsString);
				writer.Write(cursor.IdentityAsString);
				writer.Write(cursor.Depth, NpgsqlDbType.Integer);
				writer.Write(cursor.Name);
				writer.Write(cursor.IsUnsafe, NpgsqlDbType.Boolean);
				writer.Write(cursor.IsPartial, NpgsqlDbType.Boolean);
				writer.Write(cursor.IsStatic, NpgsqlDbType.Boolean);
				writer.Write(cursor.IsAbstract, NpgsqlDbType.Boolean);
				writer.Write(cursor.IsSealed, NpgsqlDbType.Boolean);
				writer.Write(cursor.FullQualified);
				writer.Write(cursor.ParentQualified);
			}

			writer.Complete();
		}



		static void Main()
		{
			//WriteTsv(@"H:\runtime", @"G:\runtime.tsv");
			var bld = new NpgsqlConnectionStringBuilder
			{
				Host = "192.168.2.102",
				Username = "tokeiya3",
				Database = "sharp_source_finder"
			};

			using var connection = new NpgsqlConnection(bld.ToString());
			connection.Open();

			BulkInsert(connection, @"G:\runtime.tsv");
		}
	}
}