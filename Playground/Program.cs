using System;
using FastEnumUtility;

namespace Playground
{
	public delegate int IntegerBinaryOperation(int x, int y);
	class Program
	{
		static void Main(string[] args)
		{
			Func<int[], int> hgoe = Add;


		}

		static unsafe int Add(int[] array)
		{
			var accum = 0;
			
			fixed (int* ptr = array)
			{
				for (int i = 0; i < array.Length; i++)
				{
					accum += *(ptr + i);
				}
			}

			return accum;
		}
	}
}