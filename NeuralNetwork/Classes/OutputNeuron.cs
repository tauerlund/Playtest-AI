using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]
  public class OutputNeuron : Neuron {
    public int Label { get; set; }
    public OutputNeuron(int label) : base() {
      this.Label = label; 
    }
  }
}
