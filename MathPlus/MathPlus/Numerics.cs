using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		public static class Numerics
		{
			public static int LeastCommonMultiple(int a, int b)
			{
				for (int res = (int)Max(a, b); res <= a * b; res++)
				{
					if (res % a == 0 && res % b == 0)
					{
						return res;
					}
				}

				return a * b;
			}

			public static int GreatestCommonDenominator(Fraction a, Fraction b)
			{
				return GreatestCommonDenominator(a.Denominator, b.Denominator);
			}
			public static int GreatestCommonDenominator(int a, int b)
			{
				for (int res = (int)Min(a, b); res > 1; res--)
				{
					if (a % res == 0 && b % res == 0)
					{
						return res;
					}
				}

				return -1;
			}

			public static double Round(double value, int digits = 0)
			{
				return (double)Math.Round((decimal)value, digits, MidpointRounding.AwayFromZero);

				//double mult = Pow(10.0, digits);
				//double bigger = value * mult;
				//int lowerInt = (int)bigger;
				//int upperInt = lowerInt + 1;
				//double lower = lowerInt;
				//double upper = upperInt;
				//
				//double result;
				//if (upper - bigger >= bigger - lower)
				//{
				//	result = lower;
				//}
				//else
				//{
				//	result = upper;
				//}
				//
				//return result / mult;
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

			public static bool AlmostEqualToNorm(double a, double b, double diff, double maxError)
			{
				double doublePrecision = MathPlus.Pow(2, -53);

				if (double.IsInfinity(a) || double.IsInfinity(b))
				{
					return a == b;
				}

				if (double.IsNaN(a) || double.IsNaN(b))
				{
					return false;
				}

				if (MathPlus.Abs(a) < doublePrecision || MathPlus.Abs(b) < doublePrecision)
				{
					return MathPlus.Abs(diff) < maxError;
				}

				if ((a == 0 && MathPlus.Abs(b) < maxError) || (b == 0 && MathPlus.Abs(a) < maxError))
				{
					return true;
				}

				return MathPlus.Abs(diff) < maxError * MathPlus.Max(MathPlus.Abs(a), MathPlus.Abs(b));
			}

			public static bool AlmostEqualTo(double a, double b, double err = 1e-10)
			{
				return AlmostEqualToNorm(a, b, Abs(a - b), err);
			}
		}
	}
}
