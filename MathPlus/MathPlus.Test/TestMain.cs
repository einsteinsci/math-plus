using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib;
using MathPlusLib.Stats;

namespace MathPlusLib.Test
{
	class TestMain
	{
		static void Main(string[] args)
		{
			MathMatrix test = new MathMatrix(new double[][] {
				new double[] {10, 9, 6, 8},
				new double[] {8, 7, 6, 9},
				new double[] {10, 9, 6, 7}
			});
			Console.WriteLine(test.ToString());

			Console.WriteLine("Results:\n" + MathPlus.Stats.ChiSquareHomogeneityTest(.05, test).ToString());

			// End of Line
			Console.ReadKey();
		}
	}
}
