using System;
using System.Runtime.Serialization;

namespace Tokeiya3.SharpSourceFinderCore
{
	[Serializable]
	public class IdentityDuplicatedException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public IdentityDuplicatedException()
		{
		}

		public IdentityDuplicatedException(string message) : base(message)
		{
		}

		public IdentityDuplicatedException(string message, Exception inner) : base(message, inner)
		{
		}

		protected IdentityDuplicatedException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}