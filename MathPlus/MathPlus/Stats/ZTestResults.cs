using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public class ZTestResults : ITestResults
	{
		public ZTestResults(double tested, double prob, double hNull, 
			InequalityType hAlt, double alpha, double se)
		{
			TestedValue = tested;
			RejectNullHypothesis = prob < alpha;
			NullHypothesis = hNull;
			AltHypothesis = hAlt;
			Probability = prob;
			StandardError = se;
			AlphaLevel = alpha;
		}

		public double TestedValue
		{ get; private set; }

		public double AlphaLevel
		{ get; private set; }

		public bool RejectNullHypothesis
		{ get; private set; }

		public double NullHypothesis
		{ get; private set; }

		public InequalityType AltHypothesis
		{ get; private set; }

		public double Probability
		{ get; private set; }

		public double StandardError
		{ get; private set; }

		public override string ToString()
		{
			string res = "";
			if (RejectNullHypothesis)
			{
				res += "Rejected Null Hypothesis\n";
			}
			else
			{
				res += "Failed to Reject Null Hypothesis\n";
			}

			res += "P( z ";
			if (AltHypothesis == InequalityType.GreaterThan)
			{
				res += ">";
			}
			else if (AltHypothesis == InequalityType.LessThan)
			{
				res += "<";
			}
			else
			{
				res += "!=";
			}
			res += " " + MathPlus.Numerics.Round(TestedValue, 4).ToString() + " ) = ";
			res += Probability.ToString() + "\n";
			res += "\u03B1 = " + AlphaLevel.ToString() + "\n";
			res += "SE = " + StandardError.ToString() + "\n";

			return res;
		}
	}
}
