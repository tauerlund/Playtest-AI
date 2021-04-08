using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace SnakeGameNS {

  // Settings passed to snake game
  [Serializable]
  public class SnakeSettings {
    public int RowCount { get; private set; }
    public int ColumnCount { get; private set; }
    public IRandomNumberGenerator RandomNumberGenerator { get; set; }
    public int MaxScore { get;  }
    public int PointsPerFoodEaten { get;  }
    public int SideLengthGUI {get;}  // For GUI

    public SnakeSettings(int gridRows, int gridColumns, int sideLenghtGUI,
                         int pointsPerFoodEaten, IRandomNumberGenerator randomNumberGenerator) {
      RowCount = gridRows;
      ColumnCount = gridColumns;
      PointsPerFoodEaten = pointsPerFoodEaten;
      RandomNumberGenerator = randomNumberGenerator;
      SideLengthGUI = sideLenghtGUI;
      // Max Points = Every field not walls or snake head
      MaxScore = ((gridRows * gridColumns) - (gridRows * 2 + gridColumns * 2) + (2 + 2) - 1) * pointsPerFoodEaten;
    }

    public void UpdateSettings(int gridRows, int gridColumns) {
      RowCount = gridRows;
      ColumnCount = gridColumns;
    }
  }
}
