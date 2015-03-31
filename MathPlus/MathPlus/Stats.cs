using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public enum ComparisonDirection
	{
		Negative = -1,
		Equal = 0,
		Positive = 1
	}

	public class StatsTestResults
	{
		public bool Passed
		{ get; set; }

		public Number Probability
		{ get; set; }
	}

	public static partial class MathPlus
	{
		public static class Stats
		{
			public static List<Number> MakeNumbers(IEnumerable<IComparable> input)
			{
				List<Number> result = new List<Number>();
				foreach (IComparable n in input)
				{
					try
					{
						result.Add((double)n);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException("Type cannot be converted to Number: "
							+ n.GetType().ToString());
					}
				}

				return result;
			}

			public static Number Sum(IEnumerable<Number> values)
			{
				return values.Aggregate((n, total) => total + n);
			}

			public static Number Mean(IEnumerable<Number> values)
			{
				Number sigma = Sum(values);

				return sigma / (double)(values.Count());
			}

			public static Number StandardDev(IEnumerable<Number> values)
			{
				Number mean = Mean(values);
				Func<Number, Number> deviation = (xi) => (xi - mean) * (xi - mean);

				Number sigmaDev = 0;
				foreach (Number i in values)
				{
					sigmaDev += deviation(i);
				}

				int n = values.Count();

				return sigmaDev / (double)(n - 1);
			}

			public static Number Proportion(IEnumerable<bool> sample)
			{
				int on = 0;
				foreach (bool b in sample)
				{
					if (b) on++;
				}
				int total = sample.Count();

				return (double)on / (double)total;
			}

			public static StatsTestResults OnePropZTest(Number H0, ComparisonDirection HA, 
				Number proportion, int n, Number alpha)
			{
				Number q = 1.0 - proportion;

				#region checking
				if (proportion > 1.0 || proportion < 0.0)
				{
					throw new ArgumentOutOfRangeException("proportion", 
						"Proportion cannot be outside of range (0, 1).");
				}

				if (H0 > 1.0 || H0 < 0.0)
				{
					throw new ArgumentOutOfRangeException("H0",
						"Null Hypothesis cannot be outside of range (0, 1).");
				}

				if (proportion * (double)n < 10.0 || q * (double)n < 10.0)
				{
					throw new StatisticInappropriateException(
						"Did not pass 10-successes 10-failures condition.");
				}
				#endregion checking

				Number se = Sqrt((proportion * q) / (double)n);

				NormalModel model = new NormalModel(H0, se);

				Number prob = -1;
				Number z = model.ZScore(proportion);

				if (HA == ComparisonDirection.Negative)
				{
					prob = NormalModel.ProbabilityUnscaled(-99.0, z);
				}
				else if (HA == ComparisonDirection.Positive)
				{
					prob = NormalModel.ProbabilityUnscaled(z, 99.0);
				}
				else if (HA == ComparisonDirection.Equal)
				{
					prob = NormalModel.ProbabilityUnscaled(Abs(z), 99.0) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid ComparisonDirection: " +
						HA.ToString());
				}

				return new StatsTestResults() { Passed = (prob < alpha), Probability = prob };
			}
			public static StatsTestResults TwoPropZTest(ComparisonDirection HA,
				Number p1, Number p2, int n1, int n2, Number alpha)
			{
				Number q1 = 1.0 - p1;
				Number q2 = 1.0 - p2;

				#region checking
				if (p1 > 1.0 || p1 < 0.0)
				{
					throw new ArgumentOutOfRangeException("p1",
						"Proportion 1 cannot be outside of range (0, 1).");
				}
				if (p2 > 1.0 || p2 < 0.0)
				{
					throw new ArgumentOutOfRangeException("p2",
						"Proportion 2 cannot be outside of range (0, 1).");
				}

				if (p1 * (double)n1 < 10.0 || q1 * (double)n1 < 10.0)
				{
					throw new StatisticInappropriateException(
						"Proportion 1 did not pass 10-successes 10-failures condition.");
				}
				if (p2 * (double)n2 < 10.0 || q2 * (double)n2 < 10.0)
				{
					throw new StatisticInappropriateException(
						"Proportion 2 did not pass 10-successes 10-failures condition.");
				}
				#endregion checking

				int total1 = (int)(p1 * n1);
				int total2 = (int)(p2 * n2);

				Number pPooled = (double)(total1 + total2) / (double)(n1 + n2);
				Number qPooled = 1.0 - pPooled;
				Number pDelta = p2 - p1;

				Number sePooled = Sqrt(((pPooled * qPooled) / (double)n1) + 
					((pPooled * qPooled) / (double)n2));

				NormalModel model = new NormalModel(0.0, sePooled);
				Number z = model.ZScore(pDelta);

				Number prob = -1;

				if (HA == ComparisonDirection.Negative)
				{
					prob = NormalModel.ProbabilityUnscaled(-99.0, z);
				}
				else if (HA == ComparisonDirection.Positive)
				{
					prob = NormalModel.ProbabilityUnscaled(z, 99.0);
				}
				else if (HA == ComparisonDirection.Equal)
				{
					prob = NormalModel.ProbabilityUnscaled(Abs(z), 99.0) * 2.0;
				}

				return new StatsTestResults() { Passed = (prob < alpha), Probability = prob };
			}

			
		}
	}
}
