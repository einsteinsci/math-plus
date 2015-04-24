using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	public static class NumericsExtensions
	{
		public static int Floor(this double value)
		{
			return MathPlus.Numerics.Floor(value);
		}
		public static int Ceiling(this double value)
		{
			return MathPlus.Numerics.Ceiling(value);
		}
		public static bool AlmostEqualToNorm(this double value, double other, 
			double diff, double maxError)
		{
			return MathPlus.Numerics.AlmostEqualToNorm(value, other, diff, maxError);
		}
		public static bool AlmostEqualTo(this double value, double other, double err = 1e-10)
		{
			return MathPlus.Numerics.AlmostEqualTo(value, other, err);
		}
	}
}
