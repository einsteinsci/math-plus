using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public struct NormalModel : IModel
	{
		public double Mean
		{ get; set; }

		public double SD
		{ get; set; }

		public NormalModel(double mu, double sigma) : this()
		{
			Mean = mu;
			SD = sigma;
		}

		public double ZScore(double value)
		{
			return (value - Mean) / SD;
		}

		public double Probability(double bottom, double top)
		{
			return ProbabilityUnscaled(ZScore(bottom), ZScore(top));
		}

		public override string ToString()
		{
			return "N(" + Mean.ToString() + ", " + SD.ToString() + ")";
		}

		public static double ProbabilityUnscaled(double zBot, double zTop)
		{
			return phi(zTop) - phi(zBot);
		}

		public static double Inverse(double p)
		{
			Function2D rationalApproximation = (t) =>
			{
				// A & S formula 26.2.23
				// |error| < 4.5e-4
				double[] c = new double[] { 2.515517, 0.802853, 0.010328 };
				double[] d = new double[] { 1.432788, 0.189269, 0.001308 };

				return t - ((c[2] * t + c[1]) * t + c[0]) /
					(((d[2] * t + d[1]) * t + d[0]) * t + 1.0);
			};

			if (p <= 0.0 || p >= 1.0)
			{
				throw new ArgumentOutOfRangeException("p",
					"Probability must be between 0 and 1.");
			}

			if (p < 0.5)
			{
				// F^-1(p) = - G^-1(p)
				return -rationalApproximation(MathPlus.Sqrt(-2.0 * MathPlus.Ln(p)));
			}
			else
			{
				// F^-1(p) = G^-1(1 - p)
				return rationalApproximation(MathPlus.Sqrt(-2.0 * MathPlus.Ln(1.0 - p)));
			}
		}

		private static double phi(double x)
		{
			// constants
			double a1 = 0.254829592;
			double a2 = -0.284496736;
			double a3 = 1.421413741;
			double a4 = -1.453152027;
			double a5 = 1.061405429;
			double p = 0.3275911;

			// save the sign of x
			int sign = x < 0 ? -1 : 1;
			x = MathPlus.Abs(x) / MathPlus.Sqrt(2.0);

			// A&S formula 7.1.26
			double t = 1.0 / (1.0 + (p * x));
			double y = 1.0 / (((((a5 * t + a4) * t) + a3) * t + a2) * 
				t + a1) * t * MathPlus.Exp(-x * x);
			return 0.5 * (1.0 + sign * y);
		}
	}
}
