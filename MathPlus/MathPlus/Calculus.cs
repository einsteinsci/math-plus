using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	/// <summary>
	/// Represents a function that can be plotted on a 2D plane
	/// </summary>
	/// <param name="x">X input of the function</param>
	/// <returns>Y output of the function</returns>
	public delegate Number Function2D(Number x);
	/// <summary>
	/// Represents a function that can be plotted in a 3D space
	/// </summary>
	/// <param name="x">X input of the function</param>
	/// <param name="y">Y input of the function</param>
	/// <returns>Z output of the function</returns>
	public delegate Number Function3D(Number x, Number y);
	/// <summary>
	/// Represents an input-output mathematical function
	/// </summary>
	/// <param name="input">Function input</param>
	/// <returns>Function output</returns>
	public delegate Complex FunctionComplex(Complex input);

	public enum IntegrationType
	{
		Low,
		High,
		Midpoint,
		Trapezoidal,
		[Obsolete("Simpson integration not completely implemented")]
		Simpson
	}

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

			public static Number Derivative(Function2D function, 
				Number point, Number deltaX)
			{
				Number yMinus = function(point - deltaX);
				Number yPlus = function(point + deltaX);

				return (yPlus - yMinus) / (deltaX * 2.0);
			}

			private static Number IntegrateLow(Function2D function, Number lower,
				Number upper, int divisions)
			{
				Number increment = (upper - lower) / divisions;
				Number area = 0;
				for (Number x = lower; x < upper; x += increment)
				{
					area += function(x) * increment;
				}

				return area;
			}
			private static Number IntegrateHigh(Function2D function, Number lower,
				Number upper, int divisions)
			{
				Number increment = (upper - lower) / divisions;
				Number area = 0;
				for (Number x = lower; x < upper; x += increment)
				{
					area += function(x + increment) * increment;
				}

				return area;
			}
			private static Number IntegrateMidpoint(Function2D function, 
				Number lower, Number upper, int divisions)
			{
				Number increment = (upper - lower) / divisions;
				Number area = 0;
				for (Number x = lower; x < upper; x += increment)
				{
					Number mid = (2.0 * x + increment) / 2.0;
					area += function(mid) * increment;
				}

				return area;
			}
			private static Number IntegrateTrapezoidal(Function2D function,
				Number lower, Number upper, int divisions)
			{
				Number increment = (upper - lower) / divisions;
				Number area = 0;
				for (Number x = lower; x < upper; x += increment)
				{
					Number a1 = function(x);
					Number a2 = function(x + increment);
					area += ((a1 + a2) / 2.0) * increment;
				}

				return area;
			}
			private static Number IntegrateSimpson(Function2D function,
				Number lower, Number upper, int divisions)
			{
				if (divisions % 2 != 0)
				{
					throw new ArgumentException(
						"Number of segments must be even for Simpson's Rule.");
				}

				Number increment = (upper - lower) / divisions;
				Number total = function(lower);
				bool odd = true;
				for (Number x = lower + increment; x < upper; x += increment)
				{
					if (odd)
					{
						total += 4.0 * function(x);
					}
					else
					{
						total += 2.0 * function(x);
					}
				}
				total += function(upper);

				return increment * total / 3.0;
			}
			public static Number Integrate(Function2D function, Number lower,
				Number upper, int divisions, IntegrationType type)
			{
				if (lower == upper)
				{
					return 0;
				}

				switch (type)
				{
				case IntegrationType.Low:
					return IntegrateLow(function, lower, upper, divisions);
				case IntegrationType.High:
					return IntegrateHigh(function, lower, upper, divisions);
				case IntegrationType.Midpoint:
					return IntegrateMidpoint(function, lower, upper, divisions);
				case IntegrationType.Trapezoidal:
					return IntegrateTrapezoidal(function, lower, upper, divisions);
				case IntegrationType.Simpson:
					return IntegrateSimpson(function, lower, upper, divisions);
				}

				throw new ArgumentOutOfRangeException("Invalid IntegrationType " +
					type.ToString());
			}
		}
	}
}
