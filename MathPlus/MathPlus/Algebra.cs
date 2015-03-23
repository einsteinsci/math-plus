using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if (PORTABLE)
using MathPlus.Portable;
#endif
#if (DESKTOP)
using MathPlus.Desktop;
#endif

namespace MathPlus
{
	public static partial class ExtraMath
	{
		public static class Algebra
		{
			/// <summary>
			/// Ratio of a circle's diameter to its cicumference: 
			/// &#x3c0; = 3.14159265358979323846
			/// </summary>
			public static readonly Number PI = 3.14159265358979323846;

			/// <summary>
			/// Euler's number e = 2.7182818284590452354
			/// </summary>
			public static readonly Number E = 2.7182818284590452354;

			#region trig
			public static Number Sin(Number theta)
			{
				double n = E;
				return Math.Sin(theta);
			}
			public static Number Cos(Number theta)
			{
				return Math.Cos(theta);
			}
			public static Number Tan(Number theta)
			{
				return Math.Tan(theta);
			}
			public static Number Csc(Number theta)
			{
				return 1.0 / Sin(theta);
			}
			public static Number Sec(Number theta)
			{
				return 1.0 / Cos(theta);
			}
			public static Number Cot(Number theta)
			{
				return 1.0 / Cot(theta);
			}
			public static Number ASin(Number proportion)
			{
				return Math.Asin(proportion);
			}
			public static Number ACos(Number proportion)
			{
				return Math.Acos(proportion);
			}
			public static Number ATan(Number proportion)
			{
				return Math.Atan(proportion);
			}
			#endregion

			public static Number Abs(Number value)
			{
				return value.AbsoluteValue();
			}
		}
	}
}
