using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  // Internal constants. Will never change.
  [Serializable]
  public static class NetworkConstants {
    // The value of a bias neuron
    public const int BIAS_NEURON_VALUE = 1;
    // One bias neuron in input layer and hidden layers
    public const int BIAS_NEURON_COUNT_REGULAR = 1;
    // No bias neuron in output layer
    public const int BIAS_NEURON_COUNT_OUTPUT_LAYER = 0;
  }
}
