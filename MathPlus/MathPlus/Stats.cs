using System;
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
			public static bool ThrowInappropriateException
			{ get; set; }

			static Stats()
			{
				ThrowInappropriateException = true;
			}

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
			public static double Sum(IEnumerable<int> values)
			{
				return values.Aggregate((n, total) => total + n);
			}

			public static double Mean(IEnumerable<double> values)
			{
				double sigma = Sum(values);
				return sigma / (double)(values.Count());
			}
			public static double Mean(IEnumerable<int> values)
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
				double res = 0;
				if (values.Count() > 1)
				{
					double sum = 0;
					foreach (double d in values)
					{
						sum += (d - mean) * (d - mean);
					}
					res = Sqrt(sum / (values.Count() - 1));
				}

				return res;
			}

			public static double RootMeanSquare(IEnumerable<double> data)
			{
				double sigmaSquare = 0;
				foreach (double d in data)
				{
					sigmaSquare += (d * d);
				}

				double meanSquare = sigmaSquare / data.Count();
				return Sqrt(meanSquare);
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
			public static double Proportion<T>(IEnumerable<T> sample, Predicate<T> evaluator)
			{
				List<bool> results = new List<bool>();
				foreach (T t in sample)
				{
					results.Add(evaluator(t));
				}

				return Proportion(results);
			}

			#region Normal
			public static ZTestResults OnePropZTest(double p0, InequalityType HA, 
				double proportion, int n, double alpha)
			{
				double q = 1.0 - proportion;

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
					if (ThrowInappropriateException)
					{
						throw new StatisticInappropriateException(
							"Did not pass 10-successes 10-failures condition.");
					}
				}

				double se = Sqrt((proportion * q) / (double)n);

				NormalModel model = new NormalModel(p0, se);

				double prob = -1;
				double z = Constrain(model.ZScore(proportion), -98.9, 98.9);

				if (HA == InequalityType.LessThan)
				{
					prob = NormalModel.CDF(-99.0, z);
				}
				else if (HA == InequalityType.GreaterThan)
				{
					prob = NormalModel.CDF(z, 99.0);
				}
				else if (HA == InequalityType.NotEqual)
				{
					prob = NormalModel.CDF(Abs(z), 99.0) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid InequalityType: " +
						HA.ToString());
				}

				return new ZTestResults(proportion, prob, p0, HA, alpha, se);
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
				if (ThrowInappropriateException)
				{
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
				}

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
					prob = NormalModel.CDF(-99.0, z);
				}
				else if (HA == InequalityType.GreaterThan)
				{
					prob = NormalModel.CDF(z, 99.0);
				}
				else if (HA == InequalityType.NotEqual)
				{
					prob = NormalModel.CDF(Abs(z), 99.0) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid InequalityType: " +
						HA.ToString());
				}

				return new ZTestResults(pDelta, prob, 0, HA, alpha, sePooled);
			}
			public static ZTestResults TwoPropZTest(InequalityType HA,
				double p1, double p2, int n1, int n2)
			{
				return TwoPropZTest(HA, p1, p2, n1, n2, 0.05);
			}
			#endregion

			#region StudentT
			public static TTestResults OneSampleTTest(double mu0, InequalityType HA,
				double mean, double sd, int n, double alpha)
			{
				if (alpha <= 0 || alpha > 1)
				{
					throw new ArgumentOutOfRangeException("alpha",
						"Alpha level must be within range (0, 1].");
				}

				if (n <= 0)
				{
					throw new ArgumentOutOfRangeException("n", "n must be positive.");
				}

				double se = sd / Sqrt(n);
				double df = n - 1;

				TModel model = new TModel(mu0, se, df);
				double prob = -1;
				double t = Constrain(model.TScore(mean), -98.9, 98.9);

				if (HA == InequalityType.LessThan)
				{
					prob = TModel.CDF(-99.0, t, df);
				}
				else if (HA == InequalityType.GreaterThan)
				{
					prob = TModel.CDF(t, 99.0, df);
				}
				else if (HA == InequalityType.NotEqual)
				{
					prob = TModel.CDF(Abs(t), 99.0, df) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid InequalityType: " +
						HA.ToString());
				}

				return new TTestResults(mean, prob, mu0, HA, alpha, se, df);
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
				if (alpha <= 0 || alpha > 1)
				{
					throw new ArgumentOutOfRangeException("alpha",
						"Alpha level must be within range (0, 1]");
				}

				if (n1 <= 0)
				{
					throw new ArgumentOutOfRangeException("n1",
						"n must be positive.");
				}
				if (n2 <= 0)
				{
					throw new ArgumentOutOfRangeException("n2",
						"n must be positive.");
				}

				double se = Sqrt(((sd1 * sd1) / (double)n1) + ((sd2 * sd2) / (double)n2));
				double df = TDegreesOfFreedom(sd1, sd2, n1, n2);

				TModel model = new TModel(0, se, df);
				double prob = -1;
				double t = Constrain(model.TScore(mean2 - mean1), -98.9, 98.9);

				if (HA == InequalityType.LessThan)
				{
					prob = TModel.CDF(-99.0, t, df);
				}
				else if (HA == InequalityType.GreaterThan)
				{
					prob = TModel.CDF(t, 99.0, df);
				}
				else if (HA == InequalityType.NotEqual)
				{
					prob = TModel.CDF(Abs(t), 99.0, df) * 2.0;
				}

				if (prob == -1)
				{
					throw new ArgumentOutOfRangeException(
						"Alternate hypothesis was not a valid InequalityType: " +
						HA.ToString());
				}

				return new TTestResults(mean2 - mean1, prob, 0, HA, alpha, se, df);
			}
			public static TTestResults TwoSampleTTest(InequalityType HA,
				double mean1, double mean2, double sd1, double sd2, 
				int n1, int n2)
			{
				return TwoSampleTTest(HA, mean1, mean2, sd1, sd2, n1, n2, 0.05);
			}
			#endregion

			#region ChiSquare
			public static ChiSquareTestResults ChiSquareGOFTest(double alpha,
				Dictionary<string, int> counts, Dictionary<string, double> expected)
			{
				if (alpha <= 0 || alpha > 1)
				{
					throw new ArgumentOutOfRangeException("alpha",
						"Alpha level must be inside of range (0, 1].");
				}

				if (counts == null)
				{
					throw new ArgumentNullException("counts", "Counts cannot be null.");
				}

				if (counts.Count == 0)
				{
					throw new ArgumentException("counts", "Counts cannot be empty.");
				}

				foreach (int n in counts.Values)
				{
					if (n < 0)
					{
						throw new ArgumentOutOfRangeException("counts",
							"No count can be below zero.");
					}

					if (n < 5 && ThrowInappropriateException)
					{
						throw new StatisticInappropriateException(
							"Counts should all be above 5.");
					}
				}

				if (expected == null)
				{
					expected = new Dictionary<string, double>();
					double mean = MathPlus.Stats.Mean(counts.Values.ToList());
					foreach (string category in counts.Keys)
					{
						expected.Add(category, mean);
					}
				}

				Dictionary<string, double> resids = new Dictionary<string, double>();
				foreach (string cat in counts.Keys)
				{
					resids.Add(cat, counts[cat] - expected[cat]);
				}

				Dictionary<string, double> components = new Dictionary<string, double>();
				foreach (string cat in counts.Keys)
				{
					double resid = resids[cat];
					components.Add(cat, (resid * resid) / expected[cat]);
				}

				double chiSquareValue = 0;
				foreach (double val in components.Values)
				{
					chiSquareValue += val;
				}

				ChiSquareModel model = new ChiSquareModel(counts.Count - 1);
				double prob = model.CDF(chiSquareValue);

				return new ChiSquareTestResults(chiSquareValue, prob, counts.Count - 1, alpha);
			}
			public static ChiSquareTestResults ChiSquareGOFTest(double alpha,
				Dictionary<string, double> counts, Dictionary<string, double> expected)
			{
				Dictionary<string, int> ints = new Dictionary<string, int>();
				foreach (KeyValuePair<string, double> kvp in counts)
				{
					ints.Add(kvp.Key, (int)kvp.Value);
				}

				return ChiSquareGOFTest(alpha, ints, expected);
			}
			public static ChiSquareTestResults ChiSquareGOFTest(double alpha,
				List<int> counts, List<int> expected)
			{
				Dictionary<string, int> dCounts = new Dictionary<string, int>();
				Dictionary<string, double> dExp = new Dictionary<string, double>();
				for (int i = 1; i <= counts.Count; i++)
				{
					dCounts.Add(i.ToString(), counts[i - 1]);
					dExp.Add(i.ToString(), expected[i - 1]);
				}

				return ChiSquareGOFTest(alpha, dCounts, dExp);
			}
			public static ChiSquareTestResults ChiSquareGOFTest(double alpha, List<int> counts)
			{
				Dictionary<string, int> dCounts = new Dictionary<string, int>();
				for (int i = 1; i <= counts.Count; i++)
				{
					dCounts.Add(i.ToString(), counts[i - 1]);
				}

				return ChiSquareGOFTest(alpha, dCounts, null);
			}

			public static ChiSquareTestResults ChiSquareHomogeneityTest(double alpha, 
				MathMatrix counts)
			{
				MathMatrix expected = new MathMatrix(counts.Width, counts.Height);
				double fullSum = counts.SumAll();

				for (int r = 0; r < counts.Height; r++)
				{
					double rowSum = counts.SumRow(r);
					
					for (int c = 0; c < counts.Width; c++) // actually C#
					{
						double colSum = counts.SumColumn(c);
						double exp = (rowSum / fullSum) * colSum;
						if (ThrowInappropriateException && exp < 5.0)
						{
							throw new StatisticInappropriateException(
								"Expected value should be above 5 for each cell.");
						}

						expected[r, c] = exp;
					}
				}

				MathMatrix residuals = new MathMatrix(counts.Width, counts.Height);
				for (int r = 0; r < counts.Height; r++)
				{
					for (int c = 0; c < counts.Width; c++) // actually C#
					{
						residuals[r, c] = counts[r, c] - expected[r, c];
					}
				}

				double sigma = 0;
				for (int r = 0; r < counts.Height; r++)
				{
					for (int c = 0; c < counts.Width; c++) // actually C#
					{
						sigma += (residuals[r, c] * residuals[r, c]) / expected[r, c];
					}
				}
				double df = (counts.Width - 1) * (counts.Height - 1);

				ChiSquareModel model = new ChiSquareModel(df);
				double pval = model.CDF(sigma);

				return new ChiSquareTestResults(sigma, pval, df, alpha);
			}
			#endregion

			#region Intervals
			public static Interval OnePropZInterval(double proportion, int n, double confidence)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence", 
						"Confidence must be between 0 and 1, exclusively.");
				}

				double q = 1.0 - proportion;
				
				double se = Sqrt((proportion * q) / (double)n);
				double zCrit = NormalModel.InverseCDF(1.0 - ((1.0 - confidence) / 2.0));

				return Interval.FromCenter(proportion, se * zCrit);
			}
			public static Interval TwoPropZInterval(double p1, double p2, int n1, int n2, 
				double confidence)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence",
						"Confidence must be between 0 and 1, exclusively.");
				}

				double q1 = 1.0 - p1;
				double q2 = 1.0 - p2;

				double se = Sqrt(((p1 * q1) / (double)n1) + ((p2 * q2) / (double)n2));
				double zCrit = NormalModel.InverseCDF(1.0 - ((1.0 - confidence) / 2.0));

				return Interval.FromCenter(p2 - p1, se * zCrit);
			}

			public static Interval OneSampleTInterval(double mean, double sd, int n, 
				double confidence)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence",
						"Confidence must be between 0 and 1, exclusively.");
				}

				double se = sd / Sqrt(n);
				double tCrit = TModel.InverseCDF(1.0 - ((1.0 - confidence) / 2.0), n - 1);

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
				double tCrit = TModel.InverseCDF(1.0 - ((1.0 - confidence) / 2.0),
					TDegreesOfFreedom(sd1, sd2, n1, n2));

				return Interval.FromCenter(deltaMean, se * tCrit);
			}
			#endregion

			public static double TDegreesOfFreedom(double s1, double s2, int n1, int n2)
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
