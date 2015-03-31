using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MathPlusLib
{
	/// <summary>
	/// Exception designed to be caught when performing statistics tests and creating intervals,
	/// stating that for the data a model or test would be inappropriate.
	/// </summary>
	[Serializable]
	public class StatisticInappropriateException : Exception
	{
		public StatisticInappropriateException() 
		{ }
		public StatisticInappropriateException(string message) : base(message) 
		{ }
		public StatisticInappropriateException(string message, Exception inner) : base(message, inner) 
		{ }
		protected StatisticInappropriateException(SerializationInfo info, StreamingContext context)
			: base(info, context) 
		{ }
	}
}
