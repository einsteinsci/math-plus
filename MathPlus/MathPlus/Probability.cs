using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		public static class Probability
		{
			public static readonly Random Rand = new Random();

			public static long Factorial(long value)
			{
				if (value == 0 || value == 1)
				{
					return 1;
				}

				return value * Factorial(value - 1);
			}

			public static long Permutation(long n, long r)
			{
				return Factorial(n) / Factorial(n - r);
			}
			public static long Combination(long n, long r)
			{
				return Factorial(n) / (Factorial(r) * Factorial(n - r));
			}

			public static List<int> RandomIntsNoRepeat(int min, int max, int length)
			{
				List<int> res = new List<int>();

				for (int i = 0; i < length; i++)
				{
					int val = Rand.Next(min, max);
					if (!res.Contains(val))
					{
						res.Add(val);
					}
				}

				return res;
			}
		}
	}
}
