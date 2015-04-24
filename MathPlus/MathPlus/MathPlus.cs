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
		/// Ratio of a circle's diameter to its cicumference: &#x3c0;
		/// </summary>
		public const double PI = 3.1415926535897932384626433832795028841971693993751d;

		/// <summary>
		/// Euler's number e
		/// </summary>
		public const double E = 2.7182818284590452353602874713526624977572470937000d;

		/// <summary>
		/// Square root of 2
		/// </summary>
		public const double SQRT2 = 1.4142135623730950488016887242096980785696718753769d;
		#endregion

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

		public static double Log10(double value)
		{
			return Math.Log10(value);
		}
		public static double Log(double value, double baseNum)
		{
			return Math.Log(value, baseNum);
		}
		public static double Ln(double value)
		{
			return Math.Log(value);
		}

		public static double Abs(double value)
		{
			return value < 0 ? (-1.0 * value) : value;
		}

		public static int Sign(double value)
		{
			return Math.Sign(value);
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
		public static double Min(IEnumerable<double> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			double low = values.First();
			foreach (double d in values)
			{
				if (d < low)
				{
					low = d;
				}
			}

			return low;
		}
		public static double Min(params double[] values)
		{
			return Min(values.ToList());
		}
		public static int Min(IEnumerable<int> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			int low = values.First();
			foreach (int n in values)
			{
				if (n < low)
				{
					low = n;
				}
			}

			return low;
		}
		public static double Max(double a, double b)
		{
			return a >= b ? a : b;
		}
		public static double Max(IEnumerable<double> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			double high = values.First();
			foreach (double d in values)
			{
				if (d > high)
				{
					high = d;
				}
			}

			return low;
		}
		public static double Max(params double[] values)
		{
			return Max(values.ToList());
		}
		public static int Max(IEnumerable<int> values)
		{
			if (values.Count() < 1)
			{
				throw new ArgumentException("Collection cannot be empty");
			}

			int high = values.First();
			foreach (int n in values)
			{
				if (n > high)
				{
					high = n;
				}
			}

			return low;
		}
		public static double Constrain(double value, double min, double max)
		{
			if (value < min)
			{
				return min;
			}
			else if (value > max)
			{
				return max;
			}

			return value;
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
