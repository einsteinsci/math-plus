using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public struct TModel : IModel
	{
		public static readonly Number MAX_LOWER = -1.0 * MathPlus.Pow(10.0, 10.0);

		public Number Mean
		{ get; set; }

		public Number SD
		{ get; set; }

		public Number DF
		{ get; set; }

		public TModel(Number mean, Number sd, Number df)
		{
			Mean = mean;
			SD = sd;
			DF = df;
		}

		public Number TScore(Number value)
		{
			return (value - Mean) / SD;
		}

		public Number Probability(Number low, Number high)
		{
			return ProbabilityUnscaled(TScore(low), TScore(high), DF);
		}

		// Experimental
		public static Number ProbabilityUnscaled(Number tLow, Number tHigh, Number df)
		{
			return cdf(tHigh, df) - cdf(tLow, df);
		}

		// Experimental
		private static Number beta(Number x, Number a, Number b)
		{
			if (x == 1.0)
			{
				return 1.0;
			}

			return MathPlus.Calculus.Integrate(
				(t) => (t ^ (a - 1.0)) * (MathPlus.Pow((1.0 - t), (b - 1.0))), 
				0.0, x, (int)(MathPlus.Abs(a - b) * 30.0), 
				IntegrationType.Trapezoidal);
		}

		// Experimental
		private static Number cdf(Number t, Number v)
		{
			if (t <= MAX_LOWER)
			{
				return 0;
			}

			Func<Number, Number, Number> x = (_t, _v) => _v / ((_t * _t) + _v);

			return MathPlus.Calculus.Integrate(
				(u) => 1.0 - (0.5 * beta(x(t, v), v * 0.5, 0.5)),
				MAX_LOWER, t, 100, IntegrationType.Trapezoidal);
		}
	}
}
