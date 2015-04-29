using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	/// <summary>
	/// A quantity represented by one integer divided by another.
	/// </summary>
	public struct Fraction : IComparable, IMathable<Fraction>, IMathable<double>, 
		IEquatable<Fraction>, IEquatable<double>
	{
		/// <summary>
		/// Upper part of the fraction
		/// </summary>
		public int Numerator
		{ get; private set; }

		/// <summary>
		/// Lower part of the fraction
		/// </summary>
		public int Denominator
		{ get; private set; }

		/// <summary>
		/// Converts the fraction to its <see cref="Double"/> equivalent
		/// </summary>
		public double FloatingPoint
		{
			get
			{
				return ToDouble();
			}
		}

		/// <summary>
		/// Returns the inverse, 1 / <c>this</c>.
		/// </summary>
		public Fraction Inverse
		{
			get
			{
				return new Fraction(Denominator, Numerator);
			}
		}

		/// <summary>
		/// Static value for the maximum denominator to use when creating 
		/// <c>Fraction</c> from a <see cref="Double"/>.
		/// </summary>
		public static int MaxDenominator
		{ get; set; }

		/// <summary>
		/// Initializes <see cref="MaxDenominator"/> to 1 000 000.
		/// </summary>
		static Fraction()
		{
			MaxDenominator = 1000000;
		}

		/// <summary>
		/// Instantiates a <c>Fraction</c> from a given numerator and denominator
		/// </summary>
		/// <param name="numerator">Numerator of fraction</param>
		/// <param name="denominator">Denominator of fraction</param>
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
		/// <summary>
		/// Instantiates a <c>Fraction</c> from a given <see cref="Double"/> value.
		/// </summary>
		/// <param name="value">Value to convert to a fraction</param>
		/// <param name="digitPrecision">
		/// Precision to use when testing potential denominators
		/// </param>
		/// <remarks>
		/// This algorithm can be quite slow if decimal values are off by 
		/// a slight amount.
		/// </remarks>
		public Fraction(double value, int digitPrecision = 6) : this()
		{
			for (int den = 1; den < MaxDenominator; den++)
			{
				double numer = value * (double)den;
				if (numer.IsInteger(digitPrecision))
				{
					Numerator = (int)numer;
					Denominator = den;
					return;
				}
			}
		}

		/// <summary>
		/// Divides the fraction to a <see cref="Double"/>.
		/// </summary>
		/// <returns>A <c>double</c> from 
		/// <see cref="Numerator"/> / <see cref="Denominator"/></returns>
		public double ToDouble()
		{
			return (double)Numerator / (double)Denominator;
		}

		/// <summary>
		/// Simplifies fraction so that the denominator and numerator have no 
		/// common factors.
		/// </summary>
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

			while (MathPlus.Numerics.GreatestCommonDenominator(Numerator, Denominator) != -1)
			{
				int lcm = MathPlus.Numerics.GreatestCommonDenominator(Numerator, Denominator);
				Numerator /= lcm;
				Denominator /= lcm;
			}
		}

		/// <summary>
		/// Compares <c>this</c> to another <c>object</c> to see if they are equal.
		/// </summary>
		/// <param name="obj">Object to compare against</param>
		/// <returns>True if the objects are compatible and equal, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (obj is Fraction)
			{
				Fraction frac = (Fraction)obj;
				return MathPlus.Numerics.Round(FloatingPoint, 20) == MathPlus.Numerics.Round(frac.FloatingPoint, 20);
			}

			return obj.Equals(this);
		}

		/// <summary>
		/// Converts fraction to a hashcode <see cref="Int32"/>.
		/// </summary>
		/// <returns>
		/// An <see cref="Int32"/> generated from the 
		/// <see cref="Numerator"/> and <see cref="Denominator"/>
		/// </returns>
		/// <remarks>Both values will be truncated to an <see cref="Int16"/> when generating.</remarks>
		public override int GetHashCode()
		{
			return ((short)Numerator << 16) + (short)Denominator;
		}

		/// <summary>
		/// Serializes to a mathematically friendly <see cref="String"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> in the form of 
		/// <see cref="Numerator"/> / <see cref="Denominator"/>.
		/// </returns>
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
			return MathPlus.Numerics.Round(FloatingPoint, 20) == MathPlus.Numerics.Round(other.FloatingPoint, 20);
		}

		public bool Equals(double other)
		{
			return MathPlus.Numerics.Round(FloatingPoint, 20) == MathPlus.Numerics.Round(other, 20);
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
