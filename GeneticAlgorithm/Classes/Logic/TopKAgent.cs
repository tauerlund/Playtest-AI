using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  public class TopKAgent : IComparable<TopKAgent> {
    public Agent Agent { get; }
    public bool IsNewScore { get; set; } // Is true for all new instances. Can be set to false from outside. 

    /// <summary>
    /// Initializes a new instance of the class <see cref="TopKAgent"/>
    /// containing the given agent and set the <see cref="IsNewScore"/> to <see cref="true"/>.
    /// </summary>
    /// <param name="agent"></param>
    public TopKAgent(Agent agent) {
      Agent = agent;
      IsNewScore = true;
    }

    public int CompareTo(TopKAgent other) {
      return this.Agent.CompareTo(other.Agent);
    }
  }
}
