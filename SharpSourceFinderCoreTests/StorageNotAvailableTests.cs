using System;
using ChainingAssertion;
using Tokeiya3.SharpSourceFinderCore;
using Xunit;

namespace SharpSourceFinderCoreTests
{
	public class StorageNotAvailableTests
	{
		[Fact]
		public void IsEquivalentToTest()
		{
			var x = StorageNotAvailable.NotAvailable;
			var y = StorageNotAvailable.NotAvailable;
			var z = StorageNotAvailable.NotAvailable;

			//Transitively
			x.IsEquivalentTo(y).IsTrue();
			y.IsEquivalentTo(z).IsTrue();
			x.IsEquivalentTo(z).IsTrue();

			//Symmetrically
			x.IsEquivalentTo(y).IsTrue();
			y.IsEquivalentTo(x).IsTrue();

			//Reflexively
			x.IsEquivalentTo(x);
			y.IsEquivalentTo(y);


			//InEqualityTest
			var a = new PhysicalStorage("HogeMoge");
			x.IsEquivalentTo(a).IsFalse();
			a.IsEquivalentTo(x).IsFalse();
		}


		[Fact]
		public void GetPathTest()
		{
			Assert.Throws<NotSupportedException>(() => StorageNotAvailable.NotAvailable.Path);
		}


		[Fact]
		public void IsNotAvailableTest()
		{
			StorageNotAvailable.IsNotAvailable(StorageNotAvailable.NotAvailable).IsTrue();
			StorageNotAvailable.IsNotAvailable(new PhysicalStorage("HogeMoge")).IsFalse();
		}
	}
}