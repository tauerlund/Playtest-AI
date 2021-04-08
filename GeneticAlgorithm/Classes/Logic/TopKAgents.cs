using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Contains a list of the K best agents, where K is the number of agents in the list. 
  /// </summary>
  [Serializable]
  public class TopKAgents {
    public TopKAgent Best { get; private set; }
    public List<TopKAgent> BestAgents { get; private set; }
    public double AverageFitness { get; private set; }

    private int K; // The number of agents in the list. 

      /// <summary>
      /// Returns the index of the top list.
      /// </summary>
      /// <param name="i"></param>
      /// <returns></returns>
    public TopKAgent this[int i] {
      get { return BestAgents[i]; }
      private set { BestAgents[i] = value; }
    }

    /// <summary>
    /// Initializes a new instance of the class <see cref="TopKAgents"/> setting
    /// the specifed k parameter as the max number of agents in the Top K Agents list.
    /// </summary>
    /// <param name="k"></param>
    public TopKAgents(int k) {
      K = k;
      BestAgents = new List<TopKAgent>();
    }
    /// <summary>
    ///  Updates the current TopKAgents with any new highscores in the passed agents. 
    /// </summary>
    public void Update(List<Agent> agents) {
      AddAnyTopAgents(agents);
      BestAgents.Sort();
      // If highscore list is filled, remove lowest score
      if(BestAgents.Count > K) {
        BestAgents.RemoveRange(K, BestAgents.Count - K);
      }
      // Calculate average.
      AverageFitness = BestAgents.Average(b => b.Agent.Fitness);
      // Assign best.
      Best = BestAgents.First();
    }

    // Goes through the passed agents and add agents who's fitness value exceeds the current top agents.
    private void AddAnyTopAgents(List<Agent> agents) {
      if(BestAgents.Count == 0) { // Check if list is empty
        BestAgents.Add(new TopKAgent(agents.First()));
      }
      for(int i = 0; i < agents.Count; i++) {
        // Check if new highscore.
        if(agents[i].Fitness > BestAgents.Last().Agent.Fitness) {
          BestAgents.Add(new TopKAgent(agents[i].GetCopy()));
        }
      }
    }
  }
}
