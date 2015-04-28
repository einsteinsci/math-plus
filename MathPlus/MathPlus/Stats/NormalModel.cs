using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPlusLib.Stats
{
	/// <summary>
	/// Normal (Guassian) statistical model
	/// </summary>
	public struct NormalModel : IModel
	{
		/// <summary>
		/// Mean (center) of model
		/// </summary>
		public double Mean
		{ get; private set; }

		/// <summary>
		/// Standard Deviation (spread) of model
		/// </summary>
		public double SD
		{ get; private set; }

		/// <summary>
		/// Normal model with center zero and spread one
		/// </summary>
		public static NormalModel Root
		{
			get
			{
				return new NormalModel(0, 1);
			}
		}

		/// <summary>
		/// Instantiates a new instance of NormalModel
		/// </summary>
		/// <param name="mu">Mean of model</param>
		/// <param name="sigma">Standard Deviation/Error of model</param>
		public NormalModel(double mu, double sigma) : this()
		{
			Mean = mu;
			SD = sigma;
		}

		/// <summary>
		/// Returns Z-score of a particular value within the model
		/// </summary>
		/// <param name="value">Value for which to get the Z-score</param>
		/// <returns>Z-score of value</returns>
		public double ZScore(double value)
		{
			return (value - Mean) / SD;
		}

		/// <summary>
		/// Scaled CDF of specific model
		/// </summary>
		/// <param name="bottom">Lower bound value of CDF</param>
		/// <param name="top">Upper bound value of CDF</param>
		/// <returns>Proportion of model within the boundaries</returns>
		public double ScaledCDF(double bottom, double top)
		{
			return CDF(ZScore(bottom), ZScore(top));
		}

		/// <summary>
		/// Serializes the model to the format N(mean, sd) as per mathematical standard.
		/// </summary>
		/// <returns>Normal model as string</returns>
		public override string ToString()
		{
			return "N(" + Mean.ToString() + ", " + SD.ToString() + ")";
		}

		/// <summary>
		/// Unscaled CDF function of the Normal Model
		/// </summary>
		/// <param name="zTop">Upper bound Z-Score of CDF</param>
		/// <returns>Proportion of Normal Model below specified Z-Score</returns>
		public static double CDF(double zTop)
		{
			return phi(zTop);
		}
		/// <summary>
		/// Unscaled CDF function of the Normal Model
		/// </summary>
		/// <param name="zBot">Lower bound Z-Score of CDF</param>
		/// <param name="zTop">Upper bound Z-Score of CDF</param>
		/// <returns>Proportion of Normal Model between speciied Z-scores</returns>
		public static double CDF(double zBot, double zTop)
		{
			return phi(zTop) - phi(zBot);
		}

		/// <summary>
		/// Calculates inverse CDF of Normal Model
		/// </summary>
		/// <param name="p">Proportion of Normal Model starting at -inf.</param>
		/// <returns>Upper bound of CDF of given proportion</returns>
		public static double InverseCDF(double p)
		{
			Function2D rationalApproximation = (t) =>
			{
				// A & S formula 26.2.23
				// |error| < 4.5e-4
				double[] c = new double[] { 2.515517, 0.802853, 0.010328 };
				double[] d = new double[] { 1.432788, 0.189269, 0.001308 };

				return t - ((c[2] * t + c[1]) * t + c[0]) /
					(((d[2] * t + d[1]) * t + d[0]) * t + 1.0);
			};

			if (p <= 0.0 || p >= 1.0)
			{
				throw new ArgumentOutOfRangeException("p",
					"Probability must be between 0 and 1.");
			}

			if (p < 0.5)
			{
				// F^-1(p) = - G^-1(p)
				return -rationalApproximation(MathPlus.Sqrt(-2.0 * MathPlus.Ln(p)));
			}
			else
			{
				// F^-1(p) = G^-1(1 - p)
				return rationalApproximation(MathPlus.Sqrt(-2.0 * MathPlus.Ln(1.0 - p)));
			}
		}

		private static double phi(double z)
		{
			return 0.5 * (1.0 + erf(z / MathPlus.SQRT2));
		}

		private static double erf(double x)
		{
			if (x == 0)
			{
				return 0;
			}

			if (double.IsPositiveInfinity(x))
			{
				return 1;
			}

			if (double.IsNegativeInfinity(x))
			{
				return -1;
			}

			if (double.IsNaN(x))
			{
				return double.NaN;
			}

			return erfImp(x, false);
		}

		private static double polynomial(double z, params double[] coefficients)
		{
			double sum = coefficients[coefficients.Length - 1];
			for (int i = coefficients.Length - 2; i >= 0; --i)
			{
				sum *= z;
				sum += coefficients[i];
			}

			return sum;
		}

		// this function is huge...
		private static double erfImp(double z, bool invert)
		{
			if (z < 0)
			{
				if (!invert)
				{
					return -erfImp(-z, false);
				}

				if (z < -0.5)
				{
					return 2 - erfImp(-z, true);
				}

				return 1 + erfImp(-z, false);
			}

			double result;

			// Big bunch of selection statements now to pick which
			// implementation to use, try to put most likely options
			// first:
			if (z < 0.5)
			{
				// We're going to calculate erf:
				if (z < 1e-10)
				{
					result = (z * 1.125) + (z * 0.003379167095512573896158903121545171688);
				}
				else
				{
					// Worst case absolute error found: 6.688618532e-21
					double[] nc = 
					{ 
						 0.00337916709551257388990745, 
						-0.00073695653048167948530905, 
						-0.374732337392919607868241, 
						 0.0817442448733587196071743, 
						-0.0421089319936548595203468, 
						 0.0070165709512095756344528, 
						-0.00495091255982435110337458, 
						 0.000871646599037922480317225 
					};
					double[] dc = 
					{ 
						 1, 
						-0.218088218087924645390535, 
						 0.412542972725442099083918, 
						-0.0841891147873106755410271, 
						 0.0655338856400241519690695, 
						-0.0120019604454941768171266, 
						 0.00408165558926174048329689, 
						-0.000615900721557769691924509 
					};

					result = (z * 1.125) + (z * polynomial(z, nc) / polynomial(z, dc));
				}
			}
			else if ((z < 110) || ((z < 110) && invert))
			{
				// We'll be calculating erfc:
				invert = !invert;
				double r, b;
				if (z < 0.75)
				{
					// Worst case absolute error found: 5.582813374e-21
					double[] nc = 
					{ 
						-0.0361790390718262471360258, 
						 0.292251883444882683221149, 
						 0.281447041797604512774415, 
						 0.125610208862766947294894, 
						 0.0274135028268930549240776, 
						 0.00250839672168065762786937 
					};
					double[] dc = 
					{ 
						1, 
						1.8545005897903486499845, 
						1.43575803037831418074962, 
						0.582827658753036572454135, 
						0.124810476932949746447682, 
						0.0113724176546353285778481 
					};
					r = polynomial(z - 0.5, nc) / polynomial(z - 0.5, dc);
					b = 0.3440242112F;
				}
				else if (z < 1.25)
				{
					// Worst case absolute error found: 4.01854729e-21
					double[] nc = 
					{ 
						-0.0397876892611136856954425, 
						 0.153165212467878293257683, 
						 0.191260295600936245503129, 
						 0.10276327061989304213645, 
						 0.029637090615738836726027, 
						 0.0046093486780275489468812, 
						 0.000307607820348680180548455 
					};
					double[] dc = 
					{ 
						1, 
						1.95520072987627704987886, 
						1.64762317199384860109595, 
						0.768238607022126250082483, 
						0.209793185936509782784315, 
						0.0319569316899913392596356, 
						0.00213363160895785378615014 
					};
					r = polynomial(z - 0.75, nc) / polynomial(z - 0.75, dc);
					b = 0.419990927F;
				}
				else if (z < 2.25)
				{
					// Worst case absolute error found: 2.866005373e-21
					double[] nc = 
					{ 
						-0.0300838560557949717328341, 
						 0.0538578829844454508530552, 
						 0.0726211541651914182692959, 
						 0.0367628469888049348429018, 
						 0.00964629015572527529605267, 
						 0.00133453480075291076745275, 
						 0.778087599782504251917881e-4 
					};
					double[] dc = 
					{ 
						 1, 
						 1.75967098147167528287343, 
						 1.32883571437961120556307, 
						 0.552528596508757581287907,
						 0.133793056941332861912279, 
						 0.0179509645176280768640766, 
						 0.00104712440019937356634038, 
						-0.106640381820357337177643e-7 
					};
					r = polynomial(z - 1.25, nc) / polynomial(z - 1.25, dc);
					b = 0.4898625016F;
				}
				else if (z < 3.5)
				{
					// Worst case absolute error found: 1.045355789e-21
					double[] nc = 
					{ 
						-0.0117907570137227847827732, 
						 0.014262132090538809896674, 
						 0.0202234435902960820020765, 
						 0.00930668299990432009042239, 
						 0.00213357802422065994322516, 
						 0.00025022987386460102395382, 
						 0.120534912219588189822126e-4 
					};
					double[] dc = 
					{ 
						1, 
						1.50376225203620482047419, 
						0.965397786204462896346934, 
						0.339265230476796681555511, 
						0.0689740649541569716897427, 
						0.00771060262491768307365526, 
						0.000371421101531069302990367 
					};
					r = polynomial(z - 2.25, nc) / polynomial(z - 2.25, dc);
					b = 0.5317370892F;
				}
				else if (z < 5.25)
				{
					// Worst case absolute error found: 8.300028706e-22
					double[] nc = 
					{ 
						-0.00546954795538729307482955, 
						 0.00404190278731707110245394, 
						 0.0054963369553161170521356, 
						 0.00212616472603945399437862, 
						 0.000394984014495083900689956, 
						 0.365565477064442377259271e-4, 
						 0.135485897109932323253786e-5 
					};
					double[] dc = 
					{ 
						 1, 
						 1.21019697773630784832251, 
						 0.620914668221143886601045, 
						 0.173038430661142762569515, 
						 0.0276550813773432047594539, 
						 0.00240625974424309709745382, 
						 0.891811817251336577241006e-4, 
						-0.465528836283382684461025e-11 
					};
					r = polynomial(z - 3.5, nc) / polynomial(z - 3.5, dc);
					b = 0.5489973426F;
				}
				else if (z < 8)
				{
					// Worst case absolute error found: 1.700157534e-21
					double[] nc = 
					{ 
						-0.00270722535905778347999196, 
						 0.0013187563425029400461378, 
						 0.00119925933261002333923989, 
						 0.00027849619811344664248235, 
						 0.267822988218331849989363e-4, 
						 0.923043672315028197865066e-6 
					};
					double[] dc = 
					{ 
						1, 
						0.814632808543141591118279, 
						0.268901665856299542168425, 
						0.0449877216103041118694989, 
						0.00381759663320248459168994,
						0.000131571897888596914350697, 
						0.404815359675764138445257e-11 
					};
					r = polynomial(z - 5.25, nc) / polynomial(z - 5.25, dc);
					b = 0.5571740866F;
				}
				else if (z < 11.5)
				{
					// Worst case absolute error found: 3.002278011e-22
					double[] nc = 
					{ 
						-0.00109946720691742196814323, 
						 0.000406425442750422675169153, 
						 0.000274499489416900707787024, 
						 0.465293770646659383436343e-4, 
						 0.320955425395767463401993e-5, 
						 0.778286018145020892261936e-7 
					};
					double[] dc = 
					{ 
						1, 
						0.588173710611846046373373, 
						0.139363331289409746077541, 
						0.0166329340417083678763028, 
						0.00100023921310234908642639, 
						0.24254837521587225125068e-4 
					};
					r = polynomial(z - 8, nc) / polynomial(z - 8, dc);
					b = 0.5609807968F;
				}
				else if (z < 17)
				{
					// Worst case absolute error found: 6.741114695e-21
					double[] nc = 
					{ 
						-0.00056907993601094962855594,
						 0.000169498540373762264416984,
						 0.518472354581100890120501e-4, 
						 0.382819312231928859704678e-5, 
						 0.824989931281894431781794e-7 
					};
					double[] dc = 
					{ 
						 1, 
						 0.339637250051139347430323, 
						 0.043472647870310663055044, 
						 0.00248549335224637114641629, 
						 0.535633305337152900549536e-4, 
						-0.117490944405459578783846e-12 
					};
					r = polynomial(z - 11.5, nc) / polynomial(z - 11.5, dc);
					b = 0.5626493692F;
				}
				else if (z < 24)
				{
					// Worst case absolute error found: 7.802346984e-22
					double[] nc = 
					{ 
						-0.000241313599483991337479091, 
						 0.574224975202501512365975e-4, 
						 0.115998962927383778460557e-4, 
						 0.581762134402593739370875e-6, 
						 0.853971555085673614607418e-8 
					};
					double[] dc = 
					{ 
						1, 
						0.233044138299687841018015, 
						0.0204186940546440312625597, 
						0.000797185647564398289151125, 
						0.117019281670172327758019e-4 
					};
					r = polynomial(z - 17, nc) / polynomial(z - 17, dc);
					b = 0.5634598136F;
				}
				else if (z < 38)
				{
					// Worst case absolute error found: 2.414228989e-22
					double[] nc = 
					{ 
						-0.000146674699277760365803642, 
						 0.162666552112280519955647e-4, 
						 0.269116248509165239294897e-5, 
						 0.979584479468091935086972e-7, 
						 0.101994647625723465722285e-8 
					};
					double[] dc = 
					{ 
						1, 
						0.165907812944847226546036, 
						0.0103361716191505884359634, 
						0.000286593026373868366935721, 
						0.298401570840900340874568e-5 
					};
					r = polynomial(z - 24, nc) / polynomial(z - 24, dc);
					b = 0.5638477802F;
				}
				else if (z < 60)
				{
					// Worst case absolute error found: 5.896543869e-24
					double[] nc = 
					{ 
						-0.583905797629771786720406e-4, 
						 0.412510325105496173512992e-5, 
						 0.431790922420250949096906e-6, 
						 0.993365155590013193345569e-8, 
						 0.653480510020104699270084e-10 
					};
					double[] dc = 
					{ 
						1, 
						0.105077086072039915406159, 
						0.00414278428675475620830226, 
						0.726338754644523769144108e-4, 
						0.477818471047398785369849e-6 
					};
					r = polynomial(z - 38, nc) / polynomial(z - 38, dc);
					b = 0.5640528202F;
				}
				else if (z < 85)
				{
					// Worst case absolute error found: 3.080612264e-21
					double[] nc = 
					{ 
						-0.196457797609229579459841e-4, 
						 0.157243887666800692441195e-5, 
						 0.543902511192700878690335e-7, 
						 0.317472492369117710852685e-9 };
					double[] dc = 
					{ 
						1, 
						0.052803989240957632204885, 
						0.000926876069151753290378112,
						0.541011723226630257077328e-5, 
						0.535093845803642394908747e-15 
					};
					r = polynomial(z - 60, nc) / polynomial(z - 60, dc);
					b = 0.5641309023F;
				}
				else
				{
					// Worst case absolute error found: 8.094633491e-22
					double[] nc = 
					{ 
						-0.789224703978722689089794e-5, 
						0.622088451660986955124162e-6, 
						0.145728445676882396797184e-7, 
						0.603715505542715364529243e-10 
					};
					double[] dc = 
					{ 
						1, 
						0.0375328846356293715248719, 
						0.000467919535974625308126054, 
						0.193847039275845656900547e-5 
					};
					r = polynomial(z - 85, nc) / polynomial(z - 85, dc);
					b = 0.5641584396F;
				}

				double g = Math.Exp(-z * z) / z;
				result = (g * b) + (g * r);
			}
			else
			{
				// Any value of z larger than 28 will underflow to zero:
				result = 0;
				invert = !invert;
			}

			if (invert)
			{
				result = 1 - result;
			}

			return result;
		}
	}
}
