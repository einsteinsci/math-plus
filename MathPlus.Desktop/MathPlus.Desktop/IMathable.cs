﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlus.Desktop
{
	public interface IMathable<T> : IComparable<T>
	{
		T Add(T other);
		T Subtract(T other);
		T Multiply(T other);
		T Divide(T other);
		T Exponent(T other);


	}
}
