namespace NameSpace
{
	public struct Public
	{
		public public (int value)
		{}

		public int Field;
		public int Prop { get; set; }

		public override string ToString()
		{
			return "Hello world";
		}
	}

	public unsafe struct Unsafe
	{

	}

	public unsafe partial struct UnsafePartial
	{

	}

	public partial struct Partial
	{

	}

	internal struct Internal
	{

	}

	public class Envelope
	{
		private struct Private
		{
			
		}

		protected struct Protected
		{
			
		}

		protected internal struct ProtectedInternal
		{

		}

		private protected struct PrivateProtected
		{
			
		}
	}
}
