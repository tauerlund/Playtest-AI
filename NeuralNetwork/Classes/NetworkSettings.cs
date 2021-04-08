using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  // Settings passed to neural network
  [Serializable]
  public class NetworkSettings {

    public int numberOfWeights { get; private set; }
    public int numberOfInputNeurons { get; private set; }
    public int[] hiddenLayerStructure { get; private set; } // Structure of the network, how many neurons in each hidden layer
    public int numberOfOutputNeurons { get; private set; }
    public int[] outputLabels { get; private set; }

    /// <summary>
    /// Instanties the networksetting containing the given information about the neural network.
    /// </summary>
    /// <param name="numberOfInputNeurons"></param>
    /// <param name="hiddenLayerStructure"></param>
    /// <param name="outputLabels"></param>
    public NetworkSettings(int numberOfInputNeurons, int[] hiddenLayerStructure, int[] outputLabels) {
      this.numberOfInputNeurons = numberOfInputNeurons;
      this.hiddenLayerStructure = hiddenLayerStructure;
      this.numberOfOutputNeurons = outputLabels.Length; 
      this.outputLabels = outputLabels; 
      numberOfWeights = CalculateNumberOfWeights();
    }

    /// <summary>
    /// Calculates the number of weights based on the size of the neural network.
    /// </summary>
    /// <returns></returns>
    public int CalculateNumberOfWeights() {

      int totalNumberOfWeights = 0;

      // Make list of number of neurons in each layer
      List<int> totalNumberOfNeurons = new List<int>();
      totalNumberOfNeurons.Add(numberOfInputNeurons);
      totalNumberOfNeurons.AddRange(hiddenLayerStructure);
      totalNumberOfNeurons.Add(numberOfOutputNeurons);

      for (int i = 0; i < totalNumberOfNeurons.Count - 1; i++) {
        totalNumberOfNeurons[i]++;
      }

      int biasNeuronCount = 1;
      for(int i = 0; i < totalNumberOfNeurons.Count - 1; i++) { 
        if(totalNumberOfNeurons[i + 1] == totalNumberOfNeurons.Last()) {
          biasNeuronCount = 0;
        }
        totalNumberOfWeights += totalNumberOfNeurons[i] * (totalNumberOfNeurons[i + 1] - biasNeuronCount);
      }
      return totalNumberOfWeights;
    }

    /// <summary>
    /// Updates the settings with new hidden layer structure.
    /// </summary>
    /// <param name="numberOfHiddenNeurons"></param>
    public void UpdateNetworkSettings(int[] numberOfHiddenNeurons) {
      UpdateNetworkSettings(numberOfInputNeurons, numberOfHiddenNeurons, outputLabels);
    }

    /// <summary>
    /// Updates the settings with the given information.
    /// </summary>
    /// <param name="numberOfInputNeurons"></param>
    /// <param name="numberOfHiddenNeurons"></param>
    /// <param name="outputLabels"></param>
    public void UpdateNetworkSettings(int numberOfInputNeurons, 
                                      int[] numberOfHiddenNeurons, 
                                      int[] outputLabels) {
      this.numberOfInputNeurons = numberOfInputNeurons;
      this.hiddenLayerStructure = numberOfHiddenNeurons;
      this.outputLabels = outputLabels;
      this.numberOfOutputNeurons = outputLabels.Length;
      numberOfWeights = CalculateNumberOfWeights();
    }
  }
} 
