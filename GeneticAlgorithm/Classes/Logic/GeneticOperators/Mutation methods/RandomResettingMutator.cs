using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Represents a mutation method where every gene is randomized if below the specified probability. 
  /// </summary>
  [Serializable]
  public class RandomResettingMutator : IMutator {
    /// <summary>
    /// Goes through every gene and randomize the gene if probabilty hit. 
    /// </summary>
    /// <param name="genes"></param>
    /// <param name="mutationProbabilityGene"></param>
    /// <param name="random"></param>
    /// <returns></returns>
    public Gene[] MakeMutation(Gene[] genes, double mutationProbabilityGene, IRandomNumberGenerator random) { 
      for(int i = 0; i < genes.Length; i++) {
        // Check if this gene should be mutated
        double randomDouble = random.GetDouble(0, 1); 
        if(randomDouble < mutationProbabilityGene) {
          // Overwrite with new random gene
          genes[i] = new Gene(random);
        }
      }
      return genes;
    } 

    public override string ToString() {
      return "Random Resetting";
    }
  }
}
