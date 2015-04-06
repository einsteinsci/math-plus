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
			public static double Sin(double theta)
			{
				double n = E;
				return Math.Sin(theta);
			}
			public static double Cos(double theta)
			{
				return Math.Cos(theta);
			}
			public static double Tan(double theta)
			{
				return Math.Tan(theta);
			}
			public static double Csc(double theta)
			{
				return 1.0 / Sin(theta);
			}
			public static double Sec(double theta)
			{
				return 1.0 / Cos(theta);
			}
			public static double Cot(double theta)
			{
				return 1.0 / Cot(theta);
			}
			public static double ASin(double proportion)
			{
				return Math.Asin(proportion);
			}
			public static double ACos(double proportion)
			{
				return Math.Acos(proportion);
			}
			public static double ATan(double proportion)
			{
				return Math.Atan(proportion);
			}
		}
	}
}
