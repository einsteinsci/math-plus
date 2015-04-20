using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public class ChiSquareTestResults : ITestResults
	{
		public double TestedValue
		{ get; private set; }

		public bool RejectNullHypothesis
		{ get; private set; }

		public double NullHypothesis
		{
			get
			{
				return 0;
			}
		}

		public InequalityType AltHypothesis
		{
			get
			{
				return InequalityType.GreaterThan;
			}
		}

		public double Probability
		{ get; private set; }

		public double AlphaLevel
		{ get; private set; }

		public double DegreesOfFreedom
		{ get; private set; }

		public ChiSquareModel Model
		{ get; private set; }

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
	}
}
