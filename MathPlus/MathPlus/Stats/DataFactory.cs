using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// A few functions for setting up hardcoded (testing) data 
	/// convenient to the programmer.
	/// </summary>
	public static class DataFactory
	{
		/// <summary>
		/// Makes a list out of the given elements
		/// </summary>
		/// <typeparam name="T">List type</typeparam>
		/// <param name="elements">Each element of list</param>
		/// <returns>A new list from the data provided</returns>
		public static List<T> MakeData<T>(params T[] elements)
		{
			return new List<T>(elements);
		}

		/// <summary>
		/// Makes a matrix out of the given element
		/// </summary>
		/// <typeparam name="T">Matrix type</typeparam>
		/// <param name="rows">
		/// Array of arrays to become the data. Each subarray is a row. 
		/// All subarrays must be the same length.
		/// </param>
		/// <returns>A new matrix filled with the data provided</returns>
		public static Matrix<T> MakeMatrix<T>(params T[][] rows)
		{
			return new Matrix<T>(rows);
		}
	}
}
