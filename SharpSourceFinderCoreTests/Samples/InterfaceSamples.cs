namespace NameSpace
{
	public interface IPublic{}
	internal interface IInternal{}

	public unsafe interface IUnsafe
	{
	}

	public partial interface IPartial{}

	public class Envelope
	{
		protected interface IProtected
		{
			
		}

		private protected interface IPrivateProtected
		{
			
		}

		protected internal interface IProtectedInternal
		{
			
		}

		private interface IPrivate
		{
		}
	}

}
