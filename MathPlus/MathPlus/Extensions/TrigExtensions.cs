using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	public static class TrigExtensions
	{
		public static double Sin(this double theta)
		{
			return MathPlus.Trig.Sin(theta);
		}
		public static double Cos(this double theta)
		{
			return MathPlus.Trig.Cos(theta);
		}
		public static double Tan(this double theta)
		{
			return MathPlus.Trig.Tan(theta);
		}

		public static double Csc(this double theta)
		{
			return MathPlus.Trig.Csc(theta);
		}
		public static double Sec(this double theta)
		{
			return MathPlus.Trig.Sec(theta);
		}
		public static double Cot(this double theta)
		{
			return MathPlus.Trig.Cot(theta);
		}

		public static double ASin(this double proportion)
		{
			return MathPlus.Trig.ASin(proportion);
		}
		public static double ACos(this double proportion)
		{
			return MathPlus.Trig.ACos(proportion);
		}
		public static double ATan(this double proportion)
		{
			return MathPlus.Trig.ATan(proportion);
		}
	}
}
