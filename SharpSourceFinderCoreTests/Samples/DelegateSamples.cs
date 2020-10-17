namespace SharpSourceFinderCoreTests.Samples
{
	public delegate void Public();

	internal delegate void Internal();

	public unsafe delegate int* Unsafe();
	
	public class DelegateSamples
	{
		protected delegate void Protected();

		protected internal delegate void ProtectedInternal();

		private protected delegate void PrivateProtected();

		private delegate void Private();
	}
}
