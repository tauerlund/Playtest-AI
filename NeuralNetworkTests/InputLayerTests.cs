using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkNS;

namespace NeuralNetworkTests {
  /// <summary>
  /// Summary description for InputLayerTests
  /// </summary>
  [TestClass]

  class NeuronListComparer : Comparer<Neuron> {
    public override int Compare(Neuron a, Neuron b) {
      return a.Output.CompareTo(b.Output);
    }
  }

  [TestClass]
  public class InputLayerTests {

    int[] input;
    InputLayer inputLayer;


    [TestInitialize]
    public void TestInitialize() {
     
      input =  new int[] { 5, 2 };
      inputLayer = new InputLayer(input.Length);
    }

    [TestMethod]
    public void InputLayer_InstantiatedWithInputAndBias_MakesCorrectLayer() {

      InputNeuron n1 = new InputNeuron();
      InputNeuron n2 = new InputNeuron();
      BiasNeuron b = new BiasNeuron();

      List<Neuron> expectedList = new List<Neuron> { n1, n2, b };
      List<Neuron> actualList = inputLayer.Neurons;

      CollectionAssert.AreEqual(expectedList, actualList, new NeuronListComparer());
    }

    public void Update_CalledWithInputValues_UpdatesWithCorrectValues() {

      InputNeuron n1 = new InputNeuron();
      InputNeuron n2 = new InputNeuron();
      BiasNeuron b = new BiasNeuron();

      List<Neuron> expectedList = new List<Neuron> { n1, n2, b };

      double[] newInput = { 8, 10 };

      inputLayer.Update(newInput);
      List<Neuron> actualList = inputLayer.Neurons;

      CollectionAssert.AreEqual(expectedList, actualList, new NeuronListComparer());
    }


  }
}
