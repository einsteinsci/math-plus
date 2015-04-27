using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="MathPlus.Stats"/>
	/// </summary>
	public static class StatExtensions
	{
		/// <summary>
		/// Calculates the sum of all the values in a collection
		/// </summary>
		/// <param name="values">collection to sum</param>
		/// <returns>Sum of all numbers in values</returns>
		public static double Sum(this IEnumerable<double> values)
		{
			return MathPlus.Stats.Sum(values);
		}
		/// <summary>
		/// Calculates the sum of all the values in a collection
		/// </summary>
		/// <param name="values">collection to sum</param>
		/// <returns>Sum of all the numbers in values</returns>
		public static double Sum(this IEnumerable<int> values)
		{
			return MathPlus.Stats.Sum(values);
		}
		/// <summary>
		/// Calculates the mean of all the values in a collection
		/// </summary>
		/// <param name="values">collection to find the mean of</param>
		/// <returns>The mean of all the numbers in values</returns>
		public static double Mean(this IEnumerable<double> values)
		{
			return MathPlus.Stats.Mean(values);
		}
		/// <summary>
		/// Calculates the mean of all the values in a collection
		/// </summary>
		/// <param name="values">collection to find the mean of</param>
		/// <returns>The mean of all the numbers in values</returns>
		public static double Mean(this IEnumerable<int> values)
		{
			return MathPlus.Stats.Mean(values);
		}
		/// <summary>
		/// Calculates the standard deviation of all the values in a collection
		/// </summary>
		/// <param name="values">collection to find the SD of</param>
		/// <returns>The standard deviation S(x) of all the numbers in values</returns>
		public static double StandardDev(this IEnumerable<double> values)
		{
			return MathPlus.Stats.StandardDev(values);
		}
		/// <summary>
		/// Calculates the standard deviation of all the values in a collection
		/// </summary>
		/// <param name="values">collection to find the SD of</param>
		/// <returns>The standard deviation S(x) of all the numbers in values</returns>
		public static double StandardDev(this IEnumerable<double> values, double mean)
		{
			return MathPlus.Stats.StandardDev(values, mean);
		}
		/// <summary>
		/// Calculates the Root-Mean-Square (RMS) of all the values in a collection
		/// </summary>
		/// <param name="data">collection to find the RMS of</param>
		/// <returns>The RMS of all the numbers in data/returns>
		public static double RootMeanSquare(this IEnumerable<double> data)
		{
			return MathPlus.Stats.RootMeanSquare(data);
		}
		/// <summary>
		/// Calculates the Root-Mean-Square (RMS) of all the values in a collection
		/// </summary>
		/// <param name="data">collection to find the RMS of</param>
		/// <returns>The RMS of all the numbers in data/returns>
		public static double RootMeanSquare(this IEnumerable<int> data)
		{
			return MathPlus.Stats.RootMeanSquare(data);
		}

		/// <summary>
		/// Returns the proportion of values that are true, ranging from 0 to 1
		/// </summary>
		/// <param name="sample">values to count</param>
		/// <returns>Proportion of values that are true</returns>
		public static double Proportion(this IEnumerable<bool> sample)
		{
			return MathPlus.Stats.Proportion(sample);
		}
		/// <summary>
		/// Returns the proportion of values that return true in the evaluator,
		/// ranging from 0 to 1
		/// </summary>
		/// <typeparam name="T">Type within the data</typeparam>
		/// <param name="sample">values to count</param>
		/// <param name="evaluator">
		/// predicate to determine if the value should contribute to
		/// the proportion, or against it
		/// </param>
		/// <returns>Proportion of values that returned true when put through the evaluator</returns>
		public static double Proportion<T>(this IEnumerable<T> sample, Predicate<T> evaluator)
		{
			return MathPlus.Stats.Proportion(sample, evaluator);
		}
	}
}
