using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib;
using MathPlusLib.Stats;

namespace MathPlusLib.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			NormalModel root = NormalModel.Root;

			//Console.WriteLine("67% ?= " + root.ScaledCDF(-2, 2).ToString());
			Console.WriteLine("95% confidence: " + NormalModel.InverseCDF(.975).ToString());

			//List<double> data = DataFactory.MakeData<double>(
			//	12, 13, 14, 14, 15, 15, 15, 16, 16, 16, 16, 16,
			//	17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 20, 21);
			//double mean = MathPlus.Stats.Mean(data);
			//double sd = MathPlus.Stats.StandardDev(data, mean);
			//
			//TTestResults results = MathPlus.Stats.OneSampleTTest(15.0, 
			//	InequalityType.GreaterThan, mean, sd, data.Count);
			//
			//Console.WriteLine(results.ToString());

			// End of Line
			Console.ReadKey();
		}
	}
}
