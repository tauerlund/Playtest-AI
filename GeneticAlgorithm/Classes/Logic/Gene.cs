using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Contains a number which represent the smallest part of a chromosome.
  /// </summary>
  [Serializable]
  public class Gene {
    public double Value { get; }

    /// <summary>
    /// Makes a random gene.
    /// </summary>
    /// <param name="random"></param>
    public Gene(IRandomNumberGenerator random) {
      Value = random.GetDouble(-1, 1);
    }

    /// <summary>
    /// Assigns given value to the value of the gene.
    /// </summary>
    /// <param name="number"></param>
    public Gene(double number) {
      Value = number;
    }
  }
}
