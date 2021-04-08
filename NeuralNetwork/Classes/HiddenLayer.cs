using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]
  public class HiddenLayer : Layer {

    /// <summary>
    /// Instanties a hidden layer with the given number of neurons.
    /// </summary>
    /// <param name="numberOfNeurons"></param>
    public HiddenLayer(int numberOfNeurons) {
      Neurons = new List<Neuron>();

      for(int i = 0; i < numberOfNeurons; i++) {
        Neurons.Add(new HiddenNeuron());
      }
      Neurons.Add(new BiasNeuron());
    }
  }
}
