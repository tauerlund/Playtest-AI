using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Represents a crossover method that crosses passed agents by randomly selecting a point where the chromsomes
  /// of two randomly selected parents will be combined.
  /// </summary>
  [Serializable]
  public class OnePointCombineCrossoverRegular : OnePointCombineCrossover, ICrossover {

    public List<Agent> MakeCrossovers(List<Agent> parentAgents, int populationSize, IRandomNumberGenerator random) { 
      List<Agent> children = MakeOnePointCombineCrossovers(parentAgents, populationSize, random);
      return children;
    }

    public override string ToString() {
      return "One-Point Combine Regular";
    }
  }
}
