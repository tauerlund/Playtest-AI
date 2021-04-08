using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Represents the essential information of a saved agent. 
  /// </summary>
  [Serializable]
  public class SavedAgent {

    public Agent agent { get; }
    public GeneticSettings geneticSettings { get; }

    /// <summary>
    /// Represents a agent that can be saved.
    /// </summary>
    /// <param name="agentToSave"></param>
    /// <param name="geneticSettingsToSave"></param>
    public SavedAgent(Agent agentToSave, GeneticSettings geneticSettingsToSave) {
      agent = agentToSave;
      geneticSettings = geneticSettingsToSave;
    }
  }
}
