using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib;

// Just declared here so you don't have to use #if.
namespace MathPlusLib.Portable
{ }

namespace MathPlusLib.Desktop
{
	/// <summary>
	/// Simple wrapper around the double struct, with additional functionality.
	/// Implicit conversion to and from double.
	/// </summary>
	public struct Number : IComparable, IFormattable, IConvertible, 
		IMathable<Number>, IEquatable<Number>
	{
		private double Value
		{ get; set; }

		private Number(double value) : this()
		{
			Value = value;
		}

		public Number Parse(string s)
		{
			try
			{
				return double.Parse(s);
			}
			catch (FormatException e)
			{
				throw e;
			}
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
		public int CompareTo(object obj)
		{
			if (obj is Number)
			{
				Number n = (Number)obj;
				return n.Value.CompareTo(Value);
			}

			if (obj is IComparable)
			{
				IComparable ic = obj as IComparable;
				return ic.CompareTo(Value);
			}

			throw new InvalidCastException("Cannot be compared to type " + obj.GetType().ToString());
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return Value.ToString(format, formatProvider);
		}

		#region IConvertible

		internal TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		private bool ToBoolean(IFormatProvider provider)
		{
			return Value != 0.0;
		}

		private byte ToByte(IFormatProvider provider)
		{
			return (byte)Value;
		}

		private char ToChar(IFormatProvider provider)
		{
			throw new InvalidOperationException("Cannot be converted to char.");
		}

		private DateTime ToDateTime(IFormatProvider provider)
		{
			throw new InvalidOperationException("Cannot be converted to DateTime.");
		}

		private decimal ToDecimal(IFormatProvider provider)
		{
			return (decimal)Value;
		}

		private double ToDouble(IFormatProvider provider)
		{
			return Value;
		}

		private short ToInt16(IFormatProvider provider)
		{
			return (short)Value;
		}

		private int ToInt32(IFormatProvider provider)
		{
			return (int)Value;
		}

		private long ToInt64(IFormatProvider provider)
		{
			return (long)Value;
		}

		private sbyte ToSByte(IFormatProvider provider)
		{
			return (sbyte)Value;
		}

		private float ToSingle(IFormatProvider provider)
		{
			return (float)Value;
		}

		public string ToString(IFormatProvider provider)
		{
			return Value.ToString(provider);
		}

		private object ToType(Type conversionType, IFormatProvider provider)
		{
			if (conversionType.IsSubclassOf(typeof(IComparable)))
			{
				return Convert.ChangeType(Value, conversionType, provider);
			}

			throw new InvalidOperationException("Cannot be converted to " + conversionType.ToString());
		}

		private ushort ToUInt16(IFormatProvider provider)
		{
			return (ushort)Value;
		}

		private uint ToUInt32(IFormatProvider provider)
		{
			return (uint)Value;
		}

		private ulong ToUInt64(IFormatProvider provider)
		{
			return (ulong)Value;
		}

		TypeCode IConvertible.GetTypeCode()
		{
			return GetTypeCode();
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ToBoolean(provider);
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ToByte(provider);
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ToChar(provider);
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException("Cannot cast Number to DateTime");
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ToDecimal(provider);
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ToDouble(provider);
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ToInt16(provider);
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ToInt32(provider);
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ToInt64(provider);
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ToSByte(provider);
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ToSingle(provider);
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ToType(conversionType, provider);
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ToUInt16(provider);
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ToUInt32(provider);
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ToUInt64(provider);
		}

		#endregion

		public int CompareTo(Number other)
		{
			return (int)(Value - other.Value);
		}

		public bool Equals(Number other)
		{
			return Value == other.Value;
		}

		public Number Add(Number other)
		{
			return new Number(other.Value + Value);
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
			if (this < 0)
			{
				return -this;
			}

			return this;
		}

		#endregion

		#region operator overloads
		public static bool operator==(Number a, Number b)
		{
			return a.Equals(b);
		}
		public static bool operator!=(Number a, Number b)
		{
			return !a.Equals(b);
		}
		public static Number operator+(Number a, Number b)
		{
			return a.Add(b);
		}
		public static Number operator-(Number a, Number b)
		{
			return a.Subtract(b);
		}
		public static Number operator*(Number a, Number b)
		{
			return a.Subtract(b);
		}
		public static Number operator/(Number a, Number b)
		{
			return a.Subtract(b);
		}
		/// <summary>
		/// Exponent here, not a XOR function
		/// </summary>
		/// <param name="a">Base</param>
		/// <param name="b">Exponent</param>
		/// <returns>Base ^ Exponent</returns>
		public static Number operator^(Number a, Number b)
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
