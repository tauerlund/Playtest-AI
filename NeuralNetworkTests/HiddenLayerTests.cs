using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkNS;

namespace NeuralNetworkTests {
  /// <summary>
  /// Summary description for HiddenLayerTests
  /// </summary>
  [TestClass]
  public class HiddenLayerTests {

    int numberOfNeurons;
    HiddenLayer hiddenLayer;
    
    [TestInitialize]
    public void TestInitialize() {

      numberOfNeurons = 3;
      hiddenLayer = new HiddenLayer(numberOfNeurons);
    }

    [TestMethod]
    public void HiddenLayer_InstantiedWithNumberOfNeurons_MakesCorrectNumberOfNeurons() {
      HiddenNeuron n1 = new HiddenNeuron();
      HiddenNeuron n2 = new HiddenNeuron();
      HiddenNeuron n3 = new HiddenNeuron();
      BiasNeuron b = new BiasNeuron();
      
      List<Neuron> l = new List<Neuron> { n1, n2, n3, b };

      int expectedValue = l.Count;
      
      int actualValue = hiddenLayer.Neurons.Count;

      Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void HiddenLayer_InstantiedWithNumberOfNeurons_MakesCorrectBias() {
      int last = hiddenLayer.Neurons.Count-1;

      Neuron n = hiddenLayer.Neurons[last];

      Assert.IsTrue(n.GetType() == typeof(BiasNeuron));
    }

  }
}
