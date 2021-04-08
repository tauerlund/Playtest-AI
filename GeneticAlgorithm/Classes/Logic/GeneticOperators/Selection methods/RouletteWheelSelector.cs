using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Represents a selection method that selects the agents using the Roulette wheel selection method.
  /// </summary>
  [Serializable]
  public class RouletteWheelSelector : ISelector {

    /// <summary>
    /// Returns selected agents based on the Roulette wheel selection method. 
    /// </summary>
    /// <param name="agents"></param>
    /// <param name="selectionSize"></param>
    /// <param name="random"></param>
    /// <returns></returns>
    public List<Agent> MakeSelection(List<Agent> agents, int selectionSize, IRandomNumberGenerator random) {
      double totalFitness = 0;
      List<double> agentProbabilities = new List<double>();
      List<Agent> selectedAgents = new List<Agent>();

      // Get total fitnes
      for(int i = 0; i < agents.Count; i++) {
        // Sum fitness values
        totalFitness += agents[i].Fitness;
      }

      // Make agent probabilities
      for(int i = 0; i < agents.Count; i++) {
        agentProbabilities.Add(agents[i].Fitness / totalFitness);
      }

      double randomNumber = 0;
      double total = 0;
      int counter = 0;
      bool agentFound = false;

      for(int i = 0; i < selectionSize; i++) {
        // Get new random number
        randomNumber = random.GetDouble(0, 1);
        // Find angent match
        for(int j = 0; j < agents.Count && !agentFound; j++) {
          total += agentProbabilities[j];
          if(total >= randomNumber) {
          selectedAgents.Add(agents[j]);
            agentFound = true;
          }
        }
        Debug.WriteLine(++counter);

        total = 0;
        agentFound = false;
      }

      return selectedAgents;
    }

    public override string ToString() {
      return "Roulette Wheel";
    }
  }
}

