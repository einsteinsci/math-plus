using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	/// <summary>
	/// Extension Methods class
	/// </summary>
	public static class MathExtensions
	{
		/// <summary>
		/// Raises a number to a power
		/// </summary>
		/// <param name="baseNum">Number to be raised to a power</param>
		/// <param name="exponent">Exponent to be raised to</param>
		/// <returns>The value baseNum ^ exponent</returns>
		public static double Pow(this double baseNum, double exponent)
		{
			return MathPlus.Pow(baseNum, exponent);
		}
		/// <summary>
		/// Raises a number to a power
		/// </summary>
		/// <param name="baseNum">Number to be raised to a power</param>
		/// <param name="exponent">Exponent to be raised to</param>
		/// <returns>The value baseNum ^ exponent</returns>
		public static double Pow(this int baseNum, double exponent)
		{
			return MathPlus.Pow(baseNum, exponent);
		}
		/// <summary>
		/// Returns the square root of a number
		/// </summary>
		/// <param name="radicand">value to take the square root of</param>
		/// <returns>The square root of radicand</returns>
		public static double Sqrt(this double radicand)
		{
			return MathPlus.Sqrt(radicand);
		}
		/// <summary>
		/// Returns the square root of a number
		/// </summary>
		/// <param name="radicand">value to take the square root of</param>
		/// <returns>The square root of radicand</returns>
		public static double Sqrt(this int radicand)
		{
			return MathPlus.Sqrt(radicand);
		}
		/// <summary>
		/// Returns the nth root of a number
		/// </summary>
		/// <param name="radicand">value to take the nth root</param>
		/// <param name="radical">index of radical n</param>
		/// <returns>The nth root of radicand</returns>
		public static double Root(this double radicand, double radical)
		{
			return MathPlus.Root(radicand, radical);
		}
		/// <summary>
		/// Returns the nth root of a number
		/// </summary>
		/// <param name="radicand">value to take the nth root</param>
		/// <param name="radical">index of radical n</param>
		/// <returns>The nth root of radicand</returns>
		public static double Root(this int radicand, double radical)
		{
			return MathPlus.Root(radicand, radical);
		}

		/// <summary>
		/// Returns the base-10 logarithm of a number
		/// </summary>
		/// <param name="value">number to take the base-10 logarithm of</param>
		/// <returns>The base-10 logarithm of value</returns>
		public static double Log10(this double value)
		{
			return MathPlus.Log10(value);
		}
		/// <summary>
		/// Returns the base-10 logarithm of a number
		/// </summary>
		/// <param name="value">number to take the base-10 logarithm of</param>
		/// <returns>The base-10 logarithm of value</returns>
		public static double Log10(this int value)
		{
			return MathPlus.Log10(value);
		}
		/// <summary>
		/// Returns the base-n logarithm of a number
		/// </summary>
		/// <param name="value">number to take the base-n logarithm of</param>
		/// <param name="baseNum">base of the logarithm n</param>
		/// <returns>The base-n logarithm of value</returns>
		public static double Log(this double value, double baseNum)
		{
			return MathPlus.Log(value, baseNum);
		}
		/// <summary>
		/// Returns the base-n logarithm of a number
		/// </summary>
		/// <param name="value">number to take the base-n logarithm of</param>
		/// <param name="baseNum">base of the logarithm n</param>
		/// <returns>The base-n logarithm of value</returns>
		public static double Log(this int value, double baseNum)
		{
			return MathPlus.Log(value, baseNum);
		}
		/// <summary>
		/// Returns the natural logarithm (base e) of a number
		/// </summary>
		/// <param name="value">number to take the natural logarithm of</param>
		/// <returns>The natural logarithm of value</returns>
		public static double Ln(this double value)
		{
			return MathPlus.Ln(value);
		}
		/// <summary>
		/// Returns the natural logarithm (base e) of a number
		/// </summary>
		/// <param name="value">number to take the natural logarithm of</param>
		/// <returns>The natural logarithm of value</returns>
		public static double Ln(this int value)
		{
			return MathPlus.Ln(value);
		}

		/// <summary>
		/// Returns the absolute value of a number |value|
		/// </summary>
		/// <param name="value">number to take the absolute value of</param>
		/// <returns>|value|</returns>
		public static double Abs(this double value)
		{
			return MathPlus.Abs(value);
		}
		/// <summary>
		/// Returns the absolute value of a number |value|
		/// </summary>
		/// <param name="value">number to take the absolute value of</param>
		/// <returns>|value|</returns>
		public static double Abs(this int value)
		{
			return MathPlus.Abs(value);
		}
		/// <summary>
		/// Returns the sign in the form of an integer
		/// </summary>
		/// <param name="value">Value to check the sign of</param>
		/// <returns>-1 if value is negative, 0 if zero, and 1 if positive</returns>
		public static int Sign(this double value)
		{
			return MathPlus.Sign(value);
		}
		/// <summary>
		/// Returns the sign in the form of an integer
		/// </summary>
		/// <param name="value">Value to check the sign of</param>
		/// <returns>-1 if value is negative, 0 if zero, and 1 if positive</returns>
		public static int Sign(this int value)
		{
			return MathPlus.Sign(value);
		}
		/// <summary>
		/// Returns fractional part of a number
		/// </summary>
		/// <param name="value">Value to take the fractional part of</param>
		/// <returns>The part of the number after the decimal point</returns>
		public static double Fractional(this double value)
		{
			return MathPlus.Fractional(value);
		}

		/// <summary>
		/// Returns the minimum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the minimum from</param>
		/// <returns>The lowest number within values</returns>
		public static double Min(this IEnumerable<double> values)
		{
			return MathPlus.Min(values);
		}
		/// <summary>
		/// Returns the minimum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the minimum from</param>
		/// <returns>The lowest number within values</returns>
		public static int Min(this IEnumerable<int> values)
		{
			return MathPlus.Min(values);
		}
		/// <summary>
		/// Returns the maximum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the maximum from</param>
		/// <returns>The greatest number within values</returns>
		public static double Max(this IEnumerable<double> values)
		{
			return MathPlus.Max(values);
		}
		/// <summary>
		/// Returns the maximum of a collection of values
		/// </summary>
		/// <param name="values">Values to find the maximum from</param>
		/// <returns>The greatest number within values</returns>
		public static int Max(this IEnumerable<int> values)
		{
			return MathPlus.Max(values);
		}
		/// <summary>
		/// Constrains a value to within a minumum and maximum.
		/// </summary>
		/// <param name="value">Value to constrain</param>
		/// <param name="min">Lower bound, inclusive</param>
		/// <param name="max">Upper bound, inclusive</param>
		/// <returns>Constrained value within [min, max]</returns>
		public static double Constrain(this double value, double min, double max)
		{
			return MathPlus.Constrain(value, min, max);
		}
	}
}
