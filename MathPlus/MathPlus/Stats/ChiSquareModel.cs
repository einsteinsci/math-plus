using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// X^2 model for multiple counts
	/// </summary>
	public struct ChiSquareModel : IModel
	{
		/// <summary>
		/// Degrees of freedom used by the model
		/// </summary>
		public double DegreesOfFreedom
		{ get; private set; }

		/// <summary>
		/// Creates an instance of ChiSquareModel
		/// </summary>
		/// <param name="df">Degrees of freedom</param>
		public ChiSquareModel(double df) : this()
		{
			DegreesOfFreedom = df;
		}

		/// <summary>
		/// Calculates the CDF of a section of the ChiSquareModel
		/// </summary>
		/// <param name="bottom">Lower bound of CDF</param>
		/// <param name="top">Upper bound of CDF</param>
		/// <returns>CDF of a section of the ChiSquareModel</returns>
		public double ScaledCDF(double bottom, double top)
		{
			return CDF(top, DegreesOfFreedom) - CDF(bottom, DegreesOfFreedom);
		}

		/// <summary>
		/// Calculates the CDF of the upper side of a ChiSquareModel
		/// </summary>
		/// <param name="value">Lower bound of CDF</param>
		/// <returns>CDF of the ChiSquareModel from value to infinity</returns>
		public double CDF(double value)
		{
			return CDF(value, DegreesOfFreedom);
		}

		/// <summary>
		/// Calculates the CDF of the upper side of a ChiSquareModel
		/// </summary>
		/// <param name="chiSquareValue">Lower bound of CDF</param>
		/// <param name="df">Degrees of freedom used for CDF</param>
		/// <returns>CDF of the ChiSquareModel from value to infinity</returns>
		public static double CDF(double chiSquareValue, double df)
		{
			return 1 - _cdf(chiSquareValue, df);
		}

		/// <summary>
		/// Serializes the model to a simple readable string in the format X^2(df).
		/// </summary>
		/// <returns>String form of the model</returns>
		public override string ToString()
		{
			return "X\u00b2(" + DegreesOfFreedom.ToString() + ")";
		}

		private static double _cdf(double x, double df)
		{
			return _gammaLowReg(df / 2.0, x / 2.0);
		}

		private static double _gammaLowReg(double a, double x)
		{
			const double epsilon = 0.000000000000001;
			const double big = 4503599627370496.0;
			const double bigInv = 2.22044604925031308085e-16;

			if (a < 0d)
			{
				throw new ArgumentOutOfRangeException("a", "a cannot be negative.");
			}

			if (x < 0d)
			{
				throw new ArgumentOutOfRangeException("x", "x cannot be negative.");
			}

			if (MathPlus.Numerics.AlmostEqualTo(a, 0.0))
			{
				if (MathPlus.Numerics.AlmostEqualTo(x, 0.0))
				{
					//use right hand limit value because so that regularized upper/lower gamma definition holds.
					return 1d;
				}

				return 1d;
			}

			if (MathPlus.Numerics.AlmostEqualTo(x, 0.0))
			{
				return 0.0;
			}

			double ax = (a * Math.Log(x)) - x - _gammaLn(a);
			if (ax < -709.78271289338399)
			{
				return a < x ? 1d : 0d;
			}

			if (x <= 1 || x <= a)
			{
				double r2 = a;
				double c2 = 1;
				double ans2 = 1;

				do
				{
					r2 = r2 + 1;
					c2 = c2 * x / r2;
					ans2 += c2;
				}
				while ((c2 / ans2) > epsilon);

				return Math.Exp(ax) * ans2 / a;
			}

			int c = 0;
			double y = 1 - a;
			double z = x + y + 1;

			double p3 = 1;
			double q3 = x;
			double p2 = x + 1;
			double q2 = z * x;
			double ans = p2 / q2;

			double error;

			do
			{
				c++;
				y += 1;
				z += 2;
				double yc = y * c;

				double p = (p2 * z) - (p3 * yc);
				double q = (q2 * z) - (q3 * yc);

				if (q != 0)
				{
					double nextans = p / q;
					error = Math.Abs((ans - nextans) / nextans);
					ans = nextans;
				}
				else
				{
					// zero div, skip
					error = 1;
				}

				// shift
				p3 = p2;
				p2 = p;
				q3 = q2;
				q2 = q;

				// normalize fraction when the numerator becomes large
				if (Math.Abs(p) > big)
				{
					p3 *= bigInv;
					p2 *= bigInv;
					q3 *= bigInv;
					q2 *= bigInv;
				}
			}
			while (error > epsilon);

			return 1d - (Math.Exp(ax) * ans);
		}

		private static double _gammaLn(double z)
		{
			double[] gammaDk = 
			{
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
			const double gammaR = 10.900511;

			const double log2SqrtEOverPi = 0.6207822376352452223455184457816472122518527279025978;

			if (z < 0.5)
			{
				double s = gammaDk[0];
				for (int i = 1; i <= 10; i++)
				{
					s += gammaDk[i] / (i - z);
				}

				return MathPlus.Ln(MathPlus.PI) - MathPlus.Ln(MathPlus.Trig.Sin(MathPlus.PI * z)) - 
					MathPlus.Ln(s) - log2SqrtEOverPi - 
					((0.5 - z) * MathPlus.Ln((0.5 - z + gammaR) / MathPlus.E));
			}
			else
			{
				double s = gammaDk[0];
				for (int i = 1; i <= 10; i++)
				{
					s += gammaDk[i] / (z + i - 1.0);
				}

				return Math.Log(s) + log2SqrtEOverPi + 
					((z - 0.5) * Math.Log((z - 0.5 + gammaR) / Math.E));
			}
		}
	}
}
