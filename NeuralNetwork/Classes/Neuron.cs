  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkNS {
  [Serializable]
  public abstract class Neuron : IComparable<Neuron> {

    public List<Dendrite> Dendrites { get; set; }
    public double Output { get; set; }

    /// <summary>
    /// Initializes a new instance of the class <see cref="Neuron"/>.
    /// </summary>
    public Neuron() {
      Dendrites = new List<Dendrite>();
    }

    /// <summary>
    /// Calculates the output value of the neuron.
    /// </summary>
    public void CalculateOutput() {
      double dendriteSum = 0;
      double neuronValue;
      // Sum dendrite value for all dendrites in list.
      foreach (Dendrite d in Dendrites) {
        dendriteSum += d.Value;
      }
      neuronValue = dendriteSum;
      this.Output = ActivationMethod(neuronValue);
    }

    // Activation method, ReLU function
    private double ActivationMethod(double neuronValue) {
      return Math.Max(0, neuronValue);
    }
    // CompareTo used for finding max output neuron
    public int CompareTo(Neuron other) {
      return this.Output.CompareTo(other.Output);
    }
  }
}
