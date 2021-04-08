using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {

  /// <summary>
  /// Calculates the similarity of the agents in the population.
  /// </summary>
  public class ConvergenceCalculator {
    private List<Agent> agents;

    public ConvergenceCalculator(List<Agent> agents) {
      this.agents = agents;
    }

    /// <summary>
    /// Calculates the convergence percent in the population. 
    /// </summary>
    /// <returns></returns>
    public double CalculateConvergence() {
      double totaluniformGenesPercentAll = 0;
      double averageUniformGenesPercentAll = 0;

      // Test convergance for all agents
      for(int i = 0; i < agents.Count; i++) {
        totaluniformGenesPercentAll += TestAgent(agents[i]);
      }

      // Calculate the how many genes the agents share with eachother on average. 
      averageUniformGenesPercentAll = totaluniformGenesPercentAll / agents.Count;
      return averageUniformGenesPercentAll;
    }

    // Test agent against other agents
    private double TestAgent(Agent agent) {
      double averageUniformGenesPercent = 0;
      double totaluniformGenesPercent = 0; 
      int uniformGeneCount = 0;
      double uniformGenesPercent = 0;

      for(int i = 0; i < agents.Count; i++) {
        if(agent != agents[i]) {

          // Calculate how many genes the agent shares with this agent. 
          uniformGeneCount = GetUniformGenesCount(agent, agents[i]);
          uniformGenesPercent = ((double)uniformGeneCount / agent.Chromosome.Genes.Length) * 100; 
          totaluniformGenesPercent += uniformGenesPercent;
        }
      }
      // Calculate how many genes the agent shares with other agents on average. 
      averageUniformGenesPercent = totaluniformGenesPercent / (agents.Count - 1); // -1 as should not unclude itself

      return averageUniformGenesPercent;
    }

    // Returns how many uniform genes the agents share.
    private int GetUniformGenesCount(Agent agent1, Agent agent2) {
      int uniformGeneCount = 0;

      Gene[] genes1 = agent1.Chromosome.Genes;
      Gene[] genes2 = agent2.Chromosome.Genes;
      int geneCount = genes1.Length;

      for(int i = 0; i < geneCount; i++) {
        if(genes1[i].Value == genes2[i].Value) {
          uniformGeneCount++;
        }
      }
      return uniformGeneCount;
    }
  }
}
