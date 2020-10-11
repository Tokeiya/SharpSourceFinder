using System;
using System.Collections.Generic;
using System.Linq;
using Tokeiya3.SharpSourceFinderCore;

namespace Playground
{


	class Program
	{
		static QualifiedElement AttachName(IDiscriminatedElement element, params string[] names)
		{
			var ret = new QualifiedElement(element);

			foreach (var name in names)
			{
				_ = new IdentityElement(ret, name);

			}

			return ret;
		}
		static void Main(string[] args)
		{
			var ns = new NameSpace(new PhysicalStorage(@"C:\Hoge\Piyo.cs"));
			AttachName(ns, "NameSpace");

			var sample = new ClassElement(ns, ScopeCategories.Public, false, false, false, false, false);
			AttachName(sample, "Sample");


			IDiscriminatedElement child =
				new ClassElement(sample, ScopeCategories.Public, false, false, false, false, false);
			AttachName(child, "ChildClass");

			child = new StructElement(sample, ScopeCategories.Public, false, false);
			AttachName(child, "ChildStruct");

			child = new StructElement(child, ScopeCategories.Public, false, false);
			AttachName(child, "InnerInner");



			foreach (var elem in sample.Children())
			{
				var txt = elem switch
				{
					QualifiedElement _=>"QualifiedElement",
					IdentityElement id=>$"Identity:{id.Name}",
					ClassElement _=>"Class",
					StructElement _=>"Struct",
					NameSpace _=>"NameSpace",
					_=>"Unknown"
			};

				Console.WriteLine(txt);
			}

		}
	}
}