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
			Function2D func = (x) => Math.Exp(-x / 5) * (2 + Math.Sin(2 * x));

			double area = MathPlus.Calculus.IntegrateSimpson(func, 0, 10, 1000);

			//Console.WriteLine("d/dx|2 x^2 = " + deriv.ToString());
			Console.WriteLine("Area: " + area.ToString());

			// End of Line
			Console.ReadKey();
		}
	}
}
