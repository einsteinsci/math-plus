using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public struct NormalModel : IModel
	{
		public Number Mean
		{ get; set; }

		public Number SD
		{ get; set; }

		public NormalModel(Number mu, Number sigma) : this()
		{
			Mean = mu;
			SD = sigma;
		}

		public Number Probability(Number value)
		{
			throw new NotImplementedException();
		}

		private static Number phi(double x)
		{
			// constants
			Number a1 = 0.254829592;
			Number a2 = -0.284496736;
			Number a3 = 1.421413741;
			Number a4 = -1.453152027;
			Number a5 = 1.061405429;
			Number p = 0.3275911;

			// save the sign of x
			int sign = x < 0 ? -1 : 1;
			x = MathPlus.Abs(x) / MathPlus.Sqrt(2.0);

			// A&S formula 7.1.26
			Number t = 1.0 / (1.0 + (p * x));
			Number y = 1.0 / (((((a5 * t + a4) * t) + a3) * t + a2) * 
				t + a1) * t * MathPlus.Exp(-x * x);
			return 0.5 * (1.0 + sign * y);
		}
	}
}
