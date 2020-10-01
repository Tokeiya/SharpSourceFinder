using System;
using System.Runtime.Serialization;

namespace Tokeiya3.SharpSourceFinderCore
{
	[Serializable]
	public class IdentityNotFoundException : InvalidOperationException
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public IdentityNotFoundException()
		{
		}

		public IdentityNotFoundException(string message) : base(message)
		{
		}

		public IdentityNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}

		protected IdentityNotFoundException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}