using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkNS;

namespace NeuralNetworkTests {
  /// <summary>
  /// Summary description for OutputLayerTests
  /// </summary>
  /// 
  class OutputNeuronListComparer : Comparer<OutputNeuron> {
    public override int Compare(OutputNeuron a, OutputNeuron b) {
      return a.Label.CompareTo(b.Label);
    }
  }
  
  [TestClass]
  public class OutputLayerTests {

    int[] outputLabels;
    OutputLayer outputLayer;

    [TestInitialize]
    public void TestInitialize() {

      outputLabels = new int[] { 1, 2 };
      outputLayer = new OutputLayer(outputLabels);
    }

    [TestMethod]
    public void OutputLayer_InstantiatedWithOutputLabels_MakesCorrectLayer() {

      OutputNeuron n1 = new OutputNeuron(1);
      OutputNeuron n2 = new OutputNeuron(2);

      List<Neuron> expectedList = new List<Neuron> { n1, n2 };

      List<Neuron> actualList = outputLayer.Neurons;

      CollectionAssert.AreEqual(expectedList, actualList, new OutputNeuronListComparer());

    }


  }
}
