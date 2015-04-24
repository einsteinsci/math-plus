using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	public class MathExtensions
	{
		public static double Pow(this double baseNum, double exponent)
		{
			return MathPlus.Pow(baseNum, exponent);
		}
		public static double Sqrt(this double radicand)
		{
			return MathPlus.Sqrt(radicand);
		}
		public static double Root(this double radicand, double radical)
		{
			return MathPlus.Root(radicand, radical);
		}

		public static double Log10(this double value)
		{
			return MathPlus.Log10(value);
		}
		public static double Log(this double value, double baseNum)
		{
			return MathPlus.Log(value, baseNum);
		}
		public static double Ln(this double value)
		{
			return MathPlus.Ln(value);
		}

		public static double Abs(this double value)
		{
			return MathPlus.Abs(value);
		}
		public static int Sign(this double value)
		{
			return MathPlus.Sign(value);
		}
		public static double Fractional(this double value)
		{
			return MathPlus.Fractional(value);
		}

		public static double Min(this IEnumerable<double> values)
		{
			return MathPlus.Min(values);
		}
		public static int Min(this IEnumerable<int> values)
		{
			return MathPlus.Min(values);
		}
		public static double Max(this IEnumerable<double> values)
		{
			return MathPlus.Max(values);
		}
		public static int Max(this IEnumerable<int> values)
		{
			return MathPlus.Max(values);
		}
		public static double Constrain(this double value, double min, double max)
		{
			return MathPlus.Constrain(value, min, max);
		}
	}
}
