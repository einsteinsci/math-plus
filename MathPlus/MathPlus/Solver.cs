using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public static partial class MathPlus
	{
		/// <summary>
		/// Solves functions for their roots. Currently only has the BrentDekker method.
		/// </summary>
		public static class Solver
		{
			/// <summary>
			/// Solves an equation using the BrentDekker method
			/// </summary>
			/// <param name="func">Function to solve</param>
			/// <param name="lowBound">Lower bound of result</param>
			/// <param name="highBound">Upper bound of result</param>
			/// <param name="precision">Precision value used when solving</param>
			/// <param name="maxDepth">Max depth of interative function.</param>
			/// <returns>Solution to <paramref name="func"/>() == 0</returns>
			/// <exception cref="DivideByZeroException">Thrown if solver fails.</exception>
			public static double SolveBrentDekker(Function2D func, double lowBound, double highBound,
				double precision = 1e-8, int maxDepth = 100)
			{
				double result = double.NaN;
				if (TryBrentDekker(func, lowBound, highBound, precision, maxDepth, out result))
				{
					return result;
				}

				throw new DivideByZeroException("Solver failed.");
			}

			/// <summary>
			/// Attempts to solve an equation using the BrentDekker method.
			/// </summary>
			/// <param name="func">Function to solve</param>
			/// <param name="low">Lower bound of result</param>
			/// <param name="high">Upper bound of result</param>
			/// <param name="precision">Precision value used when solving</param>
			/// <param name="maxDepth">Max depth of iterative function</param>
			/// <param name="result">Solution to <paramref name="func"/>() == 0</param>
			/// <returns>True if solver succeeds, false if it fails.</returns>
			public static bool TryBrentDekker(Function2D func, double low, double high,
				double precision, int maxDepth, out double result)
			{
				double fmin = func(low);
				double fmax = func(high);
				double froot = fmax;
				double d = 0, e = 0;

				result = high;
				double xMid = double.NaN;

				if (Sign(fmin) == Sign(fmax))
				{
					return false;
				}

				for (int i = 0; i <= maxDepth; i++)
				{
					// adjust bounds
					if (Sign(froot) == Sign(fmax))
					{
						high = low;
						fmax = fmin;
						e = d = result - low;
					}

					if (Abs(fmax) < Abs(froot))
					{
						low = result;
						result = high;
						high = low;
						fmin = froot;
						froot = fmax;
						fmax = fmin;
					}

					// convergence check
					double _positiveDoublePrecision = 2 * Pow(2, -53);

					double xAcc1 = _positiveDoublePrecision * Abs(result) + .5 * precision;
					double xMidOld = xMid;
					xMid = (high - result) / 2.0;

					if (Abs(xMid) <= xAcc1 || Numerics.AlmostEqualToNorm(froot, 0, froot, precision))
					{
						return true;
					}

					if (xMid == xMidOld)
					{
						return false;
					}

					if (Abs(e) >= xAcc1 && Abs(fmin) > Abs(froot))
					{
						// attempt inverse quadratic interpolation (?)
						double s = froot / fmin;
						double p;
						double q;
						if (Numerics.AlmostEqualToNorm(low, high, low - high, 10 * Pow(2, -53)))
						{
							p = 2.0 * xMid * s;
							q = 1.0 - s;
						}
						else
						{
							q = fmin / fmax;
							double r = froot / fmax;
							p = s * (2.0 * xMid * q * (q - r) - (result - low) * (r - 1.0));
							q = (q - 1.0) * (r - 1.0) * (s - 1.0);
						}

						if (p > 0.0)
						{
							q = -q;
						}

						p = Abs(p);
						if (2.0 * p < Min(3.0 * xMid * q - Abs(xAcc1 * q), Abs(e * q)))
						{
							e = d;
							d = p / q;
						}
						else
						{
							d = xMid;
							e = d;
						}
					}
					else
					{
						d = xMid;
						e = d;
					}

					low = result;
					fmin = froot;
					if (Abs(d) > xAcc1)
					{
						result += d;
					}
					else
					{
						result += _sign(xAcc1, xMid);
					}

					froot = func(result);
				}

				return false;
			}

			private static double _sign(double a, double b)
			{
				return b >= 0 ? (a >= 0 ? a : -a) : (a >= 0 ? -a : a);
			}
		}
	}
}
