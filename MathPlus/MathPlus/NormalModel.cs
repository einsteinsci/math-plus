using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
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

		public static double Inverse(double probability)
		{
			throw new NotImplementedException();
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
