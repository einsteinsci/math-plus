using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib;

/// Created for the sole purpose of maintaining the same source code
/// between Portable and Desktop projects where possible
namespace MathPlusLib.Desktop
{ }

namespace MathPlusLib.Portable
{
	public struct Number : IMathable<Number>, IComparable, 
		IComparable<Number>, IEquatable<Number>, IFormattable
	{
		private double Value
		{ get; set; }

		private Number(double value)
			: this()
		{
			Value = value;
		}

		public override bool Equals(object obj)
		{
			if (obj is Number)
			{
				Number other = (Number)obj;

				return Value == other.Value;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		#region Interfaces

		public Number Add(Number other)
		{
			return new Number(Value + other.Value);
		}

		public Number Subtract(Number other)
		{
			return new Number(Value - other.Value);
		}

		public Number Multiply(Number other)
		{
			return new Number(Value * other.Value);
		}

		public Number Divide(Number other)
		{
			return new Number(Value / other.Value);
		}

		public Number Exponent(Number other)
		{
			return new Number(Math.Pow(Value, other.Value));
		}

		public Number AbsoluteValue()
		{
			if (Value < 0.0)
			{
				return new Number(-1 * Value);
			}
			else
			{
				return this;
			}
		}

		public int CompareTo(Number other)
		{
			return Value.CompareTo(other.Value);
		}

		int IComparable.CompareTo(object obj)
		{
			return CompareTo(obj);
		}

		private int CompareTo(object obj)
		{
			if (obj == null)
			{
				return Value.CompareTo(0);
			}

			if (obj is Number)
			{
				Number n = (Number)obj;
				return Value.CompareTo(n.Value);
			}
			else if (obj is IComparable)
			{
				IComparable ic = obj as IComparable;
				if (ic == null)
				{
					return Value.CompareTo(0);
				}

				return (-1) * ic.CompareTo(Value);
			}

			throw new InvalidOperationException(
				"Cannot compare to non-comparable type " + obj.GetType().ToString());
		}

		public bool Equals(Number other)
		{
			return Value.Equals(other.Value);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return Value.ToString(format, formatProvider);
		}

		#endregion

		#region operator overloads
		public static bool operator ==(Number a, Number b)
		{
			return a.Equals(b);
		}
		public static bool operator !=(Number a, Number b)
		{
			return !a.Equals(b);
		}
		public static Number operator +(Number a, Number b)
		{
			return a.Add(b);
		}
		public static Number operator -(Number a, Number b)
		{
			return a.Subtract(b);
		}
		public static Number operator *(Number a, Number b)
		{
			return a.Subtract(b);
		}
		public static Number operator /(Number a, Number b)
		{
			return a.Subtract(b);
		}
		/// <summary>
		/// Exponent here, not a XOR function
		/// </summary>
		/// <param name="a">Base</param>
		/// <param name="b">Exponent</param>
		/// <returns>Base ^ Exponent</returns>
		public static Number operator ^(Number a, Number b)
		{
			return a.Exponent(b);
		}

		public static implicit operator Number(double d)
		{
			return new Number(d);
		}
		public static implicit operator double(Number n)
		{
			return n.Value;
		}
		#endregion
	}
}
