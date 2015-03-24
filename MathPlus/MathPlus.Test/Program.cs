using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib.Desktop;

namespace MathPlusLib.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("sin 30 = " + MathPlus.Trig.Sin(30));

			Console.ReadKey();
		}
	}
}
