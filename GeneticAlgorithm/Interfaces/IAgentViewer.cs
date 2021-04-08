using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Defines a method to view an agent. 
  /// </summary>
  public interface IAgentViewer {
    void ViewAgent(Agent agent, GeneticSettings geneticSettings);
  }
}
