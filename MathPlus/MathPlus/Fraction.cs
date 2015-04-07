using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public struct Fraction : IComparable, IMathable<Fraction>, IMathable<double>, 
		IEquatable<Fraction>, IEquatable<double>
	{
		public int Numerator
		{ get; private set; }

		public int Denominator
		{ get; private set; }

		public double FloatingPoint
		{
			get
			{
				return ToDouble();
			}
		}

		public Fraction Inverse
		{
			get
			{
				return new Fraction(Denominator, Numerator);
			}
		}

		public static int MaxDenominator
		{ get; set; }

		static Fraction()
		{
			MaxDenominator = 100000;
		}

		public Fraction(int numerator, int denominator) : this()
		{
			if (denominator == 0)
			{
				throw new DivideByZeroException(
					"Cannot divide by zero in creating a Fraction.");
			}

			Numerator = numerator;
			Denominator = denominator;
		}
		public Fraction(double value, int digitPrecision = 10) : this()
		{
			for (int den = 2; den < MaxDenominator; den++)
			{
				double numer = value * den;
				if (numer.IsInteger(digitPrecision))
				{
					Numerator = (int)numer;
					Denominator = den;
					return;
				}
			}
		}

		public double ToDouble()
		{
			return (double)Numerator / (double)Denominator;
		}

		public void Simplify()
		{
			if (Denominator < 0)
			{
				Numerator *= -1;
				Denominator *= -1;
			}

			if (Numerator == 0)
			{
				Denominator = 1;
			}

			while (MathPlus.GreatestCommonDenominator(Numerator, Denominator) != -1)
			{
				int lcm = MathPlus.GreatestCommonDenominator(Numerator, Denominator);
				Numerator /= lcm;
				Denominator /= lcm;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (obj is Fraction)
			{
				Fraction frac = (Fraction)obj;
				return MathPlus.Round(FloatingPoint, 20) == MathPlus.Round(frac.FloatingPoint, 20);
			}

			return obj.Equals(this);
		}

		public override int GetHashCode()
		{
			return ((short)Numerator << 16) + (short)Denominator;
		}

		public override string ToString()
		{
			return Numerator + " / " + Denominator;
		}

		#region interfaces
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "Compared object cannot be null.");
			}

			if (obj is Fraction)
			{
				Fraction frac = (Fraction)obj;
				return FloatingPoint.CompareTo(frac.FloatingPoint);
			}
			else if (obj is IComparable)
			{
				IComparable ic = (IComparable)obj;
				return -ic.CompareTo(FloatingPoint);
			}
			else
			{
				throw new FormatException("Cannot compare Fraction and " + 
					obj.GetType().ToString() + ".");
			}
		}

		public Fraction Add(Fraction other)
		{
			if (Denominator == other.Denominator)
			{
				return new Fraction(Numerator + other.Numerator, Denominator);
			}

			return new Fraction(FloatingPoint + other.FloatingPoint);
		}

		public Fraction Subtract(Fraction other)
		{
			if (Denominator == other.Denominator)
			{
				return new Fraction(Numerator - other.Numerator, Denominator);
			}

			return new Fraction(FloatingPoint + other.FloatingPoint);
		}

		public Fraction Multiply(Fraction other)
		{
			return new Fraction(Numerator * other.Numerator, Denominator * other.Denominator);
		}

		public Fraction Divide(Fraction other)
		{
			return Multiply(other.Inverse);
		}

		public Fraction Exponent(Fraction other)
		{
			return new Fraction(MathPlus.Pow(FloatingPoint, other.FloatingPoint));
		}

		public Fraction AbsoluteValue()
		{
			Fraction dupe = new Fraction(Numerator, Denominator);
			dupe.Simplify();

			if (Numerator < 0)
			{
				return new Fraction(-dupe.Numerator, dupe.Denominator);
			}
			else
			{
				return dupe;
			}
		}

		public int CompareTo(Fraction other)
		{
			return FloatingPoint.CompareTo(other.FloatingPoint);
		}

		public double Add(double other)
		{
			return FloatingPoint + other;
		}

		public double Subtract(double other)
		{
			return FloatingPoint - other;
		}

		public double Multiply(double other)
		{
			return FloatingPoint * other;
		}

		public double Divide(double other)
		{
			return FloatingPoint / other;
		}

		public double Exponent(double other)
		{
			return MathPlus.Pow(FloatingPoint, other);
		}

		double IMathable<double>.AbsoluteValue()
		{
			if (FloatingPoint < 0)
			{
				return -FloatingPoint;
			}
			else
			{
				return FloatingPoint;
			}
		}

		public int CompareTo(double other)
		{
			return FloatingPoint.CompareTo(other);
		}

		public bool Equals(Fraction other)
		{
			return MathPlus.Round(FloatingPoint, 20) == MathPlus.Round(other.FloatingPoint, 20);
		}

		public bool Equals(double other)
		{
			return MathPlus.Round(FloatingPoint, 20) == MathPlus.Round(other, 20);
		}
		#endregion

		#region operators
		public static bool operator==(Fraction a, Fraction b)
		{
			return a.Equals(b);
		}
		public static bool operator!=(Fraction a, Fraction b)
		{
			return !a.Equals(b);
		}

		public static Fraction operator+(Fraction a, Fraction b)
		{
			return a.Add(b);
		}
		public static Fraction operator-(Fraction a, Fraction b)
		{
			return a.Subtract(b);
		}
		public static Fraction operator-(Fraction frac)
		{
			return new Fraction(-frac.Numerator, frac.Denominator);
		}
		public static Fraction operator*(Fraction a, Fraction b)
		{
			return a.Multiply(b);
		}
		public static Fraction operator/(Fraction a, Fraction b)
		{
			return a.Divide(b);
		}

		public static explicit operator Fraction(double d)
		{
			return new Fraction(d);
		}
		public static explicit operator double(Fraction frac)
		{
			return frac.FloatingPoint;
		}

		public static explicit operator Fraction(Complex comp)
		{
			return new Fraction(comp.Real);
		}
		public static explicit operator Complex(Fraction frac)
		{
			return new Complex(frac.FloatingPoint);
		}
		#endregion
	}
}
