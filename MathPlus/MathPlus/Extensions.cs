using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	internal static class Extensions
	{
		internal static bool IsOdd(this int n)
		{
			return !n.IsEven();
		}
		internal static bool IsEven(this int n)
		{
			return n.IsDivisibleBy(2);
		}

		internal static bool IsDivisibleBy(this int n, int divisor)
		{
			return n % divisor == 0;
		}

		internal static bool IsInteger(this double value, int precision)
		{
			double rounded = MathPlus.Numerics.Round(value, precision);
			int trunc = (int)rounded;
			return rounded == (double)trunc;
		}

		internal static double RoundUp(this double value, int count = 1)
		{
			if (double.IsInfinity(value) || double.IsNaN(value) || count == 0)
			{
				return value;
			}

			if (count < 0)
			{
				return RoundDown(value, -count);
			}

			// Translate the bit pattern of the double to an integer.
			// Note that this leads to:
			// double > 0 --> long > 0, growing as the double value grows
			// double < 0 --> long < 0, increasing in absolute magnitude as the double 
			//                          gets closer to zero!
			//                          i.e. 0 - double.epsilon will give the largest long value!
			long intValue = AsInt64(value);
			if (intValue < 0)
			{
				intValue -= count;
			}
			else
			{
				intValue += count;
			}

			// Note that long.MinValue has the same bit pattern as -0.0.
			if (intValue == long.MinValue)
			{
				return 0;
			}

			// Note that not all long values can be translated into double values. There's a whole bunch of them 
			// which return weird values like infinity and NaN
			return BitConverter.Int64BitsToDouble(intValue);
		}

		internal static double RoundDown(this double value, int count = 1)
		{
			if (double.IsInfinity(value) || double.IsNaN(value) || count == 0)
			{
				return value;
			}

			if (count < 0)
			{
				return RoundDown(value, -count);
			}

			// Translate the bit pattern of the double to an integer.
			// Note that this leads to:
			// double > 0 --> long > 0, growing as the double value grows
			// double < 0 --> long < 0, increasing in absolute magnitude as the double 
			//                          gets closer to zero!
			//                          i.e. 0 - double.epsilon will give the largest long value!
			long intValue = AsInt64(value);

			// If the value is zero then we'd really like the value to be -0. So we'll make it -0 
			// and then everything else should work out.
			if (intValue == 0)
			{
				// Note that long.MinValue has the same bit pattern as -0.0.
				intValue = long.MinValue;
			}

			if (intValue < 0)
			{
				intValue += count;
			}
			else
			{
				intValue -= count;
			}

			// Note that not all long values can be translated into double values. There's a whole bunch of them 
			// which return weird values like infinity and NaN
			return BitConverter.Int64BitsToDouble(intValue);
		}

		internal static long AsInt64(double value)
		{
			return BitConverter.DoubleToInt64Bits(value);
		}
	}
}
