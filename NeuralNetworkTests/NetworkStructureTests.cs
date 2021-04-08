using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkNS;

namespace NeuralNetworkTests {
  /// <summary>
  /// Summary description for NetworkStructureTests
  /// </summary>
  [TestClass]
  public class NetworkStructureTests {
    int inputStructure;
    int[] hiddenStructure;
    int[] outputStructure;
    NetworkSettings networkSettings;

    double[] weights;

    //
    [TestInitialize]
    public void TestInitialize() {

      inputStructure = 2;
      hiddenStructure = new int[] { 3 };
      outputStructure = new int[] { 1, 2 };

      weights = new double[17];
      networkSettings = new NetworkSettings(inputStructure, hiddenStructure, outputStructure);

      weights[0] = -1.5;
      weights[1] = 2;
      weights[2] = 5.5;
      weights[3] = 2;
      weights[4] = -3.5;
      weights[5] = 1;
      weights[6] = 2.5;
      weights[7] = -3;
      weights[8] = 2.5;
      weights[9] = 1.5;
      weights[10] = 2.5;
      weights[11] = 3;
      weights[12] = -1.5;
      weights[13] = 1;
      weights[14] = -2;
      weights[15] = 5.5;
      weights[16] = -2;
    }

    [TestMethod]
    public void GetOutput_Condition_ReturnsCorrectOutputValue() {

      double[] inputValues = { 5, 2 };

      NetworkStructure networkStructure = new NetworkStructure(networkSettings, weights);
      networkStructure.UpdateInput(inputValues);
      networkStructure.MakeOutput();
      OutputNeuron outputNeuron = networkStructure.GetOutput();

      double expectedResult = 37.5;
      double actualResult = outputNeuron.Output;

      Assert.AreEqual(expectedResult, actualResult);
    }
  }
}
