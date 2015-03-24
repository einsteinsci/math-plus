using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Portable;
using MathPlusLib.Desktop;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		public static class Trig
		{
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

			public static Number Log10(Number value)
			{
				return Math.Log10(value);
			}
			public static Number Log(Number value, Number baseNum)
			{
				return Math.Log(value, baseNum);
			}
			public static Number Ln(Number value)
			{
				return Math.Log(value);
			}
		}
	}
}
