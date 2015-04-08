﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Stats;

namespace MathPlusLib
{
	public enum InequalityType
	{
		LessThan = -1,
		NotEqual = 0,
		GreaterThan = 1
	}

	public static partial class MathPlus
	{
		public static class Stats
		{
			public static List<double> MakeNumbers(IEnumerable<IComparable> input)
			{
				List<double> result = new List<double>();
				foreach (IComparable n in input)
				{
					try
					{
						result.Add((double)n);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException("Type cannot be converted to double: "
							+ n.GetType().ToString());
					}
				}

				return result;
			}

			public static double Sum(IEnumerable<double> values)
			{
				return values.Aggregate((n, total) => total + n);
			}

			public static double Mean(IEnumerable<double> values)
			{
				double sigma = Sum(values);

				return sigma / (double)(values.Count());
			}

			public static double StandardDev(IEnumerable<double> values)
			{
				return StandardDev(values, Mean(values));
			}
			public static double StandardDev(IEnumerable<double> values, double mean)
			{
				Func<double, double> deviation = (xi) => (xi - mean) * (xi - mean);

				double sigmaDev = 0;
				foreach (double i in values)
				{
					sigmaDev += deviation(i);
				}

				int n = values.Count();

				return sigmaDev / (double)(n - 1);
			}

			public static double Proportion(IEnumerable<bool> sample)
			{
				int on = 0;
				foreach (bool b in sample)
				{
					if (b) on++;
				}
				int total = sample.Count();

				return (double)on / (double)total;
			}

			public static ZTestResults OnePropZTest(double p0, InequalityType HA, 
				double proportion, int n, double alpha)
			{
				double q = 1.0 - proportion;

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

				double se = Sqrt((proportion * q) / (double)n);

				NormalModel model = new NormalModel(p0, se);

				double prob = -1;
				double z = Constrain(model.ZScore(proportion), -98.9, 98.9);

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

				return new ZTestResults(proportion, prob, alpha, se);
			}
			public static ZTestResults OnePropZTest(double p0, InequalityType HA, 
				double proportion, int n)
			{
				return OnePropZTest(p0, HA, proportion, n, 0.05);
			}
			public static ZTestResults TwoPropZTest(InequalityType HA,
				double p1, double p2, int n1, int n2, double alpha)
			{
				double q1 = 1.0 - p1;
				double q2 = 1.0 - p2;

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

				double pPooled = (double)(total1 + total2) / (double)(n1 + n2);
				double qPooled = 1.0 - pPooled;
				double pDelta = p2 - p1;

				double sePooled = Sqrt(((pPooled * qPooled) / (double)n1) + 
					((pPooled * qPooled) / (double)n2));

				NormalModel model = new NormalModel(0.0, sePooled);
				double z = Constrain(model.ZScore(pDelta), -98.9, 98.9);

				double prob = -1;

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

				return new ZTestResults(p2 - p1, prob, alpha, sePooled);
			}
			public static ZTestResults TwoPropZTest(InequalityType HA,
				double p1, double p2, int n1, int n2)
			{
				return TwoPropZTest(HA, p1, p2, n1, n2, 0.05);
			}

			public static TTestResults OneSampleTTest(double mu0, InequalityType HA,
				double mean, double sd, int n, double alpha)
			{
				double se = sd / Sqrt(n);
				double df = n - 1;

				TModel model = new TModel(mu0, se, df);
				double prob = -1;
				double t = Constrain(model.TScore(mean), -98.9, 98.9);

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

				return new TTestResults(mean, prob, alpha, se, df);
			}
			public static TTestResults OneSampleTTest(double mu0, InequalityType HA,
				double mean, double sd, int n)
			{
				return OneSampleTTest(mu0, HA, mean, sd, n, 0.05);
			}
			public static TTestResults TwoSampleTTest(InequalityType HA,
				double mean1, double mean2, double sd1, double sd2, 
				int n1, int n2, double alpha)
			{
				double se = Sqrt(((sd1 * sd1) / (double)n1) + ((sd2 * sd2) / (double)n2));
				double df = DegreesOfFreedom(sd1, sd2, n1, n2);

				TModel model = new TModel(0, se, df);
				double prob = -1;
				double t = Constrain(model.TScore(mean2 - mean1), -98.9, 98.9);

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

				return new TTestResults(mean2 - mean1, prob, alpha, se, df);
			}
			public static TTestResults TwoSampleTTest(InequalityType HA,
				double mean1, double mean2, double sd1, double sd2, 
				int n1, int n2)
			{
				return TwoSampleTTest(HA, mean1, mean2, sd1, sd2, n1, n2, 0.05);
			}

			public static Interval OnePropZInterval(double proportion, int n, double confidence)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence", 
						"Confidence must be between 0 and 1, exclusively.");
				}

				double q = 1.0 - proportion;
				
				double se = Sqrt((proportion * q) / (double)n);
				double zCrit = NormalModel.Inverse(1.0 - ((1.0 - confidence) / 2.0));

				return Interval.FromCenter(proportion, se * zCrit);
			}
			public static Interval TwoPropZInterval(double p1, double p2, int n1, int n2, double confidence)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence",
						"Confidence must be between 0 and 1, exclusively.");
				}

				double q1 = 1.0 - p1;
				double q2 = 1.0 - p2;

				double se = Sqrt(((p1 * q1) / (double)n1) + ((p2 * q2) / (double)n2));
				double zCrit = NormalModel.Inverse(1.0 - ((1.0 - confidence) / 2.0));

				return Interval.FromCenter(p2 - p1, se * zCrit);
			}

			public static Interval OneSampleTInterval(double mean, double sd, int n, double confidence)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence",
						"Confidence must be between 0 and 1, exclusively.");
				}

				double se = sd / Sqrt(n);
				double tCrit = TModel.Inverse(1.0 - ((1.0 - confidence) / 2.0), n - 1);

				return Interval.FromCenter(mean, se * tCrit);
			}
			public static Interval TwoSampleTInterval(double mean1, double mean2, 
				double sd1, double sd2, int n1, int n2, double confidence)
			{
				try
				{
					return TwoSampleTInterval(mean2 - mean1, sd1, sd2, n1, n2, confidence);
				}
				catch (ArgumentOutOfRangeException e)
				{
					throw e; // rethrow
				}
			}
			public static Interval TwoSampleTInterval(double deltaMean, double sd1, double sd2, 
				int n1, int n2, double confidence)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence",
						"Confidence must be between 0 and 1, exclusively.");
				}

				double se = Sqrt((sd1 * sd1 / (double)n1) + (sd2 * sd2 / (double)n2));
				double tCrit = TModel.Inverse(1.0 - ((1.0 - confidence) / 2.0),
					DegreesOfFreedom(sd1, sd2, n1, n2));

				return Interval.FromCenter(deltaMean, se * tCrit);
			}

			public static double DegreesOfFreedom(double s1, double s2, int n1, int n2)
			{
				double upperInnerA = (s1 * s1) / (double)n1;
				double upperInnerB = (s2 * s2) / (double)n2;
				double upper = (upperInnerA + upperInnerB) * (upperInnerA + upperInnerB);

				double lowerA = 1.0 / ((double)n1 - 1.0);
				double lowerBInner = (s1 * s1) / (double)n1;
				double lowerB = lowerBInner * lowerBInner;
				double lowerC = 1.0 / ((double)n2 - 1.0);
				double lowerDInner = (s2 * s2) / (double)n2;
				double lowerD = lowerDInner * lowerDInner;
				double lower = (lowerA * lowerB) + (lowerC * lowerD);

				return upper / lower;
			}
		}
	}
}
