using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Contains a chromosome which represents a possible solution of the given problem. 
  /// Holds methods to calculate its own fitness and mutation.
  /// </summary>
  [Serializable]
  public class Agent : IComparable<Agent> {
    public int NumberID { get; set; } // Number in generation
    public int GenerationNumber { get; set; } // In which generation agent has been created
    public int UniqueID { get; private set; }
    public double Fitness { get; private set; }
    public IFitnessInfo FitnessInfo { get; private set; }
    public Chromosome Chromosome { get; private set; }
    // Statics
    public static int TotalAgentsMade { get; private set; } // Used to set uniqe ID of agent 
    public static int TotalFitnessCalculations { get; private set; } // Used to show how many times fitness has been calculated

    /// <summary>
    /// Initializes a new instance of the <see cref="Agent"/> class using
    /// the random generator to make an agent with a chromsome of random genes
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="geneCount"></param>
    /// <param name="generationNumber"></param>
    /// <param name="random"></param>
    public Agent(int ID, int geneCount, int generationNumber, IRandomNumberGenerator random) {
      NumberID = ID;
      GenerationNumber = generationNumber;
      UniqueID = TotalAgentsMade;
      Fitness = -1;
      Chromosome = new Chromosome(geneCount, random);
      TotalAgentsMade++;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Agent"/> class using 
    /// the given <see cref="GeneticAlgorithmNS.Chromosome"/>. 
    /// </summary>
    /// <param name="chromosome">The chromosome which will become a part of the <see cref="Agent"/>. </param>
    public Agent(Chromosome chromosome) {
      NumberID = -1;
      GenerationNumber = -1;
      UniqueID = TotalAgentsMade;
      Fitness = -1;
      Chromosome = chromosome;
      TotalAgentsMade++;
    }

    /// <summary>
    /// Returns a deep copy of the agent.
    /// </summary>
    /// <returns></returns>
    public Agent GetCopy() {

      Agent agentCopy;
      MemoryStream memoryStream = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();

      using(memoryStream) {
        // Serialize this agent into the memory stream.
        binaryFormatter.Serialize(memoryStream, this);
        // Set the position in memory stream to the beginning.
        memoryStream.Seek(0, SeekOrigin.Begin);
        // Dezerialize the memory stream into the agent copy. 
        agentCopy = (Agent)binaryFormatter.Deserialize(memoryStream);
        memoryStream.Close();
      }
      return agentCopy;
    }

    public int CompareTo(Agent other) {
      return -this.Fitness.CompareTo(other.Fitness);
    }

    /// <summary>
    /// Calculates the fitness of the agent using the given fitnessCalculator. 
    /// </summary>
    /// <param name="fitnessCalculator"></param>
    public void CalculateFitness(IFitnessCalculator fitnessCalculator) {
      TotalFitnessCalculations++;
      FitnessInfo = fitnessCalculator.CalculateFitness(Chromosome.ToArray());
      Fitness = FitnessInfo.Fitness; // Assign fitness directly also, so direct access from outside. 
    }

    /// <summary>
    /// Mutates the chromsome of the agent using the given mutator.
    /// </summary>
    /// <param name="mutator"></param>
    /// <param name="mutationProbabilityGene"></param>
    /// <param name="random"></param>
    public void Mutate(IMutator mutator, double mutationProbabilityGene, IRandomNumberGenerator random) {
      Chromosome.Mutate(mutator, mutationProbabilityGene, random);
    }
  }
}
