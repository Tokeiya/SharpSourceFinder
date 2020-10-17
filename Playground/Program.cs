
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Console = System.Console;

namespace Playground
{

	class Program
	{
		static void Main()
		{
			var stack = new Stack<string>();
			stack.Push("C:\\");
			stack.Push("D:\\");
			stack.Push("G:\\");
			stack.Push("H:\\");
			stack.Push("I:\\");

			var wtr = new StreamWriter("Output.tsv");

			var cnt = 0;

			while (stack.Count!=0)
			{
				try
				{
					var current = stack.Pop();

					foreach (var dir in Directory.EnumerateDirectories(current))
					{
						stack.Push(dir);
					}

					++cnt;
					wtr.WriteLine($"dir\t{current}");

					foreach (var file in Directory.EnumerateFiles(current))
					{
						++cnt;

						if(file.Contains('\uDC6D')) continue;
						wtr.WriteLine($"file\t{file}");

						if ((cnt & 65535) == 0) Console.WriteLine($"{cnt}:{stack.Count}:{file}");
					}
				}
				catch (UnauthorizedAccessException ex)
				{
					Console.WriteLine(ex.Message);
					continue;
				}
				catch (EncoderFallbackException ex)
				{
					Console.WriteLine(ex.Message);
					wtr.Close();
					wtr = new StreamWriter(new FileStream("Output.tsv", FileMode.Append));
				}
			}

			wtr.Close();
		}

	}
}