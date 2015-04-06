using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib
{
	public interface IModel
	{
		double Probability(double bottom, double top);
	}
}
