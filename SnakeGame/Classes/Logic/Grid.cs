using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace SnakeGameNS {

  [Serializable]
  public class Grid {

    public Field[,] Fields { get; private set; }
    // Accessor to access Grid index by point
    public Field this[Point point] {
      get { return Fields[point.Row, point.Column]; }
      set { Fields[point.Row, point.Column] = value; }
    }

    // Acessor to accees Grid index by integers
    public Field this[int row, int column] {
      get { return Fields[row, column]; }
      set { Fields[row, column] = value; }
    }

    public int RowCount { get; }
    public int ColumnCount { get; }

    // Keep track of previous tail position to know which field to delete every round
    private Point previousSnakeHeadPosition;
    private Point previousSnakeTailPosition;

    // Constructor to generate random grid.
    public Grid(int rowCount, int columnCount) {
      RowCount = rowCount;
      ColumnCount = columnCount;
      // Make empty grid with walls
      InitializeGrid();
    }

    // Constructor to generate grid from given fields.
    public Grid(Field[,] fields) {
      Fields = fields;
      RowCount = fields.GetLength(0);
      ColumnCount = fields.GetLength(1);
    }

    // Fill grid with walls and empty fields
    private void InitializeGrid() {
      Fields = new Field[RowCount, ColumnCount];

      for(int i = 0; i < RowCount; i++) {
        for(int j = 0; j < ColumnCount; j++) {
          if((i == 0 || j == 0) || (i == RowCount - 1 || j == ColumnCount - 1)) {
            Fields[i, j] = new Wall(new Point(i, j));
          }
          else {
            Fields[i, j] = new Empty(new Point(i, j));
          }
        }
      }
    }

    public void PlaceSnake(Snake snake) {
      if(RowCount * ColumnCount < SnakeConstants.MIN_SNAKE_FIELDS) {
        throw new Exception($"Grid too small - no room for snake and food");
      }

      this[snake.Head.Point] = snake.Head;

      if(snake.HasBody) {
        for(int i = 0; i < snake.Body.Count; i++) {
          this[snake.Body[i].Point] = snake.Body[i];
        }
      }

      // Copy position of game objects
      previousSnakeHeadPosition = new Point(snake.Head.Point.Row, snake.Head.Point.Column);
      previousSnakeTailPosition = new Point(snake.Head.Point.Row, snake.Head.Point.Column);
    }

    public void PlaceFood(Food food) {
      this[food.Point] = food;
    }

    // Search for empty field
    public Point GetPointOfRandomEmptyField(IRandomNumberGenerator random) {
      Point randomPoint;
      do {
        randomPoint = new Point(random.GetInt(1, RowCount), random.GetInt(1, ColumnCount));
      } while(!(this[randomPoint] is Empty));
      return randomPoint;
    }

    public Point GetCentrePoint() {
      Point centrePoint;
      centrePoint = new Point((RowCount - 1 - (RowCount / 2)), ColumnCount - 1 - (ColumnCount / 2));
      return centrePoint;
    }

    public Field GetNextFieldInDirection(Point currentPoint, Direction directionToMove) {
      Point nextFieldPoint = new Point(currentPoint.Row, currentPoint.Column);
      nextFieldPoint.Move(directionToMove);
      return this[nextFieldPoint];
    }

    public bool PointWithinGrid(Point point) {
      if((point.Row > 0 && point.Row < RowCount-1) &&
         (point.Column > 0 && point.Column < ColumnCount-1)) {
        return true;
      }
      else {
        return false;
      }
    }

    // Update object of type snake. Only updates fields that has been changed 
    public void UpdateSnakePosition(Snake snake, bool bodyAdded) {
      // Place new snake head on grid
      this[snake.Head.Point] = snake.Head;

      // If snake has a body, replace previous position of snake head with snake body
      if(snake.HasBody) {
        this[previousSnakeHeadPosition] = new SnakeBodyPart(previousSnakeHeadPosition);
      }
      // If no body added in this round, delete tail 
      if(!bodyAdded) {
        if(this[previousSnakeTailPosition] is Wall) {
        }
        this[previousSnakeTailPosition] = new Empty(previousSnakeTailPosition);
      }

      // Copy position of head and tail to use when updating next time
      previousSnakeHeadPosition = new Point(snake.Head.Point.Row, snake.Head.Point.Column);
      previousSnakeTailPosition = new Point(snake.Body.Last().Point.Row, snake.Body.Last().Point.Column);
    }

    public void UpdateFoodPosition(Food food) {
      this[food.Point] = food;
      // Not deleting food position as it has been replaced by snake head earlier
    }

    /// <summary>
    /// Returns a deep copy of the grid by making new instances of every field. 
    /// </summary>
    public Grid GetCopy() {
      int rowCount = RowCount;
      int columnCount = ColumnCount;
      Grid gridCopy = new Grid(rowCount, columnCount);

      for(int i = 0; i < rowCount; i++) {
        for(int j = 0; j < columnCount; j++) {
          SnakeGameNS.Point currentPoint = new Point(i, j);
          gridCopy[currentPoint] = GetFieldCopy(currentPoint);
        }
      }
      return gridCopy;
    }
    // Returns a copy of recieved field
    public Field GetFieldCopy(Point currentPoint) {
      Field fieldCopy;
      switch(this[currentPoint]) {
        case Wall w:
          fieldCopy = new Wall(currentPoint);
          break;
        case Food f:
          fieldCopy = new Food(currentPoint);
          break;
        case SnakeHead h:
          Direction currentDirection = h.Direction;
          fieldCopy = new SnakeHead(currentPoint, currentDirection);
          break;
        case SnakeBodyPart b:
          fieldCopy = new SnakeBodyPart(currentPoint);
          break;
        case Empty e:
          fieldCopy = new Empty(currentPoint);
          break;
        default:
          throw new Exception("Unable to find type");
      }
      return fieldCopy;
    }
  }
}
