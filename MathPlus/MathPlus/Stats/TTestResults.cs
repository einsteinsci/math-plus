using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public class TTestResults : ITestResults
	{
		public double TestedValue
		{ get; private set; }

		public double AlphaLevel
		{ get; private set; }

		public bool RejectNullHypothesis
		{ get; private set; }

		public double Probability
		{ get; private set; }

		public double StandardError
		{ get; private set; }

		public TModel Model
		{ get; private set; }

		public double DegreesOfFreedom
		{ get; private set; }

		public double NullHypothesis
		{ get; private set; }

		public InequalityType AltHypothesis
		{ get; private set; }

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
			AlphaLevel = alpha;

			Model = new TModel(NullHypothesis, StandardError, DegreesOfFreedom);
		}

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
			res += " " + MathPlus.Numerics.Round(Model.TScore(TestedValue), 4).ToString() + " ) = ";
			res += Probability.ToString() + "\n";
			res += "\u03B1 = " + AlphaLevel.ToString() + "\n";
			res += "SE = " + StandardError.ToString() + "\n";
			res += "df = " + DegreesOfFreedom.ToString() + "\n";

			return res;
		}
	}
}
