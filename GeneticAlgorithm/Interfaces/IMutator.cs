using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {

  /// <summary>
  /// Defines a method for mutating genes.
  /// </summary>
  public interface IMutator : IGeneticOperator {
    Gene[] MakeMutation(Gene[] genes, double mutationProbabilityGene, IRandomNumberGenerator random);
  }
}
