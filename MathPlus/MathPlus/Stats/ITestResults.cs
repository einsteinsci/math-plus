using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public interface ITestResults
	{
		double TestedValue
		{ get; }

		bool RejectNullHypothesis
		{ get; }

		double NullHypothesis
		{ get; }

		InequalityType AltHypothesis
		{ get; }

		double Probability
		{ get; }
	}
}
