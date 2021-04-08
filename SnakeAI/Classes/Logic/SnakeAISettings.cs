using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithmNS;
using NeuralNetworkNS;
using SnakeGameNS;

namespace SnakeAI {
  /// <summary>
  /// Contains all settings used for Snake AI.
  /// </summary>
  public class SnakeAISettings {

    public GeneticSettings GeneticSettings { get; private set; }
    public NetworkSettings NetworkSettings { get; private set; }
    public SnakeSettings SnakeSettings { get; private set; }
    
    public SnakeAISettings(GeneticSettings geneticSettings, NetworkSettings networkSettings, SnakeSettings snakeSettings) {
      GeneticSettings = geneticSettings;
      NetworkSettings = networkSettings;
      SnakeSettings = snakeSettings;
    }

    public void UpdateGeneticSettings(GeneticSettings geneticSettings) {
      GeneticSettings = geneticSettings;
    }

    public void UpdateNetworkSettings(NetworkSettings networkSettings) {
      NetworkSettings = networkSettings;
    }

    public void UpdateSnakeSettings(SnakeSettings snakeSettings) {
      SnakeSettings = snakeSettings;
    }
  }
}
