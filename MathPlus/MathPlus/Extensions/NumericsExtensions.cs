﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	/// <summary>
	/// Extension methods for MathPlus.Numerics
	/// </summary>
	public static class NumericsExtensions
	{
		/// <summary>
		/// Rounds a number to the integer closest to zero
		/// </summary>
		/// <param name="value">Value to round</param>
		/// <returns>Value within 1.0 away closest to zero</returns>
		public static int Floor(this double value)
		{
			return MathPlus.Numerics.Floor(value);
		}
		/// <summary>
		/// Rounds a number to the integer farthest from zero
		/// </summary>
		/// <param name="value">Value to round</param>
		/// <returns>Value within 1.0 away farthest from zero</returns>
		public static int Ceiling(this double value)
		{
			return MathPlus.Numerics.Ceiling(value);
		}
		/// <summary>
		/// Normalized approximate comparison function
		/// </summary>
		/// <param name="value">Value to compare</param>
		/// <param name="other">Other value to compare to</param>
		/// <param name="diff">Approximated difference between value and other</param>
		/// <param name="maxError">Maximum error in the approximation; accuracy</param>
		/// <returns>True if approximately equal, false if not</returns>
		public static bool AlmostEqualToNorm(this double value, double other, 
			double diff, double maxError)
		{
			return MathPlus.Numerics.AlmostEqualToNorm(value, other, diff, maxError);
		}
		/// <summary>
		/// Approximate comparison function
		/// </summary>
		/// <param name="value">Value to compare</param>
		/// <param name="other">Other value to compare to</param>
		/// <param name="err">Maximum error in the approximation; accuracy. Defaults to 1e-10.</param>
		/// <returns>True if approximately equal, false if not</returns>
		public static bool AlmostEqualTo(this double value, double other, double err = 1e-10)
		{
			return MathPlus.Numerics.AlmostEqualTo(value, other, err);
		}

		/// <summary>
		/// Determines if an integer is odd.
		/// </summary>
		/// <param name="n">Number to test</param>
		/// <returns>True if the number is odd, false if even</returns>
		public static bool IsOdd(this int n)
		{
			return !n.IsEven();
		}
		/// <summary>
		/// Determines if an integer is even.
		/// </summary>
		/// <param name="n">Number to test</param>
		/// <returns>True if the number is even, false if odd</returns>
		public static bool IsEven(this int n)
		{
			return n.IsDivisibleBy(2);
		}

		/// <summary>
		/// Determines if an integer is divisible by another integer.
		/// </summary>
		/// <param name="n">Number to test</param>
		/// <param name="divisor">Integer <paramref name="n"/> is tested to be divisible by</param>
		/// <returns>
		/// True if <paramref name="n"/> is divisible by <paramref name="divisor"/>,
		/// false otherwise.
		/// </returns>
		public static bool IsDivisibleBy(this int n, int divisor)
		{
			return n % divisor == 0;
		}

		/// <summary>
		/// Deterimines if a number is approximately an integer
		/// </summary>
		/// <param name="value">Number to test</param>
		/// <param name="precision">Number of decimal places to round to before test</param>
		/// <returns>
		/// True if <paramref name="value"/> is an integer to <paramref name="precision"/>
		/// decimal places, false otherwise.
		/// </returns>
		public static bool IsInteger(this double value, int precision)
		{
			double rounded = MathPlus.Numerics.Round(value, precision);
			int trunc = (int)rounded;
			return rounded == (double)trunc;
		}
	}
}
