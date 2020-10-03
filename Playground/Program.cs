using System;
using System.Xml;
using Tokeiya3.SharpSourceFinderCore;

namespace Playground
{
	public delegate int IntegerBinaryOperation(int x, int y);


	class Program
	{
		static void Main(string[] args)
		{
			var q = new QualifiedElement();
			_ = new IdentityElement(q, IdentityCategories.Namespace, "Hoge");
			_ = new IdentityElement(q, IdentityCategories.Namespace, "Bar");

			var qq = q.GetQualifiedName();



			for (int i = 0; i < q.Identities.Count; i++)
			{
				var a = q.Identities[i];
				var b = qq.Identities[i];
				Console.WriteLine($"{a.Name},{b.Name},{a.Name==b.Name}");
				Console.WriteLine($"{a.Category},{b.Category},{a.Category==b.Category}");
				Console.WriteLine($"{a.Order},{b.Order},{a.Order==b.Order}");
				Console.WriteLine("");
			}

		}

	}
}