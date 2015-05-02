using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Stats;
using MathPlusLib.Extensions;

namespace MathPlusLib
{
	/// <summary>
	/// Comparison direction when comparing unequal values.
	/// Used in Statistical Tests
	/// </summary>
	public enum InequalityType
	{
		/// <summary>
		/// Value is less than expected
		/// </summary>
		LessThan = -1,
		/// <summary>
		/// Value is not equal to expected (two-sided)
		/// </summary>
		NotEqual = 0,
		/// <summary>
		/// Value is greater than expected
		/// </summary>
		GreaterThan = 1
	}

	public static partial class MathPlus
	{
		/// <summary>
		/// Statistical Functions
		/// </summary>
		public static class Stats
		{
			/// <summary>
			/// True if exceptions should be thrown if data is not
			/// suited for test or interval. Setting this to false
			/// disables the exceptions.
			/// </summary>
			public static bool ThrowInappropriateException
			{ get; set; }

			static Stats()
			{
				ThrowInappropriateException = true;
			}

			/// <summary>
			/// Returns the sum of all values in the <see cref="IEnumerable"/>.
			/// </summary>
			/// <param name="values">Values to sum</param>
			/// <returns>Sum of all items in <paramref name="values"/>.</returns>
			public static double Sum(IEnumerable<double> values)
			{
				return values.Aggregate((n, total) => total + n);
			}
			/// <summary>
			/// Returns the sum of all values in the <see cref="IEnumerable"/>.
			/// </summary>
			/// <param name="values">Values to sum</param>
			/// <returns>Sum of all items in <paramref name="values"/>.</returns>
			public static double Sum(IEnumerable<int> values)
			{
				return values.Aggregate((n, total) => total + n);
			}

			/// <summary>
			/// Returns the mean of all values in the <see cref="IEnumerable"/>.
			/// </summary>
			/// <param name="values">Values to find the mean of</param>
			/// <returns>Mean of all items in <paramref name="values"/>.</returns>
			public static double Mean(IEnumerable<double> values)
			{
				double sigma = Sum(values);
				return sigma / (double)(values.Count());
			}
			/// <summary>
			/// Returns the mean of all values in the <see cref="IEnumerable"/>.
			/// </summary>
			/// <param name="values">Values to find the mean of</param>
			/// <returns>Mean of all items in <paramref name="values"/>.</returns>
			public static double Mean(IEnumerable<int> values)
			{
				double sigma = Sum(values);
				return sigma / (double)(values.Count());
			}

			/// <summary>
			/// Returns the Standard Deviation of all values in the <see cref="IEnumerable"/>.
			/// </summary>
			/// <param name="values">Values to find the SD of</param>
			/// <returns>SD of all items in <paramref name="values"/>.</returns>
			public static double StandardDev(IEnumerable<double> values)
			{
				return StandardDev(values, Mean(values));
			}
			/// <summary>
			/// Returns the Standard Deviation of all values in the <see cref="IEnumerable"/>
			/// with a precalculated mean for speed.
			/// </summary>
			/// <param name="values">Values to find the SD of</param>
			/// <param name="mean">Precalculated mean of <paramref name="values"/>.</param>
			/// <returns>SD of all items in <paramref name="values"/>.</returns>
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

			/// <summary>
			/// Calculates the Root-Mean-Square (RMS) of data
			/// </summary>
			/// <param name="data">Data to find the RMS of</param>
			/// <returns>RMS of <paramref name="data"/>.</returns>
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
			/// <summary>
			/// Calculates the Root-Mean-Square (RMS) of data
			/// </summary>
			/// <param name="data">Data to find the RMS of</param>
			/// <returns>RMS of <paramref name="data"/>.</returns>
			public static double RootMeanSquare(IEnumerable<int> data)
			{
				double sigmaSquare = 0;
				foreach (int n in data)
				{
					sigmaSquare += (n * n);
				}

				double meanSquare = sigmaSquare / data.Count();
				return Sqrt(meanSquare);
			}

