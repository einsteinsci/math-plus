using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public enum InequalityType
	{
		LessThan = -1,
		NotEqual = 0,
		GreaterThan = 1
	}

	public class ZTestResults
	{
		public ZTestResults(Number prob, Number alpha, Number se)
		{
			RejectNullHypothesis = prob < alpha;
			Probability = prob;
			StandardError = se;
		}

		public bool RejectNullHypothesis
		{ get; private set; }

		public Number Probability
		{ get; private set; }

		public Number StandardError
		{ get; private set; }
	}

	public class TTestResults
	{
		public TTestResults(Number prob, Number alpha, Number se, Number df)
		{
			RejectNullHypothesis = prob < alpha;
			Probability = prob;
			StandardError = se;
			DegreesOfFreedom = df;
		}

		public bool RejectNullHypothesis
		{ get; private set; }

		public Number Probability
		{ get; private set; }

		public Number StandardError
		{ get; private set; }

		public Number DegreesOfFreedom
		{ get; private set; }
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

			public static ZTestResults OnePropZTest(Number p0, InequalityType HA, 
				Number proportion, int n, Number alpha)
			{
				Number q = 1.0 - proportion;

				#region checking
				if (proportion > 1.0 || proportion < 0.0)
				{
					throw new ArgumentOutOfRangeException("proportion", 
						"Proportion cannot be outside of range (0, 1).");
				}

				if (p0 > 1.0 || p0 < 0.0)
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

				NormalModel model = new NormalModel(p0, se);

				Number prob = -1;
				Number z = Constrain(model.ZScore(proportion), -98.9, 98.9);

				if (HA == InequalityType.LessThan)
				{
					prob = NormalModel.ProbabilityUnscaled(-99.0, z);
				}
				else if (HA == InequalityType.GreaterThan)
				{
					prob = NormalModel.ProbabilityUnscaled(z, 99.0);
				}
				else if (HA == InequalityType.NotEqual)
				{
					prob = NormalModel.ProbabilityUnscaled(Abs(z), 99.0) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid InequalityType: " +
						HA.ToString());
				}

				return new ZTestResults(prob, alpha, se);
			}
			public static ZTestResults OnePropZTest(Number p0, InequalityType HA, 
				Number proportion, int n)
			{
				return OnePropZTest(p0, HA, proportion, n, 0.05);
			}
			public static ZTestResults TwoPropZTest(InequalityType HA,
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
				Number z = Constrain(model.ZScore(pDelta), -98.9, 98.9);

				Number prob = -1;

				if (HA == InequalityType.LessThan)
				{
					prob = NormalModel.ProbabilityUnscaled(-99.0, z);
				}
				else if (HA == InequalityType.GreaterThan)
				{
					prob = NormalModel.ProbabilityUnscaled(z, 99.0);
				}
				else if (HA == InequalityType.NotEqual)
				{
					prob = NormalModel.ProbabilityUnscaled(Abs(z), 99.0) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid InequalityType: " +
						HA.ToString());
				}

				return new ZTestResults(prob, alpha, sePooled);
			}
			public static ZTestResults TwoPropZTest(InequalityType HA,
				Number p1, Number p2, int n1, int n2)
			{
				return TwoPropZTest(HA, p1, p2, n1, n2, 0.05);
			}

			public static TTestResults OneSampleTTest(Number mu0, InequalityType HA,
				Number mean, Number sd, int n, Number alpha)
			{
				Number se = sd / Sqrt(n);
				Number df = n - 1;

				TModel model = new TModel(mu0, se, df);
				Number prob = -1;
				Number t = Constrain(model.TScore(mean), -98.9, 98.9);

				if (HA == InequalityType.LessThan)
				{
					prob = TModel.ProbabilityUnscaled(-99.0, t, df);
				}
				else if (HA == InequalityType.GreaterThan)
				{
					prob = TModel.ProbabilityUnscaled(t, 99.0, df);
				}
				else if (HA == InequalityType.NotEqual)
				{
					prob = TModel.ProbabilityUnscaled(Abs(t), 99.0, df) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid InequalityType: " +
						HA.ToString());
				}

				return new TTestResults(prob, alpha, se, df);
			}
			public static TTestResults OneSampleTTest(Number mu0, InequalityType HA,
				Number mean, Number sd, int n)
			{
				return OneSampleTTest(mu0, HA, mean, sd, n, 0.05);
			}
			public static TTestResults TwoSampleTTest(InequalityType HA,
				Number mean1, Number mean2, Number sd1, Number sd2, 
				int n1, int n2, Number alpha)
			{
				Number se = Sqrt(((sd1 * sd1) / (Number)n1) + ((sd2 * sd2) / (Number)n2));
				Number df = DegreesOfFreedom(sd1, sd2, n1, n2);

				TModel model = new TModel(0, se, df);
				Number prob = -1;
				Number t = 
			}

			public static Number DegreesOfFreedom(Number s1, Number s2, int n1, int n2)
			{
				Number upperInnerA = (s1 * s1) / (Number)n1;
				Number upperInnerB = (s2 * s2) / (Number)n2;
				Number upper = (upperInnerA + upperInnerB) * (upperInnerA + upperInnerB);

				Number lowerA = 1.0 / ((Number)n1 - 1.0);
				Number lowerBInner = (s1 * s1) / (Number)n1;
				Number lowerB = lowerBInner * lowerBInner;
				Number lowerC = 1.0 / ((Number)n2 - 1.0);
				Number lowerDInner = (s2 * s2) / (Number)n2;
				Number lowerD = lowerDInner * lowerDInner;
				Number lower = (lowerA * lowerB) + (lowerC * lowerD);

				return upper / lower;
			}
		}
	}
}
