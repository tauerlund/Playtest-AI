using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]
  public class Dendrite {
    private readonly double weight;
    public double Value { get; set; }
    /// <summary>
    /// Initializes a new instance of the class <see cref="Dendrite"/>
    /// with the given weight.
    /// </summary>
    /// <param name="weight"></param>
    public Dendrite (double weight) {
      this.weight = weight;
    }

    /// <summary>
    /// Calculates the value of the output neuron.
    /// </summary>
    /// <param name="neuronOutput"></param>
    public void CalculateValue(double neuronOutput){
      Value = weight * neuronOutput;
    }
  }
}
