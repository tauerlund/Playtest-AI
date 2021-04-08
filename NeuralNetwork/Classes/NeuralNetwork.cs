using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetworkNS {
  /// <summary>
  /// Represents a neural network. 
  /// </summary>
  [Serializable]
  public class NeuralNetwork {
    private readonly NetworkStructure networkStructure;

    public NeuralNetwork(NetworkSettings networkSettings, double[] weights) {
      networkStructure = new NetworkStructure(networkSettings, weights);
    }

    /// <summary>
    /// Calculates the output of the neural netowrk based on the given input values.
    /// </summary>
    /// <param name="inputValues">The input values to the neural network.</param>
    /// <returns></returns>
    public int CalculateOutput(double[] inputValues) {
      networkStructure.UpdateInput(inputValues);
      networkStructure.MakeOutput();

      OutputNeuron OutputNeuron = networkStructure.GetOutput();

      return OutputNeuron.Label;
    }

    /// <summary>
    /// Returns dictionary with label and value for output layer.
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, double> GetOutputValues() {
      return networkStructure.GetOutputValues();
    }
  }
}
