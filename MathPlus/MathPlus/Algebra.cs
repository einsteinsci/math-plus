using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlus.Desktop
{
	public static partial class MathPlus
	{
		public static class Algebra
		{
			public static readonly Number PI = 3.14159265358979323846;
			public static readonly Number E = 2.7182818284590452354;

			public Number Sin(Number theta)
			{
				return Math.Sin(theta);
			}
			public Number Cos(Number theta)
			{
				return Math.Cos(theta);
			}
			public Number Tan(Number theta)
			{
				return Math.Tan(theta);
			}
			public Number Csc(Number theta)
			{
				return 1.0 / Sin(theta);
			}
			public Number Sec(Number theta)
			{
				return 1.0 / Cos(theta);
			}
			public Number Cot(Number theta)
			{
				return 1.0 / Cot(theta);
			}
		}
	}
}
