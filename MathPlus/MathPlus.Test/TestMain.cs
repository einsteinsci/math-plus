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
			MathMatrix test = new MathMatrix(4, 4);
			for (int r = 0; r < 4; r++)
			{
				for (int c = 0; c < 4; c++) // actually c#
				{
					test[r, c] = r * 4 + c;
				}
			}

			Console.WriteLine(test.ToString());

			// End of Line
			Console.ReadKey();
		}
	}
}
