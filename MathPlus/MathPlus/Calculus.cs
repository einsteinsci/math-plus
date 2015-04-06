using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	/// <summary>
	/// Represents a function that can be plotted on a 2D plane
	/// </summary>
	/// <param name="x">X input of the function</param>
	/// <returns>Y output of the function</returns>
	public delegate double Function2D(double x);
	/// <summary>
	/// Represents a function that can be plotted in a 3D space
	/// </summary>
	/// <param name="x">X input of the function</param>
	/// <param name="y">Y input of the function</param>
	/// <returns>Z output of the function</returns>
	public delegate double Function3D(double x, double y);
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
			public static double FuncMin(Function2D exp, double lower, 
				double upper, double increment)
			{
				double min = exp(lower);
				double res = lower;

				for (double x = lower; x <= upper; x += increment)
				{
					double y = exp(x);
					if (y < min)
					{
						min = y;
						res = x;
					}
				}

				return res;
			}
			public static double FuncMax(Function2D exp, double lower,
				double upper, double increment)
			{
				double max = exp(lower);
				double res = lower;

				for (double x = lower; x <= upper; x += increment)
				{
					double y = exp(x);
					if (y > max)
					{
						max = y;
						res = x;
					}
				}

				return res;
			}

			public static double Derivative(Function2D function, 
				double point, double deltaX)
			{
				double yMinus = function(point - deltaX);
				double yPlus = function(point + deltaX);

				return (yPlus - yMinus) / (deltaX * 2.0);
			}

			private static double IntegrateLow(Function2D function, double lower,
				double upper, int divisions)
			{
				double increment = (upper - lower) / divisions;
				double area = 0;
				for (double x = lower; x < upper; x += increment)
				{
					area += function(x) * increment;
				}

				return area;
			}
			private static double IntegrateHigh(Function2D function, double lower,
				double upper, int divisions)
			{
				double increment = (upper - lower) / divisions;
				double area = 0;
				for (double x = lower; x < upper; x += increment)
				{
					area += function(x + increment) * increment;
				}

				return area;
			}
			private static double IntegrateMidpoint(Function2D function, 
				double lower, double upper, int divisions)
			{
				double increment = (upper - lower) / divisions;
				double area = 0;
				for (double x = lower; x < upper; x += increment)
				{
					double mid = (2.0 * x + increment) / 2.0;
					area += function(mid) * increment;
				}

				return area;
			}
			private static double IntegrateTrapezoidal(Function2D function,
				double lower, double upper, int divisions)
			{
				double increment = (upper - lower) / divisions;
				double area = 0;
				for (double x = lower; x < upper; x += increment)
				{
					double a1 = function(x);
					double a2 = function(x + increment);
					area += ((a1 + a2) / 2.0) * increment;
				}

				return area;
			}
			private static double IntegrateSimpson(Function2D function,
				double lower, double upper, int divisions)
			{
				if (divisions % 2 != 0)
				{
					throw new ArgumentException(
						"double of segments must be even for Simpson's Rule.");
				}

				double increment = (upper - lower) / divisions;
				double total = function(lower);
				bool odd = true;
				for (double x = lower + increment; x < upper; x += increment)
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
			public static double Integrate(Function2D function, double lower,
				double upper, int divisions, IntegrationType type)
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
