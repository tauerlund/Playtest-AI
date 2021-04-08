using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  /// <summary>
  /// Represents the structure of layers in the neural network. 
  /// </summary>
  [Serializable]
  public class NetworkStructure {

    private List<Layer> Layers;
    private InputLayer InputLayer;
    private List<HiddenLayer> HiddenLayers;
    private OutputLayer OutputLayer;
    private int weightsCounter;
    /// <summary>
    /// Creates a network structure from the given settings and weights.
    /// </summary>
    /// <param name="networkSettings"></param>
    /// <param name="weights"></param>
    public NetworkStructure(NetworkSettings networkSettings, double[] weights) {
      CreateLayers(networkSettings.numberOfInputNeurons, networkSettings.outputLabels, networkSettings.hiddenLayerStructure);
      CreateDendrites(weights);
    }

    // Creates layers and create neurons in layers
    private void CreateLayers(int numberOfInputNeurons, int[] outputLabels, int[] hiddenLayerStructure) {
      int numberOfHiddenLayers = hiddenLayerStructure.Length;
      // Make input layer
      this.InputLayer = new InputLayer(numberOfInputNeurons);
      // Make hidden layers
      this.HiddenLayers = new List<HiddenLayer>();
      for (int i = 0; i < numberOfHiddenLayers; i++) {
        HiddenLayers.Add(new HiddenLayer(hiddenLayerStructure[i]));
      }
      // Make output layer
      this.OutputLayer = new OutputLayer(outputLabels);

      // Add all layers to list of layers
      List<Layer> layers = new List<Layer>();
      layers.Add(InputLayer);
      layers.AddRange(HiddenLayers);
      layers.Add(OutputLayer);

      Layers = layers;
    }

    // Creates dendrites for neurons in next layer
    private void CreateDendrites(double[] weights) {
      int biasNeuronCount = NetworkConstants.BIAS_NEURON_COUNT_REGULAR;
      for (int i = 0; i < Layers.Count - 1; i++) { // Loop through all layers but the last.
        // If next layer is last, it is output layer -> no bias neuron.
        if (Layers[i + 1] == Layers.Last()) {
          biasNeuronCount = NetworkConstants.BIAS_NEURON_COUNT_OUTPUT_LAYER;
        }
        Layers[i].AddDendritesToNextLayer(Layers[i + 1], biasNeuronCount, weights, ref weightsCounter);
      }
    }

    /// <summary>
    /// Calculates the output of the neural network.
    /// </summary>
    public void MakeOutput() {
      int biasNeuronCount = NetworkConstants.BIAS_NEURON_COUNT_REGULAR;
      for(int i = 0; i < Layers.Count - 1; i++) {
        if(Layers[i + 1] == Layers.Last()) {
          biasNeuronCount = NetworkConstants.BIAS_NEURON_COUNT_OUTPUT_LAYER;
        }
        Layers[i].SendOutputToNextLayer(Layers[i + 1], biasNeuronCount);
      }
    }

    /// <summary>
    /// Updates the input layer with the given values.
    /// </summary>
    /// <param name="inputValues"></param>
    public void UpdateInput(double[] inputValues) {
      InputLayer.Update(inputValues);
    }

    /// <summary>
    /// Gets the output neuron with the greatest value.
    /// </summary>
    /// <returns></returns>
    public OutputNeuron GetOutput() {
      return OutputLayer.GetMaxOutputNeuron();
    }

    /// <summary>
    ///  Returns dictionary with label and value for output layer.
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, double> GetOutputValues() {
      Dictionary<int, double> OutputValues = new Dictionary<int, double>();

      for(int i = 0; i < OutputLayer.Neurons.Count; i++) {
        OutputValues.Add(((OutputNeuron)OutputLayer.Neurons[i]).Label, OutputLayer.Neurons[i].Output);
      }
      return OutputValues;
    }
  }
}
