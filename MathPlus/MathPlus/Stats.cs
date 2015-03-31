using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
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
		}
	}
}
