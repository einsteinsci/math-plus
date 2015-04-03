using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public struct Interval
	{
		public Number Lower
		{ get; private set; }

		public Number Upper
		{ get; private set; }

		public Number Center
		{
			get
			{
				return (Upper + Lower) / 2.0;
			}
		}

		public Number Error
		{
			get
			{
				return (Upper - Lower) / 2.0;
			}
		}

		public Interval(Number low, Number high)
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

		public static Interval FromCenter(Number center, Number error)
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
