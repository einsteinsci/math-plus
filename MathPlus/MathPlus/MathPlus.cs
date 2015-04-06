using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		#region constants
		/// <summary>
		/// Ratio of a circle's diameter to its cicumference: 
		/// &#x3c0; = 3.14159265358979323846
		/// </summary>
		public static readonly double PI = 3.14159265358979323846;

		/// <summary>
		/// Euler's number e = 2.7182818284590452354
		/// </summary>
		public static readonly double E = 2.7182818284590452354;
		#endregion

		private static readonly List<int> primes = new List<int>();

		public static double Pow(double baseNum, double exponent)
		{
			return Math.Pow(baseNum, exponent);
		}
		public static double Exp(double exponent)
		{
			return Pow(E, exponent);
		}

		public static double Sqrt(double radicand)
		{
			return Math.Sqrt(radicand);
		}
		public static double Root(double radicand, double radical)
		{
			return Pow(radicand, 1.0 / radical);
		}

		public static double Abs(double value)
		{
			return value < 0 ? (-1.0 * value) : value;
		}

		public static double Round(double value, int digits = 0)
		{
			double mult = Pow(10.0, digits);
			double bigger = value * mult;
			int lowerInt = (int)bigger;
			int upperInt = lowerInt + 1;
			double lower = lowerInt;
			double upper = upperInt;

			double result;
			if (upper - bigger >= bigger - lower)
			{
				result = lower;
			}
			else
			{
				result = upper;
			}

			return result / mult;
		}

		public static int Floor(double value)
		{
			int res = (int)value;
			if (value < 0.0)
			{
				return res - 1;
			}
			return res;
		}
		public static int Ceiling(double value)
		{
			int res = (int)value;
			if (value > 0.0)
			{
				return res + 1;
			}
			return res;
		}

		public static double Fractional(double value)
		{
			double ipart = (int)value;
			return value - ipart;
		}

		public static double Min(double a, double b)
		{
			return a <= b ? a : b;
		}
		public static double Max(double a, double b)
		{
			return a >= b ? a : b;
		}
		public static double Constrain(double value, double min, double max)
		{
			if (value < min)
			{
				return min;
			}
			else if (value < max)
			{
				return max;
			}

			return value;
		}

		public static int LeastCommonMultiplier(int a, int b)
		{
			int max = (int)Max(a, b);

			for (int i = 2; i <= max; i++)
			{
				if (a % i == 0 && b % i == 0)
				{
					return i;
				}
			}

			return max;
		}

		public static double Sigma(Function2D exp, double lower,
			double upper, double increment)
		{
			double total = 0;
			for (double i = lower; i <= upper; i += increment)
			{
				total += exp(i);
			}
			return total;
		}
		public static double Sigma(Function2D exp, double lower,
			double upper)
		{
			return Sigma(exp, lower, upper, 1.0);
		}
	}
}
