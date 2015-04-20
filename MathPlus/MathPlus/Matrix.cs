using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public class Matrix<T>
	{
		bool isIterating = false;

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
		int _width;

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
		int _height;

		T[][] arrays;

		public T this[int row, int col]
		{
			get
			{
				return arrays[col][row];
			}
			set
			{
				arrays[col][row] = value;
			}
		}

		public Matrix(int width, int height)
		{
			_width = width;
			_height = height;

			arrays = new T[width][];
			foreach (T[] a in arrays)
			{
				a = new T[height];
			}
		}

		private void resize(int width, int height)
		{
			T[][] buf = new T[Width][];
			
			for (int x = 0; x < arrays.Length && x < width; x++)
			{
				bool biggerX = x >= arrays.Length;

				buf[x] = new T[height];

				// if code hits here it is sure to have a row zero
				for (int y = 0; y < arrays[0].Length && y < height; y++)
				{
					bool biggerY = x >= arrays[0].Length;

					if (biggerX || biggerY)
					{
						buf[x][y] = default(T);
					}
					else
					{
						buf[x][y] = arrays[x][y];
					}
				}
			}

			arrays = buf;
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

		public static Matrix<double> Identity(int dim)
		{
			Matrix<double> mat = new Matrix<double>(dim, dim);

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
	}
}
