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
			data = new double[_width][];
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = new double[_height];
				for (int j = 0; j < data[i].Length; j++)
				{
					data[i][j] = copied[j, i];
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

		/// <summary>
		/// Parses string matrix in form of '[[A,B,C],[D,E,F],[G,H,I]]'.
		/// Whitespace is allowed and ignored.
		/// </summary>
		/// <param name="matrix">String to parse</param>
		/// <returns>New MathMatrix from string matrix</returns>
		public static MathMatrix Parse(string matrix)
		{
			string noWS = matrix.Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "");
			List<List<string>> unparsed = new List<List<string>>();
			string noOuterBounds = noWS.Substring(1, noWS.Length - 2);

			int row = 0;
			int col = 0;
			bool inRow = false;
			bool startedNum = false;
			
			foreach (char c in noOuterBounds)
			{
				if (c == '[')
				{
					inRow = true;
					unparsed.Add(new List<string>());
					continue; // ignore
				}
				if (c == ']')
				{
					inRow = false;
					continue;
				}
				if (c == ',')
				{
					startedNum = false;
					if (inRow)
					{
						col++;
					}
					else
					{
						row++;
						col = 0;
					}
					continue;
				}

				if (!startedNum)
				{
					startedNum = true;
					(unparsed[row]).Add(c.ToString());
				}
				else
				{
					(unparsed[row])[col] += c;
				}
			}

			int rowCount = 0;
			int colCount = 0;
			foreach (List<string> rowStrings in unparsed)
			{
				rowCount++;
				foreach (string s in rowStrings)
				{
					if (rowCount == 1)
					{
						colCount++;
					}
				}

				if (rowStrings.Count != colCount)
				{
					throw new FormatException("String shows inconsistent column counts.");
				}
			}

			MathMatrix res = new MathMatrix(colCount, rowCount);
			for (int r = 0; r < rowCount; r++)
			{
				for (int c = 0; c < colCount; c++) // actually C#
				{
					double val = double.NaN;
					try
					{
						val = double.Parse((unparsed[r])[c]);
					}
					catch (FormatException e)
					{
						throw new FormatException("Invalid number at row " + 
							r.ToString() + ", col " + c.ToString() + ".", e);
					}
					catch (Exception e)
					{
						throw e;
					}

					if (double.IsNaN(val))
					{
						throw new Exception("Failed to parse number at row " +
							r.ToString() + ", col " + c.ToString() + ".");
					}

					res[r, c] = val;
				}
			}

			return res;
		}
	}
}
