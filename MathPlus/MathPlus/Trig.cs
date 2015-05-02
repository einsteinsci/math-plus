using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		public static class Trig
		{
			/// <summary>
			/// Returns the sine of an angle
			/// </summary>
			/// <param name="theta">Angle, in radians</param>
			/// <returns>Sine of <paramref name="theta"/>.</returns>
			public static double Sin(double theta)
			{
				return Math.Sin(theta);
			}
			/// <summary>
			/// Returns the cosine of an angle
			/// </summary>
			/// <param name="theta">Angle, in radians</param>
			/// <returns>Cosine of <paramref name="theta"/>.</returns>
			public static double Cos(double theta)
			{
				return Math.Cos(theta);
			}
			/// <summary>
			/// Returns the tangent of an angle
			/// </summary>
			/// <param name="theta">Angle, in radians</param>
			/// <returns>Tangent of <paramref name="theta"/>.</returns>
			public static double Tan(double theta)
			{
				return Math.Tan(theta);
			}

			/// <summary>
			/// Returns the cosecant of an angle
			/// </summary>
			/// <param name="theta">Angle, in radians</param>
			/// <returns>Cosecant of <paramref name="theta"/>.</returns>
			public static double Csc(double theta)
			{
				return 1.0 / Sin(theta);
			}
			/// <summary>
			/// Returns the secant of an angle
			/// </summary>
			/// <param name="theta">Angle, in radians</param>
			/// <returns>Secant of <paramref name="theta"/>.</returns>
			public static double Sec(double theta)
			{
				return 1.0 / Cos(theta);
			}
			/// <summary>
			/// Returns the cotangent of an angle
			/// </summary>
			/// <param name="theta">Angle, in radians</param>
			/// <returns>Cotangent of <paramref name="theta"/>.</returns>
			public static double Cot(double theta)
			{
				return 1.0 / Cot(theta);
			}

			/// <summary>
			/// Returns the inverse sine (arcsine) of a proportion
			/// </summary>
			/// <param name="proportion">Proportion of sides</param>
			/// <returns>
			/// Inverse sine of <paramref name="proportion"/>, in radians.
			/// </returns>
			public static double ASin(double proportion)
			{
				return Math.Asin(proportion);
			}
			/// <summary>
			/// Returns the inverse cosine (arccosine) of a proportion
			/// </summary>
			/// <param name="proportion">Proportion of sides</param>
			/// <returns>
			/// Inverse cosine of <paramref name="proportion"/>, in radians.
			/// </returns>
			public static double ACos(double proportion)
			{
				return Math.Acos(proportion);
			}
			/// <summary>
			/// Returns the inverse tangent (arctangent) of a proportion
			/// </summary>
			/// <param name="proportion">Proportion of sides</param>
			/// <returns>
			/// Inverse tangent of <paramref name="proportion"/>, in radians.
			/// </returns>
			public static double ATan(double proportion)
			{
				return Math.Atan(proportion);
			}
		}
	}
}
