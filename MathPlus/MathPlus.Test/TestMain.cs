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
			MathMatrix test = MathMatrix.Parse(
				@" [[18, 15, 5,  8, 4 ],
					[10,  5, 7, 18, 10]]");
			Console.WriteLine(test.ToString());

			Console.WriteLine("Results:\n" + MathPlus.Stats.ChiSquareHomogeneityTest(.05, test).ToString());

			// End of Line
			Console.ReadKey();
		}
	}
}
