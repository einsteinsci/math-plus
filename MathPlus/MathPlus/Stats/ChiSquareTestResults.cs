using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// Test results for tests using X^2 model
	/// </summary>
	public class ChiSquareTestResults : ITestResults
	{
		/// <summary>
		/// X^2 value tested
		/// </summary>
		public double TestedValue
		{ get; private set; }

		/// <summary>
		/// True if null hypothesis should be rejected, false if this fails
		/// </summary>
		public bool RejectNullHypothesis
		{ get; private set; }

		/// <summary>
		/// Zero. The data fits the model given for the test
		/// </summary>
		public double NullHypothesis
		{
			get
			{
				return 0;
			}
		}

		/// <summary>
		/// X^2 > value. The data does not fit the model given for the test
		/// </summary>
		public InequalityType AltHypothesis
		{
			get
			{
				return InequalityType.GreaterThan;
			}
		}

		/// <summary>
		/// Probability that a X^2 value that high or more given that the 
		/// data fits the model.
		/// </summary>
		public double Probability
		{ get; private set; }

		/// <summary>
		/// Alpha Level. Threshold for Probability value. Values lower than 
		/// this mean the null hypothesis should be rejected.
		/// </summary>
		public double AlphaLevel
		{ get; private set; }

		/// <summary>
		/// Degrees of freedom used in the model
		/// </summary>
		public double DegreesOfFreedom
		{ get; private set; }

		/// <summary>
		/// Model used in the test
		/// </summary>
		public ChiSquareModel Model
		{ get; private set; }

		/// <summary>
		/// Instantiates a ChiSquareTestResults object.
		/// </summary>
		/// <param name="chiSquare">X^2 value calculated by the test</param>
		/// <param name="prob">Probability of the test</param>
		/// <param name="df">Degrees of freedom used in model during test</param>
		/// <param name="alpha">Alpha level used in test</param>
		public ChiSquareTestResults(double chiSquare, double prob, 
			double df, double alpha = .05)
		{
			TestedValue = chiSquare;
			Probability = prob;
			AlphaLevel = alpha;
			DegreesOfFreedom = df;
			Model = new ChiSquareModel(df);
			RejectNullHypothesis = (prob < alpha);
		}

		/// <summary>
		/// Serializes a ChiSquareTestResults object to a list of result pieces
		/// </summary>
		/// <returns>String result</returns>
		public override string ToString()
		{
			string res = "Test results with Model " + Model.ToString();
			if (RejectNullHypothesis)
			{
				res += "  Rejected Null Hypothesis\n";
			}
			else
			{
				res += "  Failed to Reject Null Hypothesis\n";
			}

			res += "  P( X\u00b2 > ";
			res += MathPlus.Numerics.Round(TestedValue, 4).ToString() + " ) = ";
			res += Probability.ToString() + "\n";
			res += "  X\u00b2 = " + TestedValue.ToString() + "\n";
			res += "  df = " + DegreesOfFreedom.ToString() + "\n";
			res += "  \u03B1 = " + AlphaLevel.ToString() + "\n";

			return res;
		}
	}
}
