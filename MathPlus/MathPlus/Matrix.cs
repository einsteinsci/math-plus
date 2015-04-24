using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public class Matrix<T>
	{
		protected bool isIterating = false;

		public int Width
		{
			get
			{
				return _width;
			}
			set
			{
				resize(value, Height);
			}
		}
		protected int _width;

		public int Height
		{
			get
			{
				return _height;
			}
			set
			{
				resize(Width, value);
			}
		}
		protected int _height;

		/// <summary>
		/// All data in matrix. Each subarray represents a column.
		/// </summary>
		protected T[][] data;

		public T this[int row, int col]
		{
			get
			{
				return data[col][row];
			}
			set
			{
				data[col][row] = value;
			}
		}

		public Matrix(int width, int height)
		{
			_width = width;
			_height = height;

			data = new T[width][];
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = new T[height];
			}
		}
		public Matrix(T[][] array)
		{
			_height = array.Length;
			_width = array[0].Length;

			data = new T[_width][];
			for (int i = 0; i < array[0].Length; i++)
			{
				data[i] = new T[_height];
				for (int j = 0; j < array.Length; j++)
				{
					data[i][j] = array[j][i];
				}
			}
		}

		protected void resize(int width, int height)
		{
			T[][] buf = new T[Width][];
			
			for (int x = 0; x < data.Length && x < width; x++)
			{
				bool biggerX = x >= data.Length;

				buf[x] = new T[height];

				// if code hits here it is sure to have a row zero
				for (int y = 0; y < data[0].Length && y < height; y++)
				{
					bool biggerY = x >= data[0].Length;

					if (biggerX || biggerY)
					{
						buf[x][y] = default(T);
					}
					else
					{
						buf[x][y] = data[x][y];
					}
				}
			}

			data = buf;
		}

		public T[] GetRow(int row)
		{
			T[] res = new T[Width];

			for (int i = 0; i < Width; i++)
			{
				res[i] = this[row, i];
			}

			return res;
		}
		public T[] GetColumn(int col)
		{
			T[] res = new T[Height];

			for (int i = 0; i < Height; i++)
			{
				res[i] = this[i, col];
			}

			return res;
		}

		public static MathMatrix Identity(int dim)
		{
			MathMatrix mat = new MathMatrix(dim, dim);

			for (int i = 0; i < dim; i++)
			{
				mat[i, i] = 1.0;
			}

			return mat;
		}
		public static Matrix<int> IdentityInt(int dim)
		{
			Matrix<int> mat = new Matrix<int>(dim, dim);

			for (int i = 0; i < dim; i++)
			{
				mat[i, i] = 1;
			}

			return mat;
		}

		public List<T> ToList()
		{
			List<T> res = new List<T>();
			for (int r = 0; r < Height; r++)
			{
				for (int c = 0; c < Height; c++) // Nope, actually, C#
				{
					res.Add(this[r, c]);
				}
			}

			return res;
		}

		public override bool Equals(object obj)
		{
			Matrix<T> other = obj as Matrix<T>;
			if (other == null)
			{
				return false;
			}

			if (other.Height != Height || other.Width != Width)
			{
				return false;
			}

			for (int r = 0; r < Height; r++)
			{
				for (int c = 0; c < Width; c++) // actually C# here
				{
					if (other[r, c] == null && this[r, c] != null)
					{
						return false;
					}
					if (other[r, c] != null && this[r, c] == null)
					{
						return false;
					}

					if (other[r, c].Equals(this[r, c]))
					{
						return false;
					}
				}
			}

			return true;
		}

		public override int GetHashCode()
		{
			int sum = 0;
			for (int r = 0; r < Height; r++)
			{
				for (int c = 0; c < Width; c++) // still C#
				{
					if (this[r, c] != null)
					{
						sum += this[r, c].GetHashCode();
					}
				}
			}

			return sum;
		}

		public override string ToString()
		{
			return ToString(16);
		}
		public string ToString(int maxItemLength)
		{
			int maxLen = 0;
			List<T> list = ToList();
			foreach (T t in list)
			{
				if (t == null)
				{
					continue;
				}

				string s = t.ToString();
				if (s.Length > maxLen)
				{
					maxLen = s.Length;
				}
			}

			maxLen = Math.Min(maxLen, maxItemLength);

			string[] rows = new string[Height];
			for (int r = 0; r < Height; r++)
			{
				string rowStr = "";
				for (int c = 0; c < Width; c++)
				{
					string item = this[r, c].ToString();
					if (item.Length > maxLen)
					{
						item = item.Substring(0, maxLen);
					}
					else if (item.Length < maxLen)
					{
						int charsToFill = maxLen - item.Length;
						string buf = "";
						for (int i = 0; i < charsToFill; i++)
						{
							buf += " ";
						}
						item += buf;
					}

					rowStr += "[ " + item + " ]";
				}
				rowStr += "\n";
				rows[r] = rowStr;
			}

			string res = "";
			foreach (string s in rows)
			{
				res += s;
			}
			return res;
		}

		public void Foreach(Action<T> todo)
		{
			isIterating = true;
			for (int r = 0; r < Height; r++)
			{
				for (int c = 0; c < Width; c++) // Still C# here
				{
					if (!isIterating)
					{
						return;
					}

					todo(this[r, c]);
				}
			}

			isIterating = false;
		}
		public void Foreach(Action<int, int, T> action)
		{
			isIterating = true;
			for (int r = 0; r < Height; r++)
			{
				for (int c = 0; c < Width; c++) // And still C#
				{
					if (!isIterating)
					{
						return;
					}

					action(r, c, this[r, c]);
				}
			}
		}
		public void Break()
		{
			isIterating = false;
		}

		public bool Exists(Predicate<T> matches)
		{
			bool result = false;
			Foreach((t) =>
			{
				if (matches(t))
				{
					result = true;
					Break();
				}
			});

			return result;
		}

		public T FirstOrDefault(Predicate<T> matches)
		{
			T res = default(T);
			Foreach((t) =>
			{
				if (matches(t))
				{
					res = t;
					Break();
				}
			});

			return res;
		}

		public List<T> FindAll(Predicate<T> matches)
		{
			List<T> result = new List<T>();
			Foreach((t) =>
			{
				if (matches(t))
				{
					result.Add(t);
				}
			});

			return result;
		}

		public Matrix<T> GetExcludedSubMatrix(int row, int col)
		{
			Matrix<T> res = new Matrix<T>(Width - 1, Height - 1);

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
	}
}
