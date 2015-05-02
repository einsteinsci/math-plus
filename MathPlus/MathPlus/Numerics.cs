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
		/// Library of functions related to the intricacies of numbers
		/// </summary>
		public static class Numerics
		{
			/// <summary>
			/// Returns the least common multiple of the two numbers
			/// </summary>
			/// <param name="a">First number</param>
			/// <param name="b">Second number</param>
			/// <returns>
			/// Lowest number that is divisible by both <paramref name="a"/>
			/// and <paramref name="b"/>.
			/// </returns>
			public static int LeastCommonMultiple(int a, int b)
			{
				for (int res = (int)Max(a, b); res <= a * b; res++)
				{
					if (res % a == 0 && res % b == 0)
					{
						return res;
					}
				}

				return a * b;
			}

			/// <summary>
			/// Returns the greatest common denominator of two given fractions
			/// </summary>
			/// <param name="a">First fraction</param>
			/// <param name="b">Second fraction</param>
			/// <returns>
			/// Highest number that both denominators are divisible by, 
			/// -1 if there is none.
			/// </returns>
			public static int GreatestCommonDenominator(Fraction a, Fraction b)
			{
				return GreatestCommonDenominator(a.Denominator, b.Denominator);
			}
			/// <summary>
			/// Returns the greatest common denominator of two given denominators
			/// </summary>
			/// <param name="a">Denominator of first fraction</param>
			/// <param name="b">Denominator of second fraction</param>
			/// <returns>
			/// Highest number that both denominators are divisible by, -1 if there
			/// is none.
			/// </returns>
			public static int GreatestCommonDenominator(int a, int b)
			{
				for (int res = (int)Min(a, b); res > 1; res--)
				{
					if (a % res == 0 && b % res == 0)
					{
						return res;
					}
				}

				return -1;
			}

			/// <summary>
			/// Rounds a number to the specified number of digits.
			/// </summary>
			/// <param name="value">Value to round</param>
			/// <param name="digits">Numer of digits to round to</param>
			/// <returns>
			/// The number with the specified digits which is closest to
			/// <paramref name="value"/>.
			/// </returns>
			public static double Round(double value, int digits = 0)
			{
				return (double)Math.Round((decimal)value, digits, MidpointRounding.AwayFromZero);
			}

			/// <summary>
			/// Rounds a number down
			/// </summary>
			/// <param name="value">Value to round</param>
			/// <returns>Lowest integer value within 1.0</returns>
			public static int Floor(double value)
			{
				int res = (int)value;
				if (value < 0.0)
				{
					return res - 1;
				}
				return res;
			}
			/// <summary>
			/// Rounds a number up
			/// </summary>
			/// <param name="value">Value to round</param>
			/// <returns>Highest integer value within 1.0</returns>
			public static int Ceiling(double value)
			{
				int res = (int)value;
				if (value > 0.0)
				{
					return res + 1;
				}
				return res;
			}

			/// <summary>
			/// Normalized approximate comparison function
			/// </summary>
			/// <param name="a">First value to compare</param>
			/// <param name="b">Second value to compare</param>
			/// <param name="diff">
			/// Approximated difference between <paramref name="a"/> and 
			/// <paramref name="b"/>.
			/// </param>
			/// <param name="maxError">Maximum error in the approximation, accuracy</param>
			/// <returns>True if approximately equal, false if not</returns>
			public static bool AlmostEqualToNorm(double a, double b, double diff, double maxError)
			{
				double doublePrecision = MathPlus.Pow(2, -53);

				if (double.IsInfinity(a) || double.IsInfinity(b))
				{
					return a == b;
				}

				if (double.IsNaN(a) || double.IsNaN(b))
				{
					return false;
				}

				if (MathPlus.Abs(a) < doublePrecision || MathPlus.Abs(b) < doublePrecision)
				{
					return MathPlus.Abs(diff) < maxError;
				}

				if ((a == 0 && MathPlus.Abs(b) < maxError) || (b == 0 && MathPlus.Abs(a) < maxError))
				{
					return true;
				}

				return MathPlus.Abs(diff) < maxError * MathPlus.Max(MathPlus.Abs(a), MathPlus.Abs(b));
			}

			/// <summary>
			/// Approximate comparison function
			/// </summary>
			/// <param name="a">First value to compare</param>
			/// <param name="b">Second value to compare</param>
			/// <param name="err">Maximum error in the approximation; inaccuracy.</param>
			/// <returns></returns>
			public static bool AlmostEqualTo(double a, double b, double err = 1e-10)
			{
				return AlmostEqualToNorm(a, b, Abs(a - b), err);
			}
		}
	}
}
