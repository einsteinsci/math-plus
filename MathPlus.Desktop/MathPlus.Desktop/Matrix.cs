using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlus.Desktop
{
	public class Matrix<T>
	{
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

		public T this[int x, int y]
		{
			get
			{
				return arrays[x][y];
			}
			set
			{
				arrays[x][y] = value;
			}
		}

		public Matrix(int width, int height)
		{
			_width = width;
			_height = height;

			arrays = new T[width][];
		}

		private void resize(int width, int height)
		{

		}
	}
}
