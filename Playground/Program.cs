using System;
using System.IO;


namespace Playground
{
	class Program
	{


		static void Main()
		{
			using var writer = new StreamWriter("G:\\runtime.tsv");

			foreach (var file in Directory.EnumerateFiles("H:\\runtime","*.cs",SearchOption.AllDirectories))
			{
				Console.WriteLine(file);
				Formatter.Write(file, writer);

			}
		}
	}
}