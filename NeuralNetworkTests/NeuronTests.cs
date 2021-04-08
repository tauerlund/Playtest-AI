using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkNS;

namespace NeuralNetworkTests {
  /// <summary>
  /// Summary description for NeuronTests
  /// </summary>
  [TestClass]
  public class NeuronTests {

    [TestMethod]
    public void TestIfNeuronCalculatesCorrectOutput() {

      double weight1 = 0.5;
      double weight2 = 2.5;
      Dendrite dendrite1 = new Dendrite(weight1);
      Dendrite dendrite2 = new Dendrite(weight2);

      // Give input to inputneurons
      InputNeuron n1 = new InputNeuron();
      InputNeuron n2 = new InputNeuron();
      n1.Output = 5;
      n2.Output = 2;

      dendrite1.CalculateValue(n1.Output);
      dendrite2.CalculateValue(n2.Output);
        
      // Make hidden neuron, assign dendrites
      HiddenNeuron n3 = new HiddenNeuron();
      n3.Dendrites.Add(dendrite1);
      n3.Dendrites.Add(dendrite2);
      n3.CalculateOutput();

      double expectedValue = 7.5;
      double actualValue = n3.Output;

      Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void TestIfInputNeuronAssignsCorrectOutputValue() {
      InputNeuron n1 = new InputNeuron();
      n1.Output = 5;

      double expectedValue = 5;
      double actualValue = n1.Output;

      Assert.AreEqual(expectedValue, actualValue);
    }


  }
}
