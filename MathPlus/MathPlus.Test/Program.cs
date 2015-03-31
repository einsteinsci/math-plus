using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib;
using MathPlusLib.Desktop;

namespace MathPlusLib.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Function2D battery2 = (x) => 1.0 / x;
			Number two = MathPlus.Calculus.Integrate(battery2, 1, 
				MathPlus.E * MathPlus.E, 1000, IntegrationType.Trapezoidal);
			Console.WriteLine("val = " + two.ToString());
			two = MathPlus.Round(two);
			Console.WriteLine("Battery #" + two.ToString());

			// End of Line
			Console.ReadKey();
		}
	}
}
