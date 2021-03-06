﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// Base interface for all test result classes in this library
	/// </summary>
	public interface ITestResults
	{
		/// <summary>
		/// Tested value used in model
		/// </summary>
		double TestedValue
		{ get; }

		/// <summary>
		/// True if the null hypothesis should be rejected, false if this fails.
		/// </summary>
		bool RejectNullHypothesis
		{ get; }

		/// <summary>
		/// Value for which the the tested value is compared to
		/// </summary>
		double NullHypothesis
		{ get; }

		/// <summary>
		/// Direction to assume the actual value is if the null hypothesis is rejected.
		/// </summary>
		InequalityType AltHypothesis
		{ get; }

		/// <summary>
		/// Probability that null hypothesis is true for a tested value as such or 
		/// further, depending on alternate hypothesis.
		/// </summary>
		double Probability
		{ get; }
	}
}
