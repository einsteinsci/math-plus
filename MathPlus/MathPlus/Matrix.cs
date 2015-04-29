using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	/// <summary>
	/// Matrix of objects oriented in a grid
	/// </summary>
	/// <typeparam name="T">Type of objects stored in matrix</typeparam>
	public class Matrix<T>
	{
		/// <summary>
		/// True while <see cref="Foreach(Action<T>)"/> is running.
		/// Prevents concurrent modification.
		/// </summary>
		protected bool isIterating = false;

		/// <summary>
		/// Width of matrix
		/// </summary>
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

		/// <summary>
		/// Height of matrix
		/// </summary>
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

		/// <summary>
		/// Accessor for data within matrix
		/// </summary>
		/// <param name="row">Row index of datum</param>
		/// <param name="col">Column index of datum</param>
		/// <returns>
		/// Datum referenced by [<paramref name="row"/>, <paramref name="col"/>]
		/// </returns>
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

		/// <summary>
		/// Instantiates a new <c>Matrix</c> from a given width and height
		/// </summary>
		/// <param name="width">Width of new <c>Matrix</c></param>
		/// <param name="height">Height of new <c>Matrix</c></param>
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
		/// <summary>
		/// Instantiates a new <c>Matrix</c> from a given array of arrays,
		/// copying all data inside
		/// </summary>
		/// <param name="array">Array of arrays to copy</param>
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

		/// <summary>
		/// Instantiates a new <c>Matrix</c> from a given multidimensional 
		/// array, copying all data inside
		/// </summary>
		/// <param name="array">Multidimensional array to copy</param>
		public Matrix(T[,] array)
		{
			_height = array.GetLength(0);
			_width = array.GetLength(1);

			data = new T[_width][];
			for (int i = 0; i < _width; i++)
			{
				data[i] = new T[_height];
				for (int j = 0; j < array.Length; j++)
				{
					data[i][j] = array[j, i];
				}
			}
		}

		/// <summary>
		/// Resizes the matrix when the width or height is changed.
		/// Shrinking the matrix will delete data from the highest-ranked 
		/// rows and/or columns permanently.
		/// </summary>
		/// <param name="width">New width of matrix</param>
		/// <param name="height">New height of matrix</param>
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

		/// <summary>
		/// Returns array of objects in a specified row
		/// </summary>
		/// <param name="row">Index of row to obtain</param>
		/// <returns>An array consisting of all data in the specified row</returns>
		public T[] GetRow(int row)
		{
			T[] res = new T[Width];

			for (int i = 0; i < Width; i++)
			{
				res[i] = this[row, i];
			}

			return res;
		}
		/// <summary>
		/// Returns array of objects in a specified column
		/// </summary>
		/// <param name="col">Index of column to obtain</param>
		/// <returns>An array consisting of all data in the specified column</returns>
		public T[] GetColumn(int col)
		{
			T[] res = new T[Height];

			for (int i = 0; i < Height; i++)
			{
				res[i] = this[i, col];
			}

			return res;
		}

		/// <summary>
		/// Returns an identity matrix of specified size
		/// </summary>
		/// <param name="dim">Size of identity matrix</param>
		/// <returns>An identity matrix of size <paramref name="dim"/>.</returns>
		public static MathMatrix Identity(int dim)
		{
			MathMatrix mat = new MathMatrix(dim, dim);

			for (int i = 0; i < dim; i++)
			{
				mat[i, i] = 1.0;
			}

			return mat;
		}
		/// <summary>
		/// Returns an identity matrix of specified size
		/// </summary>
		/// <param name="dim">Size of identity Matrix</param>
		/// <returns>An identity matrix of size <paramref name="dim"/>.</returns>
		public static Matrix<int> IdentityInt(int dim)
		{
			Matrix<int> mat = new Matrix<int>(dim, dim);

			for (int i = 0; i < dim; i++)
			{
				mat[i, i] = 1;
			}

			return mat;
		}

		/// <summary>
		/// Returns a list of all data in matrix
		/// </summary>
		/// <returns>A list of all data in matrix</returns>
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

		/// <summary>
		/// Compares matrix to another <c>object</c>
		/// </summary>
		/// <param name="obj"><see cref="Object"/> to compare to</param>
		/// <returns>
		/// True if both objects are equal-sized matrices of equal content, false otherwise.
		/// </returns>
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

		/// <summary>
		/// Adds the hashcodes of all contents to produce a hashcode of the matrix.
		/// </summary>
		/// <returns>A compiled hashcode of the matrix</returns>
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

		/// <summary>
		/// Serializes the matrix to a readable string.
		/// </summary>
		/// <returns>A readable grid of the data, each row separated by newlines</returns>
		/// <remarks>Calls <see cref="ToString(int)"/>, with <c>maxItemLength</c> of 16</remarks>
		public override string ToString()
		{
			return ToString(16);
		}
		/// <summary>
		/// Serializes the matrix to a readable string.
		/// </summary>
		/// <param name="maxItemLength">Maximum length of each item before truncating.</param>
		/// <returns>A readable grid of the dat, each row separated by newlines</returns>
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

		/// <summary>
		/// Iterates over each item, applying an action to it
		/// </summary>
		/// <param name="todo">
		/// Action taking only one parameter of type <typeparamref name="T"/>.
		/// </param>
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
		/// <summary>
		/// Iterates over each item, applying an action to it
		/// </summary>
		/// <param name="action">
		/// Action taking two <c>int</c> parameters for row and column (respectively),
		/// followed by a parameter of type <typeparamref name="T"/>.
		/// </param>
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
		/// <summary>
		/// Breaks out of a running <see cref="Foreach(Action<T>)"/> operation.
		/// Has no effect outside of it.
		/// </summary>
		public void Break()
		{
			isIterating = false;
		}

		/// <summary>
		/// Checks if any element matches a specified <see cref="Predicate"/>.
		/// </summary>
		/// <param name="matches">Predicate testing each item</param>
		/// <returns>
		/// True if <paramref name="matches"/> returns true for any element,
		/// false otherwise.
		/// </returns>
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

		/// <summary>
		/// Finds the first element that matches a specified <see cref="Predicate"/>.
		/// </summary>
		/// <param name="matches">Predicate testing each item</param>
		/// <returns>First item <paramref name="matches"/> returns true on, default if none match.</returns>
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
