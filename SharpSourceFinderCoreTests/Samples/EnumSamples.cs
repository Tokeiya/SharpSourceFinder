
namespace EnumSamples
{
	public enum PublicEnum:int
	{
		Hoge=10,
		Piyo,
		Foo,
		Bar
	}

	internal enum InternalEnum
	{

	}

	public class Envelope
	{
		protected enum ProtectedEnum
		{

		}

		protected internal enum ProtectedInternalEnum
		{

		}

		private protected enum PrivateProtectedEnum
		{

		}
	}
}
