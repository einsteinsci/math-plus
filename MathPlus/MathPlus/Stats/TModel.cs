using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public struct TModel : IModel
	{
		public static readonly double MAX_LOWER = -1.0 * MathPlus.Pow(10.0, 10.0);

		public double Mean
		{ get; set; }

		public double SD
		{ get; set; }

		public double DF
		{ get; set; }

		public TModel(double mean, double sd, double df) : this()
		{
			Mean = mean;
			SD = sd;
			DF = df;
		}

		public double TScore(double value)
		{
			return (value - Mean) / SD;
		}

		public double ScaledCDF(double low, double high)
		{
			return CDF(TScore(low), TScore(high), DF);
		}
		public static double CDF(double tHigh, double df)
		{
			return cdf(tHigh, df);
		}
		public static double CDF(double tLow, double tHigh, double df)
		{
			return cdf(tHigh, df) - cdf(tLow, df);
		}

		private static double cdf(double t, double v)
		{
			if (v <= 0.0)
			{
				throw new ArgumentException("df must be positive and non-zero.");
			}

			if (double.IsPositiveInfinity(v))
			{
				return NormalModel.CDF(-999, t);
			}

			double h = v / (v + (t * t));
			double ib = 0.5 * betaReg(v / 2.0, 0.5, h);
			return t <= 0.0 ? ib : 1.0 - ib;
		}

		private static double betaReg(double a, double b, double x)
		{
			double bt = (x == 0.0 || x == 1.0) ? 0 :
				MathPlus.Pow(MathPlus.E, gammaLn(a + b) - gammaLn(a) - gammaLn(b) +
				(a * MathPlus.Ln(x)) + (b * MathPlus.Ln(1.0 - x)));

			bool symmTrans = x >= (a + 1.0) / (a + b + 2.0);

			double eps = MathPlus.Pow(2, -53.0);
			double fpmin = (0.0).RoundUp() / eps;

			if (symmTrans)
			{
				x = 1.0 - x;
				double swap = a;
				a = b;
				b = swap;
			}

			double qab = a + b;
			double qap = a + 1.0;
			double qam = a - 1.0;
			double c = 1;
			double d = 1.0 - (qab * x / qap);

			if (MathPlus.Abs(d) < fpmin)
			{
				d = fpmin;
			}

			d = 1.0 / d;
			double h = d;

			for (int m = 1, m2 = 2; m <= 140; m++, m2 += 2)
			{
				double aa = m * (b - m) * x / ((qam + m2) * (a + m2));
				d = 1.0 + (aa * d);

				if (MathPlus.Abs(d) < fpmin)
				{
					d = fpmin;
				}

				c = 1.0 + (aa / c);
				if (MathPlus.Abs(c) < fpmin)
				{
					c = fpmin;
				}

				d = 1.0 / d;
				h *= d * c;
				aa = -(a + m) * (qab + m) * x / ((a + m2) * (qap + m2));
				d = 1.0 + (aa * d);

				if (MathPlus.Abs(d) < fpmin)
				{
					d = fpmin;
				}

				c = 1.0 + (aa / c);

				if (MathPlus.Abs(c) < fpmin)
				{
					c = fpmin;
				}

				d = 1.0 / d;
				double del = d * c;
				h *= del;

				if (MathPlus.Abs(del - 1.0) <= eps)
				{
					return symmTrans ? 1.0 - (bt * h / a) : bt * h / a;
				}
			}

			return symmTrans ? 1.0 - (bt * h / a) : bt * h / a;
		}

		private static double gammaLn(double z)
		{
			double[] gammaDk =  {
				 2.48574089138753565546e-5,
				 1.05142378581721974210,
				-3.45687097222016235469,
				 4.51227709466894823700,
				-2.98285225323576655721,
				 1.05639711577126713077,
				-1.95428773191645869583e-1,
				 1.70970543404441224307e-2,
				-5.71926117404305781283e-4,
				 4.63399473359905636708e-6,
				-2.71994908488607703910e-9
			};

			double log2SqrtEOverPi = MathPlus.Ln(2.0 * MathPlus.Sqrt(MathPlus.E / MathPlus.PI));
			const double gammaR = 10.900511;

			if (z < .5)
			{
				double s = gammaDk[0];
				for (int i = 1; i <= 10; i++)
				{
					s += gammaDk[i] / (i - z);
				}

				return MathPlus.Ln(MathPlus.PI) -
					MathPlus.Ln(MathPlus.Trig.Sin(MathPlus.PI * z)) -
					MathPlus.Ln(s) -
					log2SqrtEOverPi -
					((.5 - z) * MathPlus.Ln((.5 - z + gammaR) / MathPlus.E));
			}
			else
			{
				double s = gammaDk[0];
				for (int i = 1; i <= 10; i++)
				{
					s += gammaDk[i] / (z + i - 1.0);
				}

				return MathPlus.Ln(s) + log2SqrtEOverPi +
					((z - .5) * MathPlus.Ln((z - .5 + gammaR) / MathPlus.E));
			}
		}

		public static double InverseCDF(double prob, double df)
		{
			if (df <= 0)
			{
				throw new ArgumentOutOfRangeException("df",
					"Degrees of freedom must be above zero.");
			}
			if (double.IsPositiveInfinity(df))
			{
				return NormalModel.InverseCDF(prob);
			}

			if (prob == 0.5)
			{
				return 0;
			}

			return MathPlus.Solver.SolveBrentDekker((x) =>
			{
				double h = df / (df + (x * x));
				double ib = 0.5 * betaReg(df / 2.0, 0.5, h);
				return x <= 0.0 ? ib - prob : 1.0 - ib - prob;
			}, -800, 800, precision: 1e-12);
		}
	}
}
