using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;



namespace GeneticAlgorithmNS {
  /// <summary>
  /// Represents a crossover method that combines the chromosome of two parents into one chromosome.
  /// </summary>
  [Serializable]
  public abstract class OnePointCombineCrossover {
    /// <summary>
    /// Makes children out of passed agents by randomly selecting two parents for crossover. 
    /// The process repeats until the amount of children generated equals the specified populationSize.
    /// </summary>
    /// <param name="parentAgents"> The agents that should be crossed.</param>
    /// <param name="populationSize">The size of the returned list of children. </param>
    /// <param name="random">The random number generator that will be used to choose random parents and a random crossoverpoint.</param>
    /// <returns></returns>
    protected List<Agent> MakeOnePointCombineCrossovers(List<Agent> parentAgents, int populationSize, IRandomNumberGenerator random) {
      Agent child;
      int randomParent1Index;
      int randomParent2Index;
      List<Agent> children = new List<Agent>();
        
      for(int i = 0; i < populationSize; i++) {
        // Get random parents from list of agents. 
        randomParent1Index = random.GetInt(0, parentAgents.Count);
        randomParent2Index = random.GetInt(0, parentAgents.Count);

        // Make and add child to children to return
        child = MakeCrossover(parentAgents[randomParent1Index], parentAgents[randomParent2Index], random);
        children.Add(child);
      }
      return children;
    }


    // Makes a child agent by randomly selecting an crossover point where the chromsomes of the two 
    // passed agents parents will be combined. 
    private Agent MakeCrossover(Agent parent1, Agent parent2, IRandomNumberGenerator random) {
      Agent child;
      // Get random crossover point.
      int crossoverPoint = random.GetInt(1, parent1.Chromosome.Genes.Length);
      // Make child to return. 
      child = new Agent(GetCombinedChromosome(parent1.Chromosome, parent2.Chromosome, crossoverPoint));
      return child;
    }

    /// <summary>
    /// Combines two chromosomes based on the specified crossoverPoint.
    /// </summary>
    private Chromosome GetCombinedChromosome(Chromosome chromosome1, Chromosome chromosome2, int crossoverPoint) {
      int geneCount = chromosome1.Genes.Length;
      Gene[] childGenes = new Gene[geneCount];

      Array.Copy(chromosome1.Genes, childGenes, crossoverPoint);
      Array.Copy(chromosome2.Genes, crossoverPoint, childGenes, crossoverPoint,
                 geneCount - crossoverPoint);

      Chromosome combinedChromosome = new Chromosome(childGenes);
      return combinedChromosome;
    }
  }
}
