using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]
  public class InputLayer : Layer {

    /// <summary>
    /// Instantiates a new the input layer containing the given number of input neurons. 
    /// </summary>
    /// <param name="numberOfInputNeurons"></param>
    public InputLayer(int numberOfInputNeurons) {
      Neurons = new List<Neuron>();

      for (int i = 0; i < numberOfInputNeurons; i++) {
        Neurons.Add(new InputNeuron());
      }
      Neurons.Add(new BiasNeuron());
    }

    /// <summary>
    /// Assigns the given input values the input neurons of the layer. 
    /// </summary>
    /// <param name="inputValues"></param>
    public void Update(double[] inputValues) {

      for (int i = 0; i < inputValues.Length; i++) {
        Neurons[i].Output = inputValues[i];
      }
    }
  }
}
