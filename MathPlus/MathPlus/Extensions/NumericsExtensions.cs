using System;
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
		/// <param name="value">value to round</param>
		/// <returns>Value within 1.0 away closest to zero</returns>
		public static int Floor(this double value)
		{
			return MathPlus.Numerics.Floor(value);
		}
		/// <summary>
		/// Rounds a number to the integer farthest from zero
		/// </summary>
		/// <param name="value">value to round</param>
		/// <returns>Value within 1.0 away farthest from zero</returns>
		public static int Ceiling(this double value)
		{
			return MathPlus.Numerics.Ceiling(value);
		}
		/// <summary>
		/// Normalized approximate comparison function
		/// </summary>
		/// <param name="value">value to compare</param>
		/// <param name="other">other value to compare to</param>
		/// <param name="diff">approximated difference between value and other</param>
		/// <param name="maxError">maximum error in the approximation; accuracy</param>
		/// <returns>True if approximately equal, false if not</returns>
		public static bool AlmostEqualToNorm(this double value, double other, 
			double diff, double maxError)
		{
			return MathPlus.Numerics.AlmostEqualToNorm(value, other, diff, maxError);
		}
		/// <summary>
		/// Approximate comparison function
		/// </summary>
		/// <param name="value">value to compare</param>
		/// <param name="other">other value to compare to</param>
		/// <param name="err">maximum error in the approximation; accuracy. Defaults to 1e-10.</param>
		/// <returns>True if approximately equal, false if not</returns>
		public static bool AlmostEqualTo(this double value, double other, double err = 1e-10)
		{
			return MathPlus.Numerics.AlmostEqualTo(value, other, err);
		}
	}
}
