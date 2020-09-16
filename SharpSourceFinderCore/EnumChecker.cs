using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Tokeiya3.SharpSourceFinderCore
{
	internal static class EnumChecker<T> where T : Enum
	{
		private static readonly HashSet<T> Contents = new HashSet<T>();

		static EnumChecker()
		{
			var ret = (T[])Enum.GetValues(typeof(T));
			foreach (var elem in ret)
			{
				Contents.Add(elem);
			}
		}

		public static bool Verify(T value) => Contents.Contains(value);

	}

}
