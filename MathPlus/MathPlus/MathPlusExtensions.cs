using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public static class MathPlusExtensions
	{
		public static bool IsOdd(this int n)
		{
			return !n.IsEven();
		}
		public static bool IsEven(this int n)
		{
			return n.IsDivisibleBy(2);
		}

		public static bool IsDivisibleBy(this int n, int divisor)
		{
			return n % divisor == 0;
		}
	}
}
