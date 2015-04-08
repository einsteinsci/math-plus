using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public class TTestResults : ITestResults
	{
		public TTestResults(double tested, double prob, double hNull,
			InequalityType alt, double alpha, double se, double df)
		{
			TestedValue = tested;
			RejectNullHypothesis = prob < alpha;
			Probability = prob;
			StandardError = se;
			DegreesOfFreedom = df;
			NullHypothesis = hNull;
			AltHypothesis = alt;
		}

		public double TestedValue
		{ get; private set; }

		public bool RejectNullHypothesis
		{ get; private set; }

		public double Probability
		{ get; private set; }

		public double StandardError
		{ get; private set; }

		public double DegreesOfFreedom
		{ get; private set; }

		public double NullHypothesis
		{ get; private set; }

		public InequalityType AltHypothesis
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

			res += "P( t[" + MathPlus.Numerics.Round(DegreesOfFreedom, 4).ToString() + "] ";
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
			res += "SE = " + StandardError + "\n";
			res += "df = " + DegreesOfFreedom + "\n";

			return res;
		}
	}
}
