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

		public MathMatrix GetExcludedSubMatrix(int row, int col)
		{
			MathMatrix res = new MathMatrix(Width - 1, Height - 1);

			for (int r1 = 0, r2 = 0; r1 < Height; r1++)
			{
				if (r1 == row)
				{
					continue;
				}

				for (int c1 = 0, c2 = 0; c1 < Width; c1++)
				{
					if (c1 == col)
					{
						continue;
					}

					res[r2, c2] = this[r1, c1];
					c2++;
				}
				r2++;
			}

			return res;
		}

		public double Determinant()
		{
			if (Width != Height)
			{
				throw new InvalidOperationException(
					"Cannot find determinant of a non-square matrix.");
			}

			throw new NotImplementedException();
		}
	}
}
