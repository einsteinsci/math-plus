using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		/// <summary>
		/// Functions relating to random numbers and probability
		/// </summary>
		public static class Probability
		{
			/// <summary>
			/// Static <see cref="Random"/> instance for convenience.
			/// </summary>
			public static readonly Random Rand = new Random();

			/// <summary>
			/// Factorial of a number; n!
			/// </summary>
			/// <param name="value">Value to take the factorial of</param>
			/// <returns>Factorial of <paramref name="value"/>: n!</returns>
			/// <remarks>
			/// Both input and output using this function are often very large, so
			/// a <see cref="Int64"/> (<c>long</c>) is used to avoid overflow.
			/// </remarks>
			public static long Factorial(long value)
			{
				if (value == 0 || value == 1)
				{
					return 1;
				}

				return value * Factorial(value - 1);
			}

			/// <summary>
			/// Permutations of a set of values, given a domain size and selection size.
			/// </summary>
			/// <param name="n">Size of domain</param>
			/// <param name="r">Number of items selected</param>
			/// <returns>Number of permutations: nPr.</returns>
			/// <remarks>
			/// Both input and output using this function are often very large, so
			/// a <see cref="Int64"/> (<c>long</c>) is used to avoid overflow.
			/// </remarks>
			public static long Permutation(long n, long r)
			{
				return Factorial(n) / Factorial(n - r);
			}
			/// <summary>
			/// Combinations of a set of values, given a domain size and selection size.
			/// </summary>
			/// <param name="n">Size of domain</param>
			/// <param name="r">Number of items selected</param>
			/// <returns>Number of permutations: nCr.</returns>
			/// <remarks>
			/// Both input and output using this function are often very large, so
			/// a <see cref="Int64"/> (<c>long</c>) is used to avoid overflow.
			/// </remarks>
			public static long Combination(long n, long r)
			{
				return Factorial(n) / (Factorial(r) * Factorial(n - r));
			}

			/// <summary>
			/// Returns a List of random integers of a given size, without repeats.
			/// </summary>
			/// <param name="min">Minimum value to generate</param>
			/// <param name="max">Maximum value to generate</param>
			/// <param name="length">Length of List to generate</param>
			/// <returns>A list of nonrepeating, random integers.</returns>
			/// <exception cref="ArgumentException">
			/// Thrown if <c><paramref name="max"/> &lt;= <paramref name="min"/></c>
			/// or if <c><paramref name="max"/> - <paramref name="min"/> > 
			/// <paramref name="length"/></c>, where more numbers are requested
			/// than can be generated.
			/// </exception>
			public static List<int> RandomIntsNoRepeat(int min, int max, int length)
			{
				if (max <= min)
				{
					throw new ArgumentException(
						"Exclusive max cannot be below or equal to inclusive min.");
				}
				if (max - min > length)
				{
					throw new ArgumentException("Number of numbers to generate " +
						"exceeds possible numbers that can be generated.");
				}

				List<int> res = new List<int>();

				while (res.Count < length)
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
