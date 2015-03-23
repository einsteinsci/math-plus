using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlus.Desktop;

namespace MathPlus.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("sin 30 = " + ExtraMath.Algebra.Sin(30));

			Console.ReadKey();
		}
	}
}
