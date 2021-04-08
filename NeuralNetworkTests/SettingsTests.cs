using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkNS;

namespace NeuralNetworkTests {
  [TestClass]
  public class SettingsTests {

    [TestMethod]
    public void TestIfCalculateNumberOfWeightsReturnsCorrectValue() {
      int expectedResult = 17;

      int inputStructure = 2;
      int[] hiddenStructure = { 3 };
      int[] outputStructure = {1, 2};

      NetworkSettings networkSettings = new NetworkSettings(inputStructure, hiddenStructure, outputStructure);

      int actualResult = networkSettings.CalculateNumberOfWeights();

      Assert.AreEqual(expectedResult, actualResult);
    }
  }
}
