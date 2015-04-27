using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// Numeric interval of values. Primarily used for confidence intervals.
	/// </summary>
	public struct Interval
	{
		/// <summary>
		/// Lower bound of the values
		/// </summary>
		public double Lower
		{ get; private set; }

		/// <summary>
		/// Upper bound of the values
		/// </summary>
		public double Upper
		{ get; private set; }

		/// <summary>
		/// Calculated center of the interval
		/// </summary>
		public double Center
		{
			get
			{
				return (Upper + Lower) / 2.0;
			}
		}

		/// <summary>
		/// Distance from the center to each boundary. Half the size of the interval.
		/// </summary>
		public double Error
		{
			get
			{
				return (Upper - Lower) / 2.0;
			}
		}

		/// <summary>
		/// Instantiates a new Interval object.
		/// </summary>
		/// <param name="low">Lower bound of the interval</param>
		/// <param name="high">Upper bound of the interval</param>
		public Interval(double low, double high)
			: this()
		{
			if (low > high)
			{
				throw new InvalidOperationException("Cannot create interval with " +
					"upper value less than lower value.");
			}

			Lower = low;
			Upper = high;
		}

		/// <summary>
		/// Instantiates a new Interval object from a given center and standard error.
		/// </summary>
		/// <param name="center">Center of the interval</param>
		/// <param name="error">Standard Error of the interval</param>
		/// <returns></returns>
		public static Interval FromCenter(double center, double error)
		{
			return new Interval(center - error, center + error);
		}

		/// <summary>
		/// Determines if an interval includes values from another
		/// </summary>
		/// <param name="other">Other interval to test</param>
		/// <returns>True if the intervals overlap, false otherwise</returns>
		public bool Intersects(Interval other)
		{
			return other.Lower < this.Upper || this.Lower < other.Upper;
		}

		/// <summary>
		/// Determines if an interval starts and ends at the same points as another.
		/// </summary>
		/// <param name="obj">Object to compare to</param>
		/// <returns>
		/// True if obj is an Interval and starts and ends at the same
		/// points as this, false otherwise.
		/// </returns>
		public override bool Equals(object obj)
		{
			try
			{
				Interval other = (Interval)obj;
				return other.Lower == this.Lower && other.Upper == this.Upper;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Converts Interval to int hash code. Truncates Lower to fit.
		/// </summary>
		/// <returns>Hash code of Interval</returns>
		public override int GetHashCode()
		{
			return (Lower.GetHashCode() << 16) + Upper.GetHashCode();
		}

		/// <summary>
		/// Serializes Interval to the format (lower, upper).
		/// </summary>
		/// <returns>String in exclusive interval format.</returns>
		public override string ToString()
		{
			return "(" + Lower.ToString() + ", " + Upper.ToString() + ")";
		}
	}
}
