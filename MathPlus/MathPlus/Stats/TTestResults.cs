using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// Test results for tests using the Student's <see cref="TModel"/>
	/// </summary>
	public class TTestResults : ITestResults
	{
		/// <summary>
		/// Mean value for which a T-Score was generated
		/// </summary>
		public double TestedValue
		{ get; private set; }

		/// <summary>
		/// Threshold for rejecting the null hypothesis. P-Values lower than this must reject.
		/// </summary>
		public double AlphaLevel
		{ get; private set; }

		/// <summary>
		/// Whether the null hypothesis has been rejected in the test.
		/// </summary>
		public bool RejectNullHypothesis
		{ get; private set; }

		/// <summary>
		/// Likelihood that a value this low/high/far would occur if the null hypothesis is true.
		/// </summary>
		public double Probability
		{ get; private set; }

		/// <summary>
		/// Standard Error used in the model
		/// </summary>
		public double StandardError
		{ get; private set; }

		/// <summary>
		/// Model used to generate T-Score
		/// </summary>
		public TModel Model
		{ get; private set; }

		/// <summary>
		/// Degrees of freedom used in model
		/// </summary>
		public double DegreesOfFreedom
		{ get; private set; }

		/// <summary>
		/// Value for which the tested model is compared to
		/// </summary>
		public double NullHypothesis
		{ get; private set; }

		/// <summary>
		/// Direction to assume the actual value is relative to the null hypothesis,
		/// if it is rejected.
		/// </summary>
		public InequalityType AltHypothesis
		{ get; private set; }

		/// <summary>
		/// Instantiates a new TTestResults
		/// </summary>
		/// <param name="tested">Tested mean value in test</param>
		/// <param name="prob">Resulting probability from test</param>
		/// <param name="hNull">Null hypothesis</param>
		/// <param name="alt">Alternate hypothesis direction</param>
		/// <param name="alpha">Alpha level</param>
		/// <param name="se">Standard error used in model</param>
		/// <param name="df">Degrees of freedom used in model</param>
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

		/// <summary>
		/// Serializes test results into a multiline list of result values
		/// </summary>
		/// <returns>String optimized for multiline output</returns>
		public override string ToString()
		{
			string res = "Test results with model " + Model.ToString();
			if (RejectNullHypothesis)
			{
				res += "  Rejected Null Hypothesis\n";
			}
			else
			{
				res += "  Failed to Reject Null Hypothesis\n";
			}

			res += "  P( t[" + MathPlus.Numerics.Round(DegreesOfFreedom, 4).ToString() + "] ";
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
			res += "  \u03B1 = " + AlphaLevel.ToString() + "\n";
			res += "  SE = " + StandardError.ToString() + "\n";
			res += "  df = " + DegreesOfFreedom.ToString() + "\n";

			return res;
		}
	}
}
