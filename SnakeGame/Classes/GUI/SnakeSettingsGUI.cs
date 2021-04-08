using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  /// <summary>
  /// A class that contains the settings of the snake game.
  /// </summary>
  [Serializable]
  public class SnakeSettingsGUI {

    /// <summary>
    /// The number of rows in the grid.
    /// </summary>
    public int RowCount { get; private set; }
    /// <summary>
    /// The number of columns in the grid.
    /// </summary>
    public int ColumnCount { get; private set; }
    /// The side length of each field in the grid.
    /// </summary>
    public int SideLength { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SnakeSettingsGUI"/> class, with custom settings.
    /// </summary>
    /// <param name="numColumns">The number of columns.</param>
    /// <param name="numRows">The number of rows.</param>
    /// <param name="sideLength">The side length of a single field.</param>
    public SnakeSettingsGUI(int rowCount, int columnCount, int sideLength) {
      this.RowCount = rowCount;
      this.ColumnCount = columnCount;
      this.SideLength = sideLength; 
    }
  }
}