			/// <summary>
			/// Calculates what proportion of a dataset is <c>true</c>.
			/// </summary>
			/// <param name="sample">Sample data to evaluate</param>
			/// <returns>Proportion of <paramref name="sample"/> which equals <c>true</c>.</returns>
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
			/// <summary>
			/// Calculates what proportion of a data set evaluates to <c>true</c>
			/// when evaluated in a <see cref="Predicate"/>.
			/// </summary>
			/// <typeparam name="T">Type of data sampled</typeparam>
			/// <param name="sample">Sample of data to evaluate</param>
			/// <param name="evaluator">Predicate to evaluate data with</param>
			/// <returns>
			/// Proportion of <paramref name="sample"/> which evaluates to <c>true</c>
			/// when evaluated with <paramref name="evaluator"/>().
			/// </returns>
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
			/// <summary>
			/// Performs a One-Proportion Z Test
			/// </summary>
			/// <param name="p0">Null hypothesis proportion</param>
			/// <param name="HA">Direction for alternative hypothesis</param>
			/// <param name="proportion">Observed value proportion</param>
			/// <param name="n">Number of data points</param>
			/// <param name="alpha">Alpha level in test</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="proportion"/>, <paramref name="p0"/>,
			/// or <paramref name="alpha"/> is outside of range (0, 1), or if 
			/// <paramref name="HA"/> is an invalid <see cref="InequalityType"/>.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if p * n &lt; 10 or if q * n &lt; 10, where 
			/// <c>p = <paramref name="proportion"/></c> and 
			/// <c>q = 1 - <paramref name="proportion"/></c>.
			/// </exception>
			public static ZTestResults OnePropZTest(double p0, InequalityType HA, 
				double proportion, int n, double alpha = .05)
			{
				double q = 1.0 - proportion;

				if (proportion > 1.0 || proportion < 0.0)
				{
					throw new ArgumentOutOfRangeException("proportion", 
						"Proportion cannot be outside of range (0, 1).");
				}

				if (p0 > 1.0 || p0 < 0.0)
				{
					throw new ArgumentOutOfRangeException("p0",
						"Null Hypothesis cannot be outside of range (0, 1).");
				}

				if (alpha > 1.0 || alpha < 0.0)
				{
					throw new ArgumentOutOfRangeException("alpha",
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
			/// <summary>
			/// Performs a One-Proportion Z Test
			/// </summary>
			/// <param name="p0">Null hypothesis proportion</param>
			/// <param name="HA">Direction for alternative hypothesis</param>
			/// <param name="data">Data set</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="proportion"/>, <paramref name="p0"/>,
			/// or <paramref name="alpha"/> is outside of range (0, 1), or if 
			/// <paramref name="HA"/> is an invalid <see cref="InequalityType"/>.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if p * n &lt; 10 or if q * n &lt; 10, where 
			/// <c>p = <paramref name="proportion"/></c> and 
			/// <c>q = 1 - <paramref name="proportion"/></c>.
			/// </exception>
			/// <remarks>
			/// Calls <see cref="OnePropZTest(double, InequalityType, double, int, double)"/>.
			/// </remarks>
			public static ZTestResults OnePropZTest(double p0, InequalityType HA,
				IEnumerable<bool> data, double alpha = 0.05)
			{
				return OnePropZTest(p0, HA, data.Proportion(), data.Count(), alpha);
			}

			/// <summary>
			/// Performs a Two-Proportion Z-Test
			/// </summary>
			/// <param name="HA">
			/// Alternative Hypothesis direction: 
			/// <paramref name="p2"/> ? <paramref name="p1"/></param>
			/// <param name="p1">Proportion in first data set</param>
			/// <param name="p2">Proportion in second data set</param>
			/// <param name="n1">Number of data points in first set</param>
			/// <param name="n2">Number of data points in second set</param>
			/// <param name="alpha">Alpha level in test</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="p1"/>, <paramref name="p2"/>, or
			/// <paramref name="alpha"/> is outside of range (0, 1), or if 
			/// <paramref name="HA"/> is an invalid <see cref="InequalityType"/>.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if <paramref name="p1"/> or <paramref name="p2"/> does not
			/// pass the 10-successes 10-failures condition. See 
			/// <see cref="OnePropZTest(double, InequalityType, double, int, double)"/>
			/// for details.
			/// </exception>
			public static ZTestResults TwoPropZTest(InequalityType HA,
				double p1, double p2, int n1, int n2, double alpha = .05)
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

				if (alpha > 1.0 || alpha < 0.0)
				{
					throw new ArgumentOutOfRangeException("alpha",
						"Null Hypothesis cannot be outside of range (0, 1).");
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
			/// <summary>
			/// Performs a Two-Proportion Z-Test
			/// </summary>
			/// <param name="HA">Alternative Hypothesis direction: p2 ? p1</param>
			/// <param name="data1">First data set</param>
			/// <param name="data2">Second data set</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of range (0, 1), or if 
			/// <paramref name="HA"/> is an invalid <see cref="InequalityType"/>.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if <see cref="ThrowInappropriateException"/> is set to <c>true</c>
			/// and either p1 or p2 does not pass the 10-successes 10-failures condition. See 
			/// <see cref="OnePropZTest(double, InequalityType, double, int, double)"/>
			/// for details.
			/// </exception>
			/// <remarks>
			/// Calls <see cref="TwoPropZTest(InequalityType, double, double, int, int, double)"/>.
			/// </remarks>
			public static ZTestResults TwoPropZTest(InequalityType HA,
				IEnumerable<bool> data1, IEnumerable<bool> data2, 
				double alpha = 0.05)
			{
				return TwoPropZTest(HA, data1.Proportion(), data2.Proportion(),
					data1.Count(), data2.Count(), alpha);
			}
			#endregion

			#region StudentT
			/// <summary>
			/// Performs a One-Sample T-Test
			/// </summary>
			/// <param name="mu0">Expected mean in Null Hypothesis</param>
			/// <param name="HA">Direction of Alternative Hypothesis</param>
			/// <param name="mean">Observed mean in data</param>
			/// <param name="sd">Observed SD in data</param>
			/// <param name="n">Number of data points</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if <paramref name="n"/> is zero or negative, or if
			/// <paramref name="HA"/> is an invalid <see cref="InequalityType"/>.
			/// </exception>
			public static TTestResults OneSampleTTest(double mu0, InequalityType HA,
				double mean, double sd, int n, double alpha = .05)
			{
				if (alpha <= 0 || alpha >= 1)
				{
					throw new ArgumentOutOfRangeException("alpha",
						"Alpha level must be within range (0, 1).");
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
			/// <summary>
			/// Performs a One-Sample T-Test
			/// </summary>
			/// <param name="mu0">Expected mean in null hypothesis</param>
			/// <param name="HA">Direction of alternative hypothesis</param>
			/// <param name="sample">Sample data</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if <paramref name="n"/> is zero or negative, or if
			/// <paramref name="HA"/> is an invalid <see cref="InequalityType"/>.
			/// </exception>
			/// <remarks>
			/// Calls <see cref="OneSampleTTest(double, InequalityType, double, 
			/// double, int, double)"/>.
			/// </remarks>
			public static TTestResults OneSampleTTest(double mu0, InequalityType HA,
				IEnumerable<double> sample, double alpha = 0.05)
			{
				double mean = sample.Mean();

				return OneSampleTTest(mu0, HA, mean, sample.StandardDev(mean), 
					sample.Count(), alpha);
			}

			/// <summary>
			/// Performs a two-sample T-Test
			/// </summary>
			/// <param name="HA">
			/// Alternate Hypothesis Direction:
			/// <paramref name="mean2"/> ? <paramref name="mean1"/>
			/// </param>
			/// <param name="mean1">Mean of first sample</param>
			/// <param name="mean2">Mean of second sample</param>
			/// <param name="sd1">SD of first sample</param>
			/// <param name="sd2">SD of second sample</param>
			/// <param name="n1">Number of data points in first sample</param>
			/// <param name="n2">Number of data points in second sample</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if <paramref name="n1"/> or <paramref name="n2"/> is zero or
			/// negative, or if <paramref name="HA"/> is an invalid
			/// <see cref="InequalityType"/>.
			/// </exception>
			public static TTestResults TwoSampleTTest(InequalityType HA,
				double mean1, double mean2, double sd1, double sd2, 
				int n1, int n2, double alpha = .05)
			{
				if (alpha <= 0 || alpha >= 1)
				{
					throw new ArgumentOutOfRangeException("alpha",
						"Alpha level must be within range (0, 1)");
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
			/// <summary>
			/// Performs a two-sample T-Test
			/// </summary>
			/// <param name="HA">
			/// Alternate Hypothesis Direction: mean2 ? mean1
			/// </param>
			/// <param name="sample1">First sample</param>
			/// <param name="sample2">Second sample</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if <paramref name="n1"/> or <paramref name="n2"/> is zero or
			/// negative, or if <paramref name="HA"/> is an invalid
			/// <see cref="InequalityType"/>.
			/// </exception>
			/// <remarks>
			/// Calls <see cref="TwoSampleTTest(InequalityType, double, double, 
			/// double, double, int, int, double)"/>.
			/// </remarks>
			public static TTestResults TwoSampleTTest(InequalityType HA,
				IEnumerable<double> sample1, IEnumerable<double> sample2, 
				double alpha = 0.05)
			{
				double mean1 = sample1.Mean();
				double mean2 = sample2.Mean();

				return TwoSampleTTest(HA, mean1, mean2, sample1.StandardDev(mean1),
					sample2.StandardDev(mean2), sample1.Count(), sample2.Count(), alpha);
			}
			#endregion

			#region ChiSquare
			/// <summary>
			/// Performs a Chi-Square Goodness-of-Fit test. If
			/// <paramref name="expected"/> is <c>null</c>, then values will be
			/// filled in from the mean of <paramref name="counts"/>.
			/// </summary>
			/// <param name="counts">Counted values for each category</param>
			/// <param name="expected">Expected values for each category</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if any value in <paramref name="counts"/> is negative.
			/// </exception>
			/// <exception cref="ArgumentNullException">
			/// Thrown if <paramref name="counts"/> is null.
			/// </exception>
			/// <exception cref="ArgumentException">
			/// Thronw if <paramref name="counts"/> is empty.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if any value in <paramref name="expected"/> is below 5 
			/// and <see cref="ThrowInappropriateException"/> is set to <c>true</c>.
			/// </exception>
			public static ChiSquareTestResults ChiSquareGOFTest(Dictionary<string, int> counts,
				Dictionary<string, double> expected, double alpha = 0.05)
			{
				if (alpha <= 0 || alpha >= 1)
				{
					throw new ArgumentOutOfRangeException("alpha",
						"Alpha level must be inside of range (0, 1).");
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
				}

				foreach (double d in expected.Values)
				{
					if (d < 5 && ThrowInappropriateException)
					{
						throw new StatisticInappropriateException(
							"Expected values should all be above 5.");
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

				Dictionary<string, double> deviations = new Dictionary<string, double>();
				foreach (string cat in counts.Keys)
				{
					deviations.Add(cat, counts[cat] - expected[cat]);
				}

				Dictionary<string, double> components = new Dictionary<string, double>();
				foreach (string cat in counts.Keys)
				{
					double resid = deviations[cat];
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
			/// <summary>
			/// Performs a Chi-Square Goodness-of-Fit test. Truncates values in
			/// <paramref name="counts"/> to <see cref="int"/> values. If
			/// <paramref name="expected"/> is <c>null</c>, then values will be
			/// filled in from the mean of <paramref name="counts"/>.
			/// </summary>
			/// <param name="counts">Counted values for each category</param>
			/// <param name="expected">Expected values for each category</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if any value in <paramref name="counts"/> is negative.
			/// </exception>
			/// <exception cref="ArgumentNullException">
			/// Thrown if <paramref name="counts"/> is null.
			/// </exception>
			/// <exception cref="ArgumentException">
			/// Thronw if <paramref name="counts"/> is empty.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if any value in <paramref name="expected"/> is below 5 
			/// and <see cref="ThrowInappropriateException"/> is set to <c>true</c>.
			/// </exception>
			/// <remarks>
			/// Calls <see cref="ChiSquareGOFTest(Dictionary{string, double}, 
			/// Dictionary{string, int}, double)"/>
			/// </remarks>
			public static ChiSquareTestResults ChiSquareGOFTest(Dictionary<string, double> counts,
				Dictionary<string, double> expected, double alpha = 0.05)
			{
				Dictionary<string, int> ints = new Dictionary<string, int>();
				foreach (KeyValuePair<string, double> kvp in counts)
				{
					ints.Add(kvp.Key, (int)kvp.Value);
				}

				return ChiSquareGOFTest(ints, expected, alpha);
			}
			/// <summary>
			/// Performs a Chi-Square Goodness-of-Fit test
			/// </summary>
			/// <param name="counts">Counted values for implied categories</param>
			/// <param name="expected">Expected values for implied categories</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if any value in <paramref name="counts"/> is negative.
			/// </exception>
			/// <exception cref="ArgumentNullException">
			/// Thrown if <paramref name="counts"/> is null.
			/// </exception>
			/// <exception cref="ArgumentException">
			/// Thronw if <paramref name="counts"/> is empty.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if any value in <paramref name="expected"/> is below 5 
			/// and <see cref="ThrowInappropriateException"/> is set to <c>true</c>.
			/// </exception>
			/// <remarks>
			/// Calls <see cref="ChiSquareGOFTest(Dictionary{string, int}, 
			/// Dictionary{string, double}, double)"/>
			/// </remarks>
			public static ChiSquareTestResults ChiSquareGOFTest(List<int> counts,
				List<int> expected, double alpha = 0.05)
			{
				Dictionary<string, int> dCounts = new Dictionary<string, int>();
				Dictionary<string, double> dExp = new Dictionary<string, double>();
				for (int i = 1; i <= counts.Count; i++)
				{
					dCounts.Add(i.ToString(), counts[i - 1]);
					dExp.Add(i.ToString(), expected[i - 1]);
				}

				return ChiSquareGOFTest(dCounts, dExp, alpha);
			}
			/// <summary>
			/// Performs a Chi-Square Goodness-of-Fit test, calculating expected values
			/// from the mean of <paramref name="counts"/>.
			/// </summary>
			/// <param name="counts">Counted values for implied categories</param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="alpha"/> is outside of (0, 1), or
			/// if any value in <paramref name="counts"/> is negative.
			/// </exception>
			/// <exception cref="ArgumentNullException">
			/// Thrown if <paramref name="counts"/> is null.
			/// </exception>
			/// <exception cref="ArgumentException">
			/// Thronw if <paramref name="counts"/> is empty.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if any expected value is below 5 and <see cref="ThrowInappropriateException"/>
			/// is set to <c>true</c>
			/// </exception>
			/// <remarks>
			/// Calls <see cref="ChiSquareGOFTest(Dictionary{string, int}, 
			/// Dictionary{string, double}, double)"/>.
			/// </remarks>
			public static ChiSquareTestResults ChiSquareGOFTest(List<int> counts, 
				double alpha = 0.05)
			{
				Dictionary<string, int> dCounts = new Dictionary<string, int>();
				for (int i = 1; i <= counts.Count; i++)
				{
					dCounts.Add(i.ToString(), counts[i - 1]);
				}

				return ChiSquareGOFTest(dCounts, null, alpha);
			}

			/// <summary>
			/// Performs a Chi-Square test of Independence or Homogeneity
			/// </summary>
			/// <param name="observed">Observed counted values.</param>
			/// <param name="expected">
			/// Expected values. If this is <c>null</c>, this will be calculated
			/// from <paramref name="observed"/> with the methods of a Chi-Square 
			/// test of Homogeneity.
			/// </param>
			/// <param name="alpha">Alpha level</param>
			/// <returns>Test results</returns>
			/// <exception cref="ArgumentNullException">
			/// Thrown if <paramref name="observed"/> is <c>null</c>.
			/// </exception>
			/// <exception cref="StatisticInappropriateException">
			/// Thrown if any value in <paramref name="expected"/> is
			/// less than 5 and <see cref="ThrowInappropriateException"/>
			/// is set to <c>true</c>. This occurs even when values for
			/// <paramref name="expected"/> are generated in this method.
			/// </exception>
			public static ChiSquareTestResults ChiSquareTest(MathMatrix observed,
				MathMatrix expected = null, double alpha = 0.05)
			{
				if (observed == null)
				{
					throw new ArgumentNullException("observed",
						"Observed values cannot be null.");
				}
				if (expected == null)
				{
					expected = new MathMatrix(observed.Width, observed.Height);
					double fullSum = observed.SumAll();

					for (int r = 0; r < observed.Height; r++)
					{
						double rowSum = observed.SumRow(r);

						for (int c = 0; c < observed.Width; c++) // actually C#
						{
							double colSum = observed.SumColumn(c);
							double exp = (rowSum / fullSum) * colSum;

							expected[r, c] = exp;
						}
					}
				}

				expected.Foreach((d) =>
				{
					if (d < 5 && ThrowInappropriateException)
					{
						throw new StatisticInappropriateException(
							"Expected values should all be above 5.");
					}
				});

				MathMatrix deviations = new MathMatrix(observed.Width, observed.Height);
				for (int r = 0; r < observed.Height; r++)
				{
					for (int c = 0; c < observed.Width; c++) // actually C#
					{
						deviations[r, c] = observed[r, c] - expected[r, c];
					}
				}

				double sigma = 0;
				for (int r = 0; r < observed.Height; r++)
				{
					for (int c = 0; c < observed.Width; c++) // actually C#
					{
						sigma += (deviations[r, c] * deviations[r, c]) / expected[r, c];
					}
				}
				double df = (observed.Width - 1) * (observed.Height - 1);

				ChiSquareModel model = new ChiSquareModel(df);
				double pval = model.CDF(sigma);

				return new ChiSquareTestResults(sigma, pval, df, alpha);
			}
			#endregion

			#region Intervals
			/// <summary>
			/// Creates a confidence interval for a single proportion data set.
			/// </summary>
			/// <param name="proportion">Proportion of data</param>
			/// <param name="n">Number of data points</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/> or 
			/// <paramref name="proportion"/> is outside of range (0, 1).
			/// </exception>
			public static Interval OnePropZInterval(double proportion, int n, 
				double confidence = 0.95)
			{
				if (proportion <= 0.0 || proportion >= 1.0)
				{
					throw new ArgumentOutOfRangeException("proportion",
						"Proportion must be between 0 and 1, exclusively.");
				}

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
			/// <summary>
			/// Creates a confidence interval for a single proportion data set.
			/// </summary>
			/// <param name="data">Data set</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/> or 
			/// <paramref name="proportion"/> is outside of range (0, 1).
			/// </exception>
			public static Interval OnePropZInterval(IEnumerable<bool> data,
				double confidence = 0.95)
			{
				return OnePropZInterval(data.Proportion(), data.Count(), confidence);
			}

			/// <summary>
			/// Creates a confidence interval for two proportion data sets.
			/// </summary>
			/// <param name="p1">Proportion for first data set</param>
			/// <param name="p2">Proportion for second data set</param>
			/// <param name="n1">Number of data points in first data set</param>
			/// <param name="n2">Number of data points in second data set</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/>, <paramref name="p1"/>,
			/// or <paramref name="p2"/> is outside of range (0, 1).
			/// </exception>
			public static Interval TwoPropZInterval(double p1, double p2, int n1, int n2, 
				double confidence = 0.95)
			{
				if (p1 <= 0.0 || p1 >= 1.0)
				{
					throw new ArgumentOutOfRangeException("p1",
						"p1 must be between 0 and 1, exclusively.");
				}
				if (p2 <= 0.0 || p2 >= 1.0)
				{
					throw new ArgumentOutOfRangeException("p2",
						"p2 must be between 0 and 1, exclusively.");
				}

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

			/// <summary>
			/// Constructs a confidence interval for two proportion data sets.
			/// </summary>
			/// <param name="data1">First data set</param>
			/// <param name="data2">Second data set</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/>, <paramref name="p1"/>,
			/// or <paramref name="p2"/> is outside of range (0, 1).
			/// </exception>
			/// <remarks>
			/// Calls <see cref="TwoPropZInterval(double, double, int, int, double)"/>.
			/// </remarks>
			public static Interval TwoPropZInterval(IEnumerable<bool> data1,
				IEnumerable<bool> data2, double confidence = 0.95)
			{
				return TwoPropZInterval(data1.Proportion(), data2.Proportion(),
					data1.Count(), data2.Count(), confidence);
			}

			/// <summary>
			/// Constructs a confidence interval from one quantitative data set.
			/// </summary>
			/// <param name="mean">Mean of data</param>
			/// <param name="sd">SD of data</param>
			/// <param name="n">Number of data points</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/> is outside of range (0, 1).
			/// </exception>
			public static Interval OneSampleTInterval(double mean, double sd, int n, 
				double confidence = 0.95)
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

			/// <summary>
			/// Constructs a confidence interval from one quantitative data set.
			/// </summary>
			/// <param name="data">Data set</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/> is outside of range (0, 1).
			/// </exception>
			/// <remarks>Calls <see cref="OneSampleTInterval(double, double, int, double)"/>.</remarks>
			public static Interval OneSampleTInterval(IEnumerable<double> data, 
				double confidence = 0.95)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence",
						"Confidence must be between 0 and 1, exclusively.");
				}

				double mean = data.Mean();
				return OneSampleTInterval(mean, data.StandardDev(mean), data.Count(), confidence);
			}

			/// <summary>
			/// Constructs a confidence interval from two quantitative data sets.
			/// </summary>
			/// <param name="mean1">Mean of first data set</param>
			/// <param name="mean2">Mean of second data set</param>
			/// <param name="sd1">SD of first data set</param>
			/// <param name="sd2">SD of second data set</param>
			/// <param name="n1">Number of data points in first data set</param>
			/// <param name="n2">Number of data points in second data set</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/> is outside of range (0, 1).
			/// </exception>
			/// <remarks>
			/// Calls <see cref="TwoSampleTInterval(double, double, double, int, int, double)"/>.
			/// </remarks>
			public static Interval TwoSampleTInterval(double mean1, double mean2, 
				double sd1, double sd2, int n1, int n2, double confidence = 0.95)
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
			/// <summary>
			/// Constructs a confidence interval from two quantitative data sets.
			/// </summary>
			/// <param name="deltaMean">Difference of means: mu2 - mu1</param>
			/// <param name="sd1">SD of first data set</param>
			/// <param name="sd2">SD of second data set</param>
			/// <param name="n1">Number of data points in first data set</param>
			/// <param name="n2">Number of data points in second data set</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/> is outside of range (0, 1).
			/// </exception>
			public static Interval TwoSampleTInterval(double deltaMean, double sd1, double sd2, 
				int n1, int n2, double confidence = 0.95)
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

			/// <summary>
			/// Constructs a confidence interval from two quantitative data sets
			/// </summary>
			/// <param name="data1">First data set</param>
			/// <param name="data2">Second data set</param>
			/// <param name="confidence">Confidence level</param>
			/// <returns>Confidence Interval</returns>
			/// <exception cref="ArgumentOutOfRangeException">
			/// Thrown if <paramref name="confidence"/> is outside of range (0, 1).
			/// </exception>
			/// <remarks>
			/// Calls <see cref="TwoSampleTInterval(double, double, double, double, int, int, double)"/>.
			/// </remarks>
			public static Interval TwoSampleTInterval(IEnumerable<double> data1,
				IEnumerable<double> data2, double confidence = 0.95)
			{
				if (confidence <= 0.0 || confidence >= 1.0)
				{
					throw new ArgumentOutOfRangeException("confidence",
						"Confidence must be between 0 and 1, exclusively.");
				}
				
				double mean1 = data1.Mean();
				double mean2 = data2.Mean();

				return TwoSampleTInterval(mean1, mean2, data1.StandardDev(mean1), 
					data2.StandardDev(mean2), data1.Count(), data2.Count(), confidence);
			}
			#endregion

			/// <summary>
			/// Calculates degrees of freedom to use in a Two-sample T-Test or
			/// T Interval.
			/// </summary>
			/// <param name="s1">SD of first data set</param>
			/// <param name="s2">SD of second data set</param>
			/// <param name="n1">Number of data points in first data set</param>
			/// <param name="n2">Number of data points in second data set</param>
			/// <returns>Degrees of freedom to use in T model.</returns>
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
