using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]

  public class OutputLayer : Layer {
    
    /// <summary>
    /// Initilizes a new instance of the class <see cref="OutputLayer"/>
    /// with the given outputLabels. 
    /// </summary>
    /// <param name="outputLabels"></param>
    public OutputLayer(int[] outputLabels) {
      Neurons = new List<Neuron>();

      for(int i = 0; i < outputLabels.Length; i++) {
        Neurons.Add(new OutputNeuron(outputLabels[i]));
      }
    }

    /// <summary>
    /// Returns the output neuron with the maximum value. 
    /// </summary>
    /// <returns></returns>
    public OutputNeuron GetMaxOutputNeuron() {
      return Neurons.Max() as OutputNeuron;
    }
  }
}
