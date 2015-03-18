using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlus.Desktop
{
	/// <summary>
	/// To help split between mobile and desktop versions of double
	/// </summary>
	public struct Number : IComparable, IFormattable, IConvertible, IMathable<Number>, IEquatable<Number>
	{
		private double Value
		{ get; set; }

		private Number(double value)
		{
			Value = value;
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
		// yep, not xor anymore
		public static Number operator^(Number a, Number b)
		{
			return a.Exponent(b);
		}

		public static explicit operator Number(double d)
		{
			return new Number(d);
		}
		public static implicit operator Number(double d)
		{
			return new Number(d);
		}
		public static explicit operator double(Number n)
		{
			return n.Value;
		}
		public static implicit operator double(Number n)
		{
			return n.Value;
		}
		#endregion
	}
}
