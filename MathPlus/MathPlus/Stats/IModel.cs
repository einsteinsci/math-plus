using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// Base interface for all statistical models in this library
	/// </summary>
	public interface IModel
	{
		/// <summary>
		/// Scaled Cumulative Density Function (CDF) parameterized by
		/// start and end points
		/// </summary>
		/// <param name="bottom">Lower bound of CDF</param>
		/// <param name="top">Upper bound of CDF</param>
		/// <returns>The proportion of the model highlighted between the bounds</returns>
		double ScaledCDF(double bottom, double top);
	}
}
