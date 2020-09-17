//using System.Collections.Generic;
//using Tokeiya3.SharpSourceFinderCore;
//using Xunit.Abstractions;

//namespace SharpSourceFinderCoreTests.Old
//{
//	public class ClassTypeTests:TypeElementTest
//	{
//		private readonly NameSpace _nsA;
//		private readonly NameSpace _nsB;

//		private readonly ClassType _clsA;
//		private readonly ClassType _clsB;


//		public ClassTypeTests(ITestOutputHelper output):base(output)
//		{
//			_nsA = new NameSpace(new SourceFile(@"C:\Foo\Bar.cs"));
//			_nsA.Name.Add("A");

//			_nsB = new NameSpace(new SourceFile(@"C:\Hoge\Piyo.cs"));
//			_nsB.Name.Add("B");

//			_clsA = new ClassType(_nsA, Accessibilities.Public, TypeAttributes.None, "ClassA");
//			_clsB = new ClassType(_clsB, Accessibilities.Public, TypeAttributes.None, "ClassB");
//		}

//		protected override IEnumerable<(TypeElement x, TypeElement y, TypeElement z)> CreateTransitivelyTestSamples()
//		{
//			var x = new ClassType(_nsA, Accessibilities.Public, TypeAttributes.None, "Foo");
//			var y = new ClassType(_nsA, Accessibilities.Public, TypeAttributes.None, "Foo");
//			var z = new ClassType(_nsA, Accessibilities.Public, TypeAttributes.None, "Foo");

//			yield return (x, y, z);


//			throw new System.NotImplementedException();
//		}

//		protected override IEnumerable<(TypeElement x, TypeElement y)> CreateInEqualTestSamples()
//		{
//			throw new System.NotImplementedException();
//		}

//		protected override IEnumerable<(TypeElement actual, QualifiedName expected)> GetQualifiedNameSamples()
//		{
//			throw new System.NotImplementedException();
//		}

//		protected override IEnumerable<(TypeElement actual, Accessibilities expected)> GetAccessibilitiesSamples()
//		{
//			throw new System.NotImplementedException();
//		}

//		protected override IEnumerable<(TypeElement actual, TypeAttributes expected)> GetTypeAttributeSamples()
//		{
//			throw new System.NotImplementedException();
//		}

//	}
//}

