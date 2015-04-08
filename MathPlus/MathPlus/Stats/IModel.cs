using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	public interface IModel
	{
		double ScaledCDF(double bottom, double top);
	}
}
