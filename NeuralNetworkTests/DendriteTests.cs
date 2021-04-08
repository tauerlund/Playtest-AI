using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkNS;

namespace NeuralNetworkTests {
  /// <summary>
  /// Summary description for DendriteTests
  /// </summary>
  [TestClass]
  public class DendriteTests {

    [TestMethod]
    public void CalculateValue_CalledWithOutput_HasCorrectValue() {
      double weight = 0.5;
      double output = 4;

      Dendrite d = new Dendrite(weight);
      d.CalculateValue(output);

      double expectedResult = 2;
      double actualResult = d.Value;

      Assert.AreEqual(expectedResult, actualResult);
    }
  }
}
