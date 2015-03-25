using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		#region constants
		/// <summary>
		/// Ratio of a circle's diameter to its cicumference: 
		/// &#x3c0; = 3.14159265358979323846
		/// </summary>
		public static readonly Number PI = 3.14159265358979323846;

		/// <summary>
		/// Euler's number e = 2.7182818284590452354
		/// </summary>
		public static readonly Number E = 2.7182818284590452354;
		#endregion

		private static readonly List<int> primes = new List<int>();

		public static Number Pow(Number baseNum, Number exponent)
		{
			return baseNum ^ exponent;
		}

		public static Number Sqrt(Number radicand)
		{
			return Math.Sqrt(radicand);
		}
		public static Number Root(Number radicand, Number radical)
		{
			return Pow(radicand, 1.0 / radical);
		}

		public static Number Abs(Number value)
		{
			return value.AbsoluteValue();
		}

		public static Number Round(Number value, int digits = 0)
		{
			Number mult = Pow(10.0, digits);
			Number bigger = value * mult;
			int lower = (int)bigger;
			int upper = lower + 1;

			if (upper - bigger >= bigger - lower)
			{
				return upper;
			}

			return lower;
		}

		public static int Floor(Number value)
		{
			int res = (int)value;
			if (value < 0.0)
			{
				return res - 1;
			}
			return res;
		}
		public static int Ceiling(Number value)
		{
			int res = (int)value;
			if (value > 0.0)
			{
				return res + 1;
			}
			return res;
		}

		public static Number Fractional(Number value)
		{
			Number ipart = (int)value;
			return value - ipart;
		}

		public static Number Min(Number a, Number b)
		{
			return a <= b ? a : b;
		}
		public static Number Max(Number a, Number b)
		{
			return a >= b ? a : b;
		}
		public static Number Constrain(Number value, Number min, Number max)
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

		public static Number Sigma(Function2D exp, Number lower,
			Number upper, Number increment)
		{
			Number total = 0;
			for (Number i = lower; i <= upper; i += increment)
			{
				total += exp(i);
			}
			return total;
		}
		public static Number Sigma(Function2D exp, Number lower,
			Number upper)
		{
			return Sigma(exp, lower, upper, 1.0);
		}
	}
}
