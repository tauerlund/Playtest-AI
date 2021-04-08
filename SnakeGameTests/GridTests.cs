using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGameNS;

namespace SnakeGameTests {
  [TestClass]
  public class GridTests {
    Field[,] fields;
    Grid grid;
    
    [TestInitialize]
    public void TestInitialize() {
      fields = new Field[10, 10];
      grid = new Grid(fields);
    }

    [TestMethod]
    public void Grid_TestPointWithinGridMethodWithPointInside_ReturnsCorrectBool() {
      Point pointToTest = new Point(4, 7);

      bool expectedResult = true;
      bool actualResult = grid.PointWithinGrid(pointToTest);

      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Grid_TestPointWithinGridMethodWithPointOutside_ReturnsCorrectBool() {
      Point pointToTest = new Point(2, 16);

      bool expectedResult = false;
      bool actualResult = grid.PointWithinGrid(pointToTest);

      Assert.AreEqual(expectedResult, actualResult);
    }
  }
}
