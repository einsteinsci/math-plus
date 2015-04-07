using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public static class DataFactory
	{
		public static List<T> MakeData<T>(params T[] elements)
		{
			return new List<T>(elements);
		}
	}
}
