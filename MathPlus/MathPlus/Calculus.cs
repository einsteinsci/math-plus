using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public delegate Number Function2D(Number x);
	public delegate Number Function3D(Number x, Number y);
	public delegate Complex FunctionComplex(Complex input);

	public static partial class MathPlus
	{
		public static class Calculus
		{
			public static Number FuncMin(Function2D exp, Number lower, 
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
			public static Number FuncMax(Function2D exp, Number lower,
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

			public static Number IntegrateLow(Function2D function, Number lower,
				Number upper, Number increment)
			{
				/// TODO: stuff
				return -1;
			}
		}
	}
}
