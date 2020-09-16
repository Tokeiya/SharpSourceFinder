using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using FastEnumUtility;
using Tokeiya3.SharpSourceFinderCore;

namespace Playground
{



	class Program
	{
		static void Main(string[] args)
		{
			FastEnum.IsDefined<Accessibilities>(Accessibilities.Internal);
		}
	}
}