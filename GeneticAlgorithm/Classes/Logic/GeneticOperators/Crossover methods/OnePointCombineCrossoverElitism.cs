using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Represents a crossover method that keeps the parents.
  /// </summary>
  [Serializable]
  public class OnePointCombineCrossoverElitism : OnePointCombineCrossover, ICrossover {

    public List<Agent> MakeCrossovers(List<Agent> parentAgents, int populationSize, IRandomNumberGenerator random) {
      List<Agent> children = MakeOnePointCombineCrossovers(parentAgents, populationSize - parentAgents.Count, random );
      children.AddRange(parentAgents); // Add parents
      return children;
    }

    public override string ToString() {
      return "One-Point Combine and Elitism";
    }
  }
}
