using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	/// <summary>
	/// Represents a quantity mathematical operations can be performed upon
	/// </summary>
	/// <typeparam name="T">Type which operations can be performed with</typeparam>
	public interface IMathable<T> : IComparable<T>
	{
		/// <summary>
		/// Adds with <paramref name="other"/> to produce a sum.
		/// </summary>
		/// <param name="other">Object to add</param>
		/// <returns>The sum of <c>this</c> and <paramref name="other"/>.</returns>
		T Add(T other);
		/// <summary>
		/// Subtracts <paramref name="other"/> to produce a difference.
		/// </summary>
		/// <param name="other">Object to subtract</param>
		/// <returns>The difference of <c>this</c> and <paramref name="other"/>.</returns>
		T Subtract(T other);
		/// <summary>
		/// Multiplies with <paramref name="other"/> to produce a product.
		/// </summary>
		/// <param name="other">Object to multiply with</param>
		/// <returns>The product of <c>this</c> and <paramref name="other"/>.</returns>
		T Multiply(T other);
		/// <summary>
		/// Divides by <paramref name="other"/> to produce a quotient.
		/// </summary>
		/// <param name="other">Object to divide by</param>
		/// <returns>The quotient of <c>this</c> and <paramref name="other"/>.</returns>
		T Divide(T other);
		/// <summary>
		/// Raises <c>this</c> to the power of <paramref name="other"/>.
		/// </summary>
		/// <param name="other">Exponent in the operation</param>
		/// <returns><c>this</c> ^ <paramref name="other"/>.</returns>
		T Exponent(T other);

		/// <summary>
		/// Absolute value of <c>this</c>.
		/// </summary>
		/// <returns>The absolute value of <c>this</c></returns>
		T AbsoluteValue();
	}
}
