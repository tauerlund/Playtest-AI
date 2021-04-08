using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]
  public class BiasNeuron : Neuron {
    public BiasNeuron () : base () {
      Output = NetworkConstants.BIAS_NEURON_VALUE;
    }
  }
}

