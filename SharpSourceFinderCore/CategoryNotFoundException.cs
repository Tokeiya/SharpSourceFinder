using System;
using System.Runtime.Serialization;

namespace Tokeiya3.SharpSourceFinderCore
{
	[Serializable]
	public class CategoryNotFoundException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//


		public CategoryNotFoundException(string name) : base($"{name} category not found.")
		{
		}

		protected CategoryNotFoundException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}