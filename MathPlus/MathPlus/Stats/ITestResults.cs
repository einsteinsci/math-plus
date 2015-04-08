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
		{ get; set; }

		bool RejectNullHypothesis
		{ get; set; }

		double NullHypothesis
		{ get; set; }

		InequalityType AltHypothesis
		{ get; set; }

		double Probability
		{ get; set; }
	}
}
