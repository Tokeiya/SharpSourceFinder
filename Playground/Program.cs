using System;
using System.Runtime.InteropServices.ComTypes;

namespace Playground
{
	class Program
	{
		static void Main(string[] args)
		{
			var root = new Folder(null, "root");

			for (int i = 0; i < 5; i++)
			{
				var elem = root.AddChild(i.ToString());

				for (int j = 0; j < 5; j++)
				{
					elem.AddChild((j + 10).ToString());
				}
			}

			foreach (var elem in root.FullScratch())
			{
				Console.WriteLine($"{elem?.Root?.Path??"root"} {elem.Path}");	
			}
		}
	}
}
