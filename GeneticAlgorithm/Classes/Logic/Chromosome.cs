using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Contains an array of genes representing af chromosome. 
  /// </summary>
  [Serializable]
  public class Chromosome {
    public Gene[] Genes { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Chromosome"/> class containing random
    /// genes using the specified <see cref="IRandomNumberGenerator"/>. 
    /// </summary>
    /// <param name="geneCount">The number of genes the chromosome should contain.</param>
    /// <param name="random">The class that should be used to create a create a random <see cref="Gene"/></param>
    public Chromosome(int geneCount, IRandomNumberGenerator random) {
      Genes = new Gene[geneCount];
      MakeGenes(geneCount, random);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Chromosome"/> class using the
    /// given genes.
    /// </summary>
    /// <param name="genes">The genes that will become a part of the <see cref="Chromosome"/>.</param>
    public Chromosome(Gene[] genes) {
      Genes = genes;
    }
    
    // Makes random genes
    private void MakeGenes(int geneCount, IRandomNumberGenerator random) {
      for(int i = 0; i < geneCount; i++) {
        Genes[i] = new Gene(random);
      }
    }

    /// <summary>
    /// Mutates the genes in the <see cref="Chromosome"/> by using the 
    /// specified <see cref="IMutator"/>. 
    /// </summary>
    /// <param name="mutator"></param>
    /// <param name="mutationProbabilityGene"></param>
    /// <param name="random"></param>
    public void Mutate(IMutator mutator, double mutationProbabilityGene, IRandomNumberGenerator random) {
      Genes = mutator.MakeMutation(Genes, mutationProbabilityGene, random);
    }

    /// <summary>
    /// Returns the genes of the <see cref="Chromosome"/> to an array.
    /// </summary>
    /// <returns></returns>
    public double[] ToArray() {
      double[] array = new double[Genes.Length];
      for(int i = 0; i < Genes.Length; i++) {
        array[i] = Genes[i].Value;
      }
      return array;
    }
  }
}
