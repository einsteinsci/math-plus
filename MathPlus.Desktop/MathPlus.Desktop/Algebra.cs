using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlus.Desktop
{
	public static class Algebra
	{
		public static double Csc(double value)
		{
			return 1.0 / Math.Sin(value);
		}
		public static double Sec(double value)
		{
			return 1.0 / Math.Cos(value);
		}
		public static double Cot(double value)
		{
			return 1.0 / Math.Tan(value);
		}

		
	}
}
