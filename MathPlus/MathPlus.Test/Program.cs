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
			Function2D squared = (x) => x * x;
			double deriv = MathPlus.Numerics.Round(MathPlus.Calculus.Derivative(squared, 2), 5);
			double area = MathPlus.Calculus.Integrate(squared, -1.0, 1.0, 2, IntegrationType.Simpson);

			//Console.WriteLine("d/dx|2 x^2 = " + deriv.ToString());
			Console.WriteLine("Area: " + area.ToString());

			// End of Line
			Console.ReadKey();
		}
	}
}
