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
				double lower, double upper, int divisions = 2)
			{
				double increment = (upper - lower) / divisions;
				double offset = increment;
				double sum = 0.5 * (function(lower) + function(upper));
				for (double i = 0; i < divisions - 1; i++)
				{
					sum += function(lower + offset);
					offset += increment;
				}

				return increment * sum;
			}
			private static double IntegrateSimpson(Function2D function,
				double lower, double upper, int divisions = 3)
			{
				if (divisions.IsOdd())
				{
					throw new ArgumentException("Division count must be even.", "divisions");
				}

				double step = (lower - upper) / (double)divisions;
				double factor = step / 3.0;

				double offset = step;
				int m = 4;
				double sum = function(lower) + function(upper);
				for (int i = 0; i < divisions - 1; i++)
				{
					sum += m * function(lower + offset);
					m = 6 - m; // alternate 4 and 2
					offset += step;
				}

				return factor * sum;
			}
			public static double Integrate(Function2D function, double lower,
				double upper, int divisions, IntegrationType type)
			{
				if (function == null)
				{
					throw new ArgumentNullException("function");
				}

				if (divisions <= 0)
				{
					throw new ArgumentOutOfRangeException("divisions",
						"Division count must be positive and greater than zero.");
				}

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
					try
					{
						return IntegrateSimpson(function, lower, upper, divisions);
					}
					catch (ArgumentException e)
					{
						throw e;
					}
				}

				throw new ArgumentOutOfRangeException("Invalid IntegrationType " +
					type.ToString());
			}
		}
	}
}
