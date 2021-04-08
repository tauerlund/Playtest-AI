using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class SnakeGame {

    public Grid Grid { get; private set; }
    public Food Food { get; private set; }
    public Snake Snake { get; private set; }
    public int MaxScore { get; private set; }
    public int FoodEaten { get; private set; }
    public int Score {
      get { return FoodEaten * snakeSettings.PointsPerFoodEaten; }
    }

    private SnakeSettings snakeSettings; 
    private SnakeGameGUI snakeGameGUI;
    private Direction previousDirection;
    private bool withGUI;
    private bool foundFood;

    /// <summary>
    /// Initializes a new snake game by using the specified snake level.
    /// </summary>
    /// <param name=""></param>
    public SnakeGame(SnakeLevel snakeLevel) {
      Grid = snakeLevel.Grid.GetCopy();
      snakeSettings = snakeLevel.SnakeSettings;
      MaxScore = snakeLevel.SnakeSettings.MaxScore;
      // Make snake
      Snake = new Snake(Grid.GetPointOfRandomEmptyField(snakeSettings.RandomNumberGenerator));
      // Make food
      Food = new Food(Grid.GetPointOfRandomEmptyField(snakeSettings.RandomNumberGenerator));
      foundFood = false;
      // Place objects in grid
      Grid.PlaceSnake(Snake);
      Grid.PlaceFood(Food);
    }

    /// <summary>
    /// Initializes a new snake game by using the specified settings to set up af random grid.
    /// </summary>
    public SnakeGame(SnakeSettings snakeSettings) {
      this.snakeSettings = snakeSettings;
      MaxScore = snakeSettings.MaxScore;
      // Make grid
      Grid = new Grid(snakeSettings.RowCount, snakeSettings.ColumnCount);
      // Make snake
      Snake = new Snake(Grid.GetCentrePoint());
      // Make food
      Food = new Food(Grid.GetPointOfRandomEmptyField(snakeSettings.RandomNumberGenerator));
      foundFood = false;
      // Place objects in grid
      Grid.PlaceSnake(Snake);
      Grid.PlaceFood(Food);
    }

    /// <summary>
    /// Runs a GUI window of the snake game.
    /// </summary>
    public void StartGUI() {
      withGUI = true;
      snakeGameGUI = new SnakeGameGUI(snakeSettings, this);
      snakeGameGUI.OpenGameWindow();
    }

    /// <summary>
    /// Updates game grid as a result of passed action.
    /// </summary>
    /// <param name="action"> 
    /// Up = 1, Down = 2, Left = 3, Right = 4.
    /// </param>
    public void UpdateDirection(int action) {
      // Convert action recived by caller to enum
      Direction directionToMove = GetDirectionToMove(action);

      // Prevent from going against itself
      if(isOppositeDirections(directionToMove, previousDirection)) {
        directionToMove = previousDirection;
      }

      // Investigate the field snake is moving to.
      Field targetField = Grid.GetNextFieldInDirection(Snake.Head.Point, directionToMove);

      // Check if snake is dead (obstacle or body hit).
      if(DeadlyField(targetField)) {
        Snake.Kill();
      }
      else { // If not dead, do a normal round.
        // Check if food --> extend snake before moving!
        if(targetField is Food) {
          FoodEaten++;
          Snake.Extend();
          foundFood = true;
        }

        // Move snake and place on grid.
        Snake.Move(directionToMove);
        Grid.UpdateSnakePosition(Snake, foundFood);

        // Make new food AFTER snake has been moved and placed in grid, to avoid placing food where the snake will move to
        if(foundFood) {
          // Check if game has been completed
          if(Score != snakeSettings.MaxScore) { // Check if max points, else method GetRandomField will never find empty field = infinite loop.
            Food = new Food(Grid.GetPointOfRandomEmptyField(snakeSettings.RandomNumberGenerator));
            Grid.UpdateFoodPosition(Food);
          }
          else {
            Snake.Kill();
          }
          foundFood = false;
        }
        // Save direction for next round to check if going against itself.
        previousDirection = directionToMove;
      }
      if(withGUI) {
        snakeGameGUI.UpdateView(this);
      }
    }

    // Check if passed directions is opposite of eachother.
    private bool isOppositeDirections(Direction direction1, Direction direction2) {
      bool isOpposite = false;
      switch(direction1) {
        case Direction.Up:
          if(direction2 == Direction.Down) {
            isOpposite = true;
          }
          break;
        case Direction.Down:
          if(direction2 == Direction.Up) {
            isOpposite = true;
          }
          break;
        case Direction.Left:
          if(direction2 == Direction.Right) {
            isOpposite = true;
          }
          break;
        case Direction.Right:
          if(direction2 == Direction.Left) {
            isOpposite = true;
          }
          break;
        default:
          isOpposite = false;
          break;
      }
      return isOpposite;
    }

    // Check if field will make snake die.
    private bool DeadlyField(Field newField) {
      if(newField is Obstacle ||
         newField is SnakeBodyPart) {
        return true;
      }
      else {
        return false;
      }
    }

    // Converts an integer to correct snake action.
    private Direction GetDirectionToMove(int action) {
      Direction directionToMove;
      directionToMove = (Direction)action;

      if(!(directionToMove == Direction.Up || directionToMove == Direction.Down ||
          directionToMove == Direction.Left || directionToMove == Direction.Right)) {
        throw new Exception("Invalid action passed to SnakeGame");
      }
      return directionToMove;
    }
  }
}
