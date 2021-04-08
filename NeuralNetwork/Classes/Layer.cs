using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]
  public abstract class Layer {
    public List<Neuron> Neurons { get; set; }

    /// <summary>
    /// Create dendrites for all neurons in NEXT layer and pass weights to dendrites.
    /// </summary>
    /// <param name="nextLayer"></param>
    /// <param name="biasNeuronCount"></param>
    /// <param name="weights"></param>
    /// <param name="weightsCounter"></param>
    public void AddDendritesToNextLayer(Layer nextLayer, int biasNeuronCount, double[] weights, ref int weightsCounter) {
      // For every neuron in THIS layer...
      for (int i = 0; i < Neurons.Count; i++) {
        // Add dendrite to every neuron in NEXT layer
        for (int j = 0; j < nextLayer.Neurons.Count - biasNeuronCount; j++) {
          nextLayer.Neurons[j].Dendrites.Add(new Dendrite(weights[weightsCounter]));
          weightsCounter++; // Count op index of weight list (NB: counter is a pointer)
        }
      }
    }

    /// <summary>
    /// Send output of every neuron in THIS layer to the dendrite list of every neuron in NEXT layer.
    /// </summary>
    /// <param name="nextLayer"></param>
    /// <param name="biasNeuronCount"></param>
    public void SendOutputToNextLayer(Layer nextLayer, int biasNeuronCount) {
      // For every neuron in THIS layer...
      for (int i = 0; i < Neurons.Count; i++) {
        // Go through every neuron in NEXT layer and add a dendrite. Pass output of current neuron in THIS layer
        for (int j = 0; j < nextLayer.Neurons.Count - biasNeuronCount; j++) {
          nextLayer.Neurons[j].Dendrites[i].CalculateValue(Neurons[i].Output);
          // If currently at last neuron in THIS layer: Calculate output value for next layer neuron
          if(Neurons[i].Equals(Neurons.Last())) {  
            nextLayer.Neurons[j].CalculateOutput();
          }
        }
      }
    }
  }
}