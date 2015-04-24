using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Extensions
{
	public static class StatExtensions
	{
		public static double Sum(this IEnumerable<double> values)
		{
			return MathPlus.Stats.Sum(values);
		}
		public static double Sum(this IEnumerable<int> values)
		{
			return MathPlus.Stats.Sum(values);
		}
		public static double Mean(this IEnumerable<double> values)
		{
			return MathPlus.Stats.Mean(values);
		}
		public static double Mean(this IEnumerable<int> values)
		{
			return MathPlus.Stats.Mean(values);
		}
		public static double StandardDev(this IEnumerable<double> values)
		{
			return MathPlus.Stats.StandardDev(values);
		}
		public static double StandardDev(this IEnumerable<double> values, double mean)
		{
			return MathPlus.Stats.StandardDev(values, mean);
		}
		public static double RootMeanSquare(this IEnumerable<double> data)
		{
			return MathPlus.Stats.RootMeanSquare(data);
		}

		public static double Proportion(this IEnumerable<bool> sample)
		{
			return MathPlus.Stats.Proportion(sample);
		}
		public static double Proportion<T>(this IEnumerable<T> sample, Predicate<T> evaluator)
		{
			return MathPlus.Stats.Proportion(sample, evaluator);
		}
	}
}
