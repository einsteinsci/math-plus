using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public struct Interval
	{
		public double Lower
		{ get; private set; }

		public double Upper
		{ get; private set; }

		public double Center
		{
			get
			{
				return (Upper + Lower) / 2.0;
			}
		}

		public double Error
		{
			get
			{
				return (Upper - Lower) / 2.0;
			}
		}

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

		public static Interval FromCenter(double center, double error)
		{
			return new Interval(center - error, center + error);
		}

		public bool Intersects(Interval other)
		{
			return other.Lower < this.Upper || this.Lower < other.Upper;
		}

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

		public override int GetHashCode()
		{
			return (Lower.GetHashCode() << 16) + Upper.GetHashCode();
		}

		public override string ToString()
		{
			return "(" + Lower.ToString() + ", " + Upper.ToString() + ")";
		}
	}
}
