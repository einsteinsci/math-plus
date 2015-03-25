using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPlusLib.Desktop;
using MathPlusLib.Portable;

namespace MathPlusLib
{
	public interface IModel
	{
		Number Probability(Number value);
	}
}
