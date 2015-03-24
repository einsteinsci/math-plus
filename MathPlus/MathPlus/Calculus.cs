using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public delegate Number FunctionXY(Number input);

	public static partial class MathPlus
	{
		public static class Calculus
		{
			public static Number FuncMin(FunctionXY exp, Number lower, 
				Number upper, Number increment)
			{
				Number min = exp(lower);
				Number res = lower;

				for (Number x = lower; x <= upper; x += increment)
				{
					Number y = exp(x);
					if (y < min)
					{
						min = y;
						res = x;
					}
				}

				return res;
			}
			public static Number FuncMax(FunctionXY exp, Number lower,
				Number upper, Number increment)
			{
				Number max = exp(lower);
				Number res = lower;

				for (Number x = lower; x <= upper; x += increment)
				{
					Number y = exp(x);
					if (y > max)
					{
						max = y;
						res = x;
					}
				}

				return res;
			}


		}
	}
}
