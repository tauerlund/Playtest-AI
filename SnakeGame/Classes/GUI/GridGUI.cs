using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class GridGUI {

    public FieldGUI[,] FieldGUIs { get; set; }

    // Accessor to access Grid index by point
    public FieldGUI this[Point point] {
      get { return FieldGUIs[point.Row, point.Column]; }
      private set { FieldGUIs[point.Row, point.Column] = value; }
    }

    // Acessor to accees Grid index by integers
    public FieldGUI this[int row, int column] {
      get { return FieldGUIs[row, column]; }
      private set { FieldGUIs[row, column] = value; }
    }

    public int RowCount { get; }
    public int ColumnCount { get; }
    public int SideLength { get; }

    /// <summary>
    /// The width of the grid.
    /// </summary>
    public int GridWidth { get; }

    /// <summary>
    /// The height of the grid.
    /// </summary>
    public int GridHeight { get; }

    // Constructor to generate grid
    public GridGUI(Grid gridModel, SnakeSettings snakeSettings, bool isAlive) {
      RowCount = snakeSettings.RowCount;
      ColumnCount = snakeSettings.ColumnCount;
      SideLength = snakeSettings.SideLengthGUI;
      GridWidth = ColumnCount * SideLength;
      GridHeight = RowCount * SideLength;
      InitilizeGridGUI(gridModel, isAlive);
    }

    // Converts model grid (logic) to GUI grid
    private void InitilizeGridGUI(Grid gridModel, bool isAlive) {
      FieldGUIs = new FieldGUI[RowCount, ColumnCount];

      for(int i = 0; i < RowCount; i++) {
        for(int j = 0; j < ColumnCount; j++) {
          SnakeGameNS.Point currentPoint = new Point(i, j);
          this[currentPoint] = GetGUIFieldEquivalent(gridModel[currentPoint], currentPoint, isAlive);
        }
      }
    }

    // Converts a logic field to a GUI field
    private FieldGUI GetGUIFieldEquivalent(Field fieldModel, SnakeGameNS.Point currentPoint, bool isAlive) {
      FieldGUI fieldGUI;

      switch(fieldModel) {
        case Wall w:
          fieldGUI = new WallGUI(currentPoint, SideLength);
          break;
        case Food f:
          fieldGUI = new FoodGUI(currentPoint, SideLength);
          break;
        case SnakeHead h:
          Direction headDirection = (fieldModel as SnakeHead).Direction;

          fieldGUI = new SnakeHeadGUI(currentPoint, SideLength, headDirection, isAlive);
          break;
        case SnakeBodyPart b:
          fieldGUI = new SnakeBodyPartGUI(currentPoint, SideLength);
          break;
        case Empty e:
          fieldGUI = new EmptyGUI(currentPoint, SideLength);
          break;
        default:
          throw new Exception("Unable to find type");
      }
      return fieldGUI;
    }

    // Updates the grid    
    public void Update(Grid gridModel, SnakeGameNS.Point snakeHeadPoint, bool isAlive) {
      //If snake not alive. Grid is not changed. Kill snake head to draw dead snake image. 
      if(!isAlive) {
        this[snakeHeadPoint] = GetGUIFieldEquivalent(gridModel[snakeHeadPoint], snakeHeadPoint, isAlive);
      }
      else {
        UpdateChangedFields(gridModel, isAlive);
      }
    }

    // Updates fields that has been changed
    private void UpdateChangedFields(Grid gridModel, bool isAlive) {
      for(int i = 0; i < RowCount; i++) {
        for(int j = 0; j < ColumnCount; j++) {
          SnakeGameNS.Point currentPoint = new Point(i, j);
          Type currentGUIType = GetGUITypeEquivalent(gridModel[currentPoint]);

          if(this[currentPoint].GetType() != currentGUIType) {
            this[currentPoint] = GetGUIFieldEquivalent(gridModel[currentPoint], currentPoint, isAlive);
          }
        }
      }
    }

    private Type GetGUITypeEquivalent(Field currentField) {
      Type GUItype = null;

      switch(currentField) {
        case Wall w:
          GUItype = typeof(WallGUI);
          break;
        case Food f:
          GUItype = typeof(FoodGUI);
          break;
        case SnakeHead h:
          GUItype = typeof(SnakeHeadGUI);
          break;
        case SnakeBodyPart b:
          GUItype = typeof(SnakeBodyPartGUI);
          break;
        case Empty e:
          GUItype = typeof(EmptyGUI);
          break;
        default:
          throw new Exception("Unable to find type");
      }
      return GUItype;
    }
    
    /// <summary>
    /// Draws each of the fields in the grid.
    /// </summary>
    /// <param name="graphics">Graphical drawing surface.</param>
    public void Draw(Graphics graphics) {
      foreach(FieldGUI field in FieldGUIs) {
        field.Draw(graphics);
      }
    }
   
  }
}
