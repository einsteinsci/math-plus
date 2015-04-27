using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="MathPlus.Trig"/>
	/// </summary>
	public static class TrigExtensions
	{
		/// <summary>
		/// Returns the sine of a value
		/// </summary>
		/// <param name="theta">angle to take the sine of, in radians</param>
		/// <returns>Sine of theta</returns>
		public static double Sin(this double theta)
		{
			return MathPlus.Trig.Sin(theta);
		}
		/// <summary>
		/// Returns the sine of a value
		/// </summary>
		/// <param name="theta">angle to take the sine of, in radians</param>
		/// <returns>Sine of theta</returns>
		public static double Sin(this int theta)
		{
			return MathPlus.Trig.Sin(theta);
		}
		/// <summary>
		/// Returns the cosine of a value
		/// </summary>
		/// <param name="theta">angle to take the cosine of, in radians</param>
		/// <returns>Cosine of theta</returns>
		public static double Cos(this double theta)
		{
			return MathPlus.Trig.Cos(theta);
		}
		/// <summary>
		/// Returns the cosine of a value
		/// </summary>
		/// <param name="theta">angle to take the cosine of, in radians</param>
		/// <returns>Cosine of theta</returns>
		public static double Cos(this int theta)
		{
			return MathPlus.Trig.Cos(theta);
		}
		/// <summary>
		/// Returns the tangent of a value
		/// </summary>
		/// <param name="theta">angle to take the tangent of, in radians</param>
		/// <returns>Tangent of theta</returns>
		public static double Tan(this double theta)
		{
			return MathPlus.Trig.Tan(theta);
		}
		/// <summary>
		/// Returns the tangent of a value
		/// </summary>
		/// <param name="theta">angle to take the tangent of, in radians</param>
		/// <returns>Tangent of theta</returns>
		public static double Tan(this int theta)
		{
			return MathPlus.Trig.Tan(theta);
		}

		/// <summary>
		/// Returns the cosecant of a value
		/// </summary>
		/// <param name="theta">angle to take the cosecant of, in radians</param>
		/// <returns>Cosecant of theta</returns>
		public static double Csc(this double theta)
		{
			return MathPlus.Trig.Csc(theta);
		}
		/// <summary>
		/// Returns the cosecant of a value
		/// </summary>
		/// <param name="theta">angle to take the cosecant of, in radians</param>
		/// <returns>Cosecant of theta</returns>
		public static double Csc(this int theta)
		{
			return MathPlus.Trig.Csc(theta);
		}
		/// <summary>
		/// Returns the secant of a value
		/// </summary>
		/// <param name="theta">angle to take the secant of, in radians</param>
		/// <returns>Secant of theta</returns>
		public static double Sec(this double theta)
		{
			return MathPlus.Trig.Sec(theta);
		}
		/// <summary>
		/// Returns the secant of a value
		/// </summary>
		/// <param name="theta">angle to take the secant of, in radians</param>
		/// <returns>Secant of theta</returns>
		public static double Sec(this int theta)
		{
			return MathPlus.Trig.Sec(theta);
		}
		/// <summary>
		/// Returns the cotangent of a value
		/// </summary>
		/// <param name="theta">angle to take the cotangent of, in radians</param>
		/// <returns>Cotangent of theta</returns>
		public static double Cot(this double theta)
		{
			return MathPlus.Trig.Cot(theta);
		}
		/// <summary>
		/// Returns the cotangent of a value
		/// </summary>
		/// <param name="theta">angle to take the cotangent of, in radians</param>
		/// <returns>Cotangent of theta</returns>
		public static double Cot(this int theta)
		{
			return MathPlus.Trig.Cot(theta);
		}

		/// <summary>
		/// Returns the inverse sine (arcsine) of a value
		/// </summary>
		/// <param name="proportion">Side lengths proportion to find the inverse sine of</param>
		/// <returns>Inverse sine of proportion, in radians</returns>
		public static double ASin(this double proportion)
		{
			return MathPlus.Trig.ASin(proportion);
		}
		/// <summary>
		/// Returns the inverse sine (arcsine) of a value
		/// </summary>
		/// <param name="proportion">Side lengths proportion to find the inverse sine of</param>
		/// <returns>Inverse sine of proportion, in radians</returns>
		public static double ASin(this int proportion)
		{
			return MathPlus.Trig.ASin(proportion);
		}
		/// <summary>
		/// Returns the inverse cosine (arccosine) of a value
		/// </summary>
		/// <param name="proportion">Side lengths proportion to find the inverse cosine of</param>
		/// <returns>Inverse cosine of proportion, in radians</returns>
		public static double ACos(this double proportion)
		{
			return MathPlus.Trig.ACos(proportion);
		}
		/// <summary>
		/// Returns the inverse cosine (arccosine) of a value
		/// </summary>
		/// <param name="proportion">Side lengths proportion to find the inverse cosine of</param>
		/// <returns>Inverse cosine of proportion, in radians</returns>
		public static double ACos(this int proportion)
		{
			return MathPlus.Trig.ACos(proportion);
		}
		/// <summary>
		/// Returns the inverse tangent (arctangent) of a value
		/// </summary>
		/// <param name="proportion">Side lengths proportion to find the inverse tangent of</param>
		/// <returns>Inverse tangent of proportion, in radians</returns>
		public static double ATan(this double proportion)
		{
			return MathPlus.Trig.ATan(proportion);
		}
		/// <summary>
		/// Returns the inverse tangent (arctangent) of a value
		/// </summary>
		/// <param name="proportion">Side lengths proportion to find the inverse tangent of</param>
		/// <returns>Inverse tangent of proportion, in radians</returns>
		public static double ATan(this int proportion)
		{
			return MathPlus.Trig.ATan(proportion);
		}

		/// <summary>
		/// Converts degrees to radians for easier use with <see cref="MathPlus.Trig"/>
		/// </summary>
		/// <param name="degrees">degrees units to convert</param>
		/// <returns>Angle in radians</returns>
		public static double ToRadians(this double degrees)
		{
			return (degrees / 180.0) * MathPlus.PI;
		}
		/// <summary>
		/// Converts degrees to radians for easier use with <see cref="MathPlus.Trig"/>
		/// </summary>
		/// <param name="degrees">degrees units to convert</param>
		/// <returns>Angle in radians</returns>
		public static double ToRadians(this int degrees)
		{
			return ToRadians((double)degrees);
		}
		/// <summary>
		/// Converts radians to degrees for after use with <see cref="MathPlus.Trig"/>
		/// </summary>
		/// <param name="degrees">radians units to convert</param>
		/// <returns>Angle in degrees</returns>
		public static double ToDegrees(this double radians)
		{
			return (radians * 180.0) / MathPlus.PI;
		}
		/// <summary>
		/// Converts radians to degrees for after use with <see cref="MathPlus.Trig"/>
		/// </summary>
		/// <param name="degrees">radians units to convert</param>
		/// <returns>Angle in degrees</returns>
		public static double ToDegrees(this int radians)
		{
			return ToRadians((double)radians);
		}
	}
}
