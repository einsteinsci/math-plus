using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public class MathMatrix : Matrix<double>
	{
		public MathMatrix(int width, int height) : 
			base(width, height) { }

		public MathMatrix(Matrix<double> copied) : 
			base(copied.Width, copied.Height)
		{


			arrays = new double[_width][];
			for (int i = 0; i < arrays.Length; i++)
			{
				arrays[i] = new double[_height];
				for (int j = 0; j < arrays[i].Length; j++)
				{
					arrays[i][j] = copied[j, i];
				}
			}
		}

		public MathMatrix(double[][] array) :
			base(array) { }

		public double Determinant()
		{
			if (Width != Height)
			{
				throw new InvalidOperationException(
					"Cannot find determinant of a non-square matrix.");
			}

			if (Width == 2)
			{
				double forward =  this[0, 0] * this[1, 1];
				double backward = this[1, 0] * this[0, 1];
				return forward - backward;
			}

			int sign = 1;
			double sigma = 0;
			for (int c = 0; c < Width; c++) // actually C#
			{
				MathMatrix inner = new MathMatrix(GetExcludedSubMatrix(0, c));
				sigma += sign * this[0, c] * inner.Determinant();
				sign = -sign;
			}
			return sigma;
		}

		public double SumRow(int row)
		{
			double sigma = 0;
			for (int i = 0; i < Width; i++)
			{
				sigma += this[row, i];
			}

			return sigma;
		}

		public double SumColumn(int col)
		{
			double sigma = 0;
			for (int i = 0; i < Height; i++)
			{
				sigma += this[i, col];
			}

			return sigma;
		}

		public double SumAll()
		{
			double sigma = 0;
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					sigma += this[i, j];
				}
			}

			return sigma;
		}
	}
}
