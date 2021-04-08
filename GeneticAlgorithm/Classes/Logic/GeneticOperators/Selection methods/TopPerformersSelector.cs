using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Represents a selection method that selects only the top performers.
  /// </summary>
  [Serializable]
  public class TopPerformersSelector : ISelector {

    /// <summary>
    /// Returns a list of the best agents from the given agents. 
    /// </summary>
    /// <param name="agents"></param>
    /// <param name="selectionSize"></param>
    /// <param name="random"></param>
    /// <returns></returns>
    public List<Agent> MakeSelection(List<Agent> agents, int selectionSize, IRandomNumberGenerator random) {
      List<Agent> selectedAgents = new List<Agent>();
      // Sort list descending by fitness 
      agents.Sort(); // pass sorter??
      // Fill list with top performers
      for(int i = 0; i < selectionSize; i++) {
        selectedAgents.Add(agents[i]);
      }
      return selectedAgents;
    }

    public override string ToString() {
      return "Top Performers";
    }
  }
}
