using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
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

		public double Probability(double low, double high)
		{
			return ProbabilityUnscaled(TScore(low), TScore(high), DF);
		}

		public static double ProbabilityUnscaled(double tLow, double tHigh, double df)
		{
			return cdf(tHigh, df) - cdf(tLow, df);
		}

		private static double cdf(double t, double v)
		{
			if (t <= MAX_LOWER)
			{
				return 0;
			}

			Function2D gamma = (n) =>
			{
				return MathPlus.Calculus.Integrate((x) =>
				{
					return MathPlus.Pow(x, n - 1.0) * MathPlus.Pow(MathPlus.E, -x);
				}, 0, 100000, 1000, IntegrationType.Simpson);
			};

			double part1 = gamma((v + 1.0) / 2.0) / gamma(v / 2.0);
			double part2 = 1.0 / MathPlus.Sqrt(v * MathPlus.PI);
			double part3 = MathPlus.Calculus.Integrate(
				(_t) => 1.0 / ((1.0 + (_t * _t) / v) * ((v + 1.0) / 2.0)), 
				-10000, t, 100, IntegrationType.Simpson);

			return part1 * part2 * part3;
		}

		public static double Inverse(double prob, double df)
		{
			throw new NotImplementedException(
				"Apparently nobody on the planet knows how to do this.");
		}
	}
}
