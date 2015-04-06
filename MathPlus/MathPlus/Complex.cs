using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public struct Complex : IEquatable<Complex>, IEquatable<double>
	{
		public double Real
		{ get; set; }
		public double Imag
		{ get; set; }

		// Polar
		public double Argument
		{
			get
			{
				return MathPlus.Trig.ATan(Imag / Real);
			}
		}
		public double AbsoluteValue
		{
			get
			{
				return MathPlus.Sqrt(Real * Real + Imag * Imag);
			}
		}
		
		public static Complex FromPolar(double arg, double abs)
		{
			return new Complex(MathPlus.Trig.Cos(arg) * abs,
				MathPlus.Trig.Sin(arg) * abs);
		}

		public Complex(double real, double imag) : this()
		{
			Real = real;
			Imag = imag;
		}
		public Complex(double real) : this(real, 0)
		{ }

		public Complex Conjugate()
		{
			return new Complex(Real, -Imag);
		}

		public override bool Equals(object obj)
		{
			if (obj is Complex)
			{
				return Equals((Complex)obj);
			}
			else if (obj is double)
			{
				return Equals((double)obj);
			}
			else if (obj is IComparable)
			{
				return obj.Equals(Real) && Imag.Equals(0.0);
			}
			else
			{
				return false;
			}
		}
		public override int GetHashCode()
		{
			return Real.GetHashCode() << 16 + Imag.GetHashCode();
		}

		public override string ToString()
		{
			if (Imag == 0)
			{
				return Real.ToString();
			}
			else if (Real == 0)
			{
				return Imag.ToString() + "i";
			}
			else
			{
				return Real.ToString() + " + " + Imag.ToString() + "i";
			}
		}

		#region interfaces
		public bool Equals(Complex other)
		{
			return Real.Equals(other.Real) && Imag.Equals(other.Imag);
		}
		public bool Equals(double other)
		{
			return Real.Equals(other) && Imag.Equals(0.0);
		}

		public Complex Add(Complex other)
		{
			return new Complex(Real + other.Real, Imag + other.Imag);
		}
		public Complex Add(double other)
		{
			return new Complex(Real + other, Imag);
		}
		public Complex Subtract(Complex other)
		{
			return new Complex(Real - other.Real, Imag - other.Imag);
		}
		public Complex Subtract(double other)
		{
			return new Complex(Real - other, Imag);
		}
		public Complex Multiply(Complex other)
		{
			return new Complex(Real * other.Real + Imag * other.Imag,
				Real * other.Imag + Imag * other.Real);
		}
		public Complex Multiply(double other)
		{
			return new Complex(Real * other, Imag * other);
		}
		public Complex Divide(Complex other)
		{
			Complex conj = Conjugate();
			Complex top = Multiply(conj);
			Complex bot = other.Multiply(conj);
			double botNum = bot.Real; // Imaginary will be zero
			return top.Divide(botNum);
		}
		public Complex Divide(double other)
		{
			return new Complex(Real / other, Imag / other);
		}
		#endregion

		#region operators
		public static bool operator==(Complex a, Complex b)
		{
			return a.Equals(b);
		}
		public static bool operator!=(Complex a, Complex b)
		{
			return !a.Equals(b);
		}

		public static Complex operator+(Complex a, Complex b)
		{
			return a.Add(b);
		}
		public static Complex operator+(Complex a, double b)
		{
			return a.Add(b);
		}
		public static Complex operator-(Complex a, Complex b)
		{
			return a.Subtract(b);
		}
		public static Complex operator-(Complex a, double b)
		{
			return a.Subtract(b);
		}
		public static Complex operator*(Complex a, Complex b)
		{
			return a.Multiply(b);
		}
		public static Complex operator*(Complex a, double b)
		{
			return a.Multiply(b);
		}
		public static Complex operator/(Complex a, Complex b)
		{
			return a.Divide(b);
		}
		public static Complex operator/(Complex a, double b)
		{
			return a.Divide(b);
		}

		public static explicit operator double(Complex a)
		{
			return a.Real;
		}
		public static explicit operator Complex(double r)
		{
			return new Complex(r);
		}
		#endregion
	}
}
