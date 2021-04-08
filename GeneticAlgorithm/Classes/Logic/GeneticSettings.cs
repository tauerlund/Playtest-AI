using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Contains the settings of the genetic algorithm. Is immutable. 
  /// </summary>
  [Serializable]
  public class GeneticSettings {

    public int GeneCount { get; private set; }
    public int PopulationSize { get; private set; }
    public int SelectionSize { get; private set; }

    public double MutationProbabiltyAgents { get; private set; }
    public double MutationProbabilityGenes { get; private set; }

    public IRandomNumberGenerator RandomNumberGenerator { get; private set; }
    public IFitnessCalculator FitnessCalculator { get; }
    public IMutator Mutator { get; }
    public ISelector Selector { get; private set; }
    public ICrossover Crossover { get; private set; }
    public IAgentViewer AgentViewer { get; private set; }

    public int TopKAgentsCount { get; }

    public GeneticSettings(int populationSize,
                           int geneCount,
                           int selectionSize,
                           double mutationProbabiltyAgents,
                           double mutationProbabilityGenes,
                           IRandomNumberGenerator randomNumberGenerator,
                           IFitnessCalculator fitnessCalculator,
                           IMutator mutator,
                           ISelector selector,
                           ICrossover crossover,
                           IAgentViewer agentViewer,
                           int topKAgentsCount) {

      if(populationSize < selectionSize) {
        throw new Exception("The population size has to be greater than the selection size");
      }

      PopulationSize = populationSize;
      GeneCount = geneCount;
      SelectionSize = selectionSize;

      MutationProbabiltyAgents = mutationProbabiltyAgents;
      MutationProbabilityGenes = mutationProbabilityGenes;

      RandomNumberGenerator = randomNumberGenerator;
      FitnessCalculator = fitnessCalculator;
      Mutator = mutator;
      Selector = selector;
      Crossover = crossover;

      AgentViewer = agentViewer; 

      TopKAgentsCount = topKAgentsCount;
    }

    public void ChangeMutationProbablityGene(double probability) {
      MutationProbabilityGenes = probability;
    }

    public void UpdateGeneticSettings(int geneCount,
                                      int populationSize,
                                      int selectionSize,
                                      double mutationProbabilityAgents,
                                      double mutationProbabilityGenes,
                                      ISelector selectionMethod,
                                      ICrossover crossoverMethod
                                      ) {
      GeneCount = geneCount;
      PopulationSize = populationSize;
      SelectionSize = selectionSize;
      MutationProbabiltyAgents = mutationProbabilityAgents;
      MutationProbabilityGenes = mutationProbabilityGenes;
      Selector = selectionMethod;
      Crossover = crossoverMethod;
    }

  }
}
