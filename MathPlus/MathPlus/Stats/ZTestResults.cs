using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// Test results for tests that use the <see cref="NormalModel"/>
	/// </summary>
	public class ZTestResults : ITestResults
	{
		/// <summary>
		/// Proportion for which a Z-Score was generated 
		/// </summary>
		public double TestedValue
		{ get; private set; }

		/// <summary>
		/// Threshold used in test. P-Values lower than this constitute rejecting
		/// the null hypothesis.
		/// </summary>
		public double AlphaLevel
		{ get; private set; }

		/// <summary>
		/// Whether to reject the null hypothesis or not
		/// </summary>
		public bool RejectNullHypothesis
		{ get; private set; }

		/// <summary>
		/// Value for which the tested value is tested against
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
		/// Likelihood of attaining a value as high/low/far as the tested value if the null
		/// hypothesis is true.
		/// </summary>
		public double Probability
		{ get; private set; }

		/// <summary>
		/// Standard error to use when generating the Normal Model
		/// </summary>
		public double StandardError
		{ get; private set; }

		/// <summary>
		/// Model used in test
		/// </summary>
		public NormalModel Model
		{ get; private set; }

		/// <summary>
		/// Instantiates an instance of ZTestResults
		/// </summary>
		/// <param name="tested">Tested proportion in test</param>
		/// <param name="prob">Resulting probability in test</param>
		/// <param name="hNull">Null Hypothesis</param>
		/// <param name="hAlt">Alternative Hypothesis</param>
		/// <param name="alpha">Alpha Level</param>
		/// <param name="se">Standard Error</param>
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
			Model = new NormalModel(hNull, se);
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

			res += "  P( z ";
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
			res += "  \u03B1 = " + AlphaLevel.ToString() + "\n";
			res += "  SE = " + StandardError.ToString() + "\n";

			return res;
		}
	}
}
