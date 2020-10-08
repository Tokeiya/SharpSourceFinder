using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Tokeiya3.SharpSourceFinderCore;

namespace Playground
{
	public delegate int IntegerBinaryOperation(int x, int y);


	class Program
	{
		static void Main(string[] args)
		{
			var expected = new List<IDiscriminatedElement>();

			var sample = new NameSpace(new PhysicalStorage("PathA"));
			var name = new QualifiedElement(sample);
			var elem = new IdentityElement(name, "Tokeiya3");

			expected.Add(name);
			expected.Add(elem);


			var a = new NameSpace(sample);
			name = new QualifiedElement(a);
			elem = new IdentityElement(name, "SharpSourceFinder");

			expected.Add(a);
			expected.Add(name);
			expected.Add(elem);


			var hoge = a.Descendants().ToArray();
			var act = sample.Descendants().ToArray();


		}

	}
}