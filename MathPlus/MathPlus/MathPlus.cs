using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	/// <summary>
	/// Main class of basic functions. Additional functions are provided in subclasses.
	/// </summary>
	public static partial class MathPlus
	{
		#region constants
		/// <summary>
		/// Ratio of a circle's diameter to its cicumference: &#x3c0;
		/// </summary>
		public const double PI = 3.1415926535897932384626433832795028841971693993751d;

		/// <summary>
		/// Euler's number e
		/// </summary>
		public const double E = 2.7182818284590452353602874713526624977572470937000d;

		/// <summary>
		/// Square root of 2
		/// </summary>
		public const double SQRT2 = 1.4142135623730950488016887242096980785696718753769d;
		#endregion

		/// <summary>
		/// Raises a number to a power
		/// </summary>
		/// <param name="baseNum">Number to be raised to a power</param>
		/// <param name="exponent">Exponent to be raised to</param>
		/// <returns>The value <paramref name="baseNum"/> ^ <paramref name="exponent"/>.</returns>
		/// <remarks>This is an alternative to <c>Math.Pow(double, double)</c></remarks>
		public static double Pow(double baseNum, double exponent)
		{
			return Math.Pow(baseNum, exponent);
		}
		/// <summary>
		/// Raises e to a power
		/// </summary>
		/// <param name="exponent">Exponent to raise e to</param>
		/// <returns>The value e ^ <paramref name="exponent"/>.</returns>
		public static double Exp(double exponent)
		{
			return Pow(E, exponent);
		}

		/// <summary>
		/// Returns the square root of a number
		/// </summary>
		/// <param name="radicand">Number to take the square root of</param>
		/// <returns>The square root of <paramref name="radicand"/>.</returns>
		/// <remarks>This is an alternative to <c>Math.Sqrt(double)</c></remarks>
		public static double Sqrt(double radicand)
		{
			return Math.Sqrt(radicand);
		}
		/// <summary>
		/// Returns the nth root of a number
		/// </summary>
		/// <param name="radicand">Number to take the nth root of</param>
		/// <param name="radical">Index of radical n</param>
		/// <returns>The nth root of <paramref name="radicand"/>.</returns>
		public static double Root(double radicand, double radical)
		{
			return Pow(radicand, 1.0 / radical);
		}

		/// <summary>
		/// Returns the base-10 logarithm of a number
		/// </summary>
		/// <param name="value">Number to take the base-10 logarithm of</param>
		/// <returns>The base-10 logarithm of <paramref name="value"/>.</returns>
		/// <remarks>This is a shortcut to <c>Math.Log10(double)</c></remarks>
		public static double Log10(double value)
		{
			return Math.Log10(value);
		}
		/// <summary>
		/// Returns the base-n logarithm of a number
		/// </summary>
		/// <param name="value">Number to take the base-n logarithm of</param>
		/// <param name="baseNum">Base of the logarithm n</param>
		/// <returns>The base-n logarithm of <paramref name="value"/>.</returns>
		/// <remarks>This is a shortcut to <c>Math.Log(double, double)</c></remarks>
		public static double Log(double value, double baseNum)
		{
			return Math.Log(value, baseNum);
		}
		/// <summary>
		/// Returns the natural logarithm (base e) of a number
		/// </summary>
		/// <param name="value">Number to take the natural logarithm of</param>
		/// <returns>The natural logarithm of <paramref name="value"/>.</returns>
		/// <remarks>This is a shortcut to <c>Math.Log(double)</c></remarks>
		public static double Ln(double value)
		{
			return Math.Log(value);
		}

		/// <summary>
		/// Returns the absolute value of a number |value|
		/// </summary>
		/// <param name="value">Number to take the absolute value of</param>
		/// <returns>|<paramref name="value"/>|</returns>
		public static double Abs(double value)
		{
			return value < 0 ? (-1.0 * value) : value;
		}

		/// <summary>
		/// Returns the sign of a number in the form of an integer
		/// </summary>
		/// <param name="value">Value to check the sign of</param>
		/// <returns>
		/// -1 if <paramref name="value"/> is negative, 0 if zero, and
		/// 1 if positive.
		/// </returns>
		/// <remarks>This is a shortcut to <c>Math.Sign(value)</c></remarks>
		public static int Sign(double value)
		{
			return Math.Sign(value);
		}

		/// <summary>
		/// Returns the fractional part of a number
		/// </summary>
		/// <param name="value">Value to take the fractional part of</param>
		/// <returns>The part of the number after the decimal point</returns>
		public static double Fractional(double value)
		{
			double ipart = (int)value;
			return value - ipart;
		}

		/// <summary>
		/// Returns the lower of two values
		/// </summary>
		/// <param name="a">First value</param>
		/// <param name="b">Second value</param>
		/// <returns>
		/// Whichever is lower, <paramref name="a"/> or <paramref name="b"/>
		/// </returns>
		public static double Min(double a, double b)
		{
			return a <= b ? a : b;
		}
		/// <summary>
		/// Returns the minimum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the minimum from</param>
		/// <returns>The lowest number within <paramref name="values"/></returns>
		public static double Min(IEnumerable<double> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			double low = values.First();
			foreach (double d in values)
			{
				if (d < low)
				{
					low = d;
				}
			}

			return low;
		}
		/// <summary>
		/// Returns the minimum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the minimum from</param>
		/// <returns>The lowest number within <paramref name="values"/></returns>
		public static double Min(params double[] values)
		{
			return Min(values.ToList());
		}
		/// <summary>
		/// Returns the minimum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the minimum from</param>
		/// <returns>The lowest number within <paramref name="values"/></returns>
		public static int Min(IEnumerable<int> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			int low = values.First();
			foreach (int n in values)
			{
				if (n < low)
				{
					low = n;
				}
			}

			return low;
		}

		/// <summary>
		/// Returns the higher of two values
		/// </summary>
		/// <param name="a">First value</param>
		/// <param name="b">Second value</param>
		/// <returns>
		/// Whichever is higher, <paramref name="a"/> or <paramref name="b"/>
		/// </returns>
		public static double Max(double a, double b)
		{
			return a >= b ? a : b;
		}
		/// <summary>
		/// Returns the maximum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the maximum from</param>
		/// <returns>The highest number within <paramref name="values"/></returns>
		public static double Max(IEnumerable<double> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			double high = values.First();
			foreach (double d in values)
			{
				if (d > high)
				{
					high = d;
				}
			}

			return high;
		}
		/// <summary>
		/// Returns the maximum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the maximum from</param>
		/// <returns>The highest number within <paramref name="values"/></returns>
		public static double Max(params double[] values)
		{
			return Max(values.ToList());
		}
		/// <summary>
		/// Returns the maximum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the maximum from</param>
		/// <returns>The highest number within <paramref name="values"/></returns>
		public static int Max(IEnumerable<int> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			int high = values.First();
			foreach (int n in values)
			{
				if (n > high)
				{
					high = n;
				}
			}

			return high;
		}

		/// <summary>
		/// Constrains a value to within a minimum and maximum.
		/// </summary>
		/// <param name="value">Value to constrain</param>
		/// <param name="min">Lower bound, inclusive</param>
		/// <param name="max">Upper bound, inclusive</param>
		/// <returns>Constrained value within [min, max]</returns>
		public static double Constrain(double value, double min, double max)
		{
			if (value < min)
			{
				return min;
			}
			else if (value > max)
			{
				return max;
			}

			return value;
		}

		/// <summary>
		/// Returns the iterative sum of a function
		/// </summary>
		/// <param name="exp">Expression to be summed</param>
		/// <param name="lower">Lower bound of input</param>
		/// <param name="upper">Upper bound of input</param>
		/// <param name="increment">Amount to increment by</param>
		/// <returns>Sum of all outputs from the specified inputs</returns>
		public static double Sigma(Function2D exp, double lower,
			double upper, double increment = 1.0)
		{
			double total = 0;
			for (double i = lower; i <= upper; i += increment)
			{
				total += exp(i);
			}
			return total;
		}
	}
}
