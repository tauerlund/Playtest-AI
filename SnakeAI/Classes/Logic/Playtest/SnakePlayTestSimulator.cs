using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGameNS;
using NeuralNetworkNS;
using GeneticAlgorithmNS;

// Unikt for vores løsing! 

namespace SnakeAI {
  /// <summary>
  /// Representes a simulator which is able to simulate a play test snake simulation. 
  /// </summary>
  public class SnakePlayTestSimulator {
    private SnakeLevel snakeLevel;

    /// <summary>
    /// Creates a play test simulator based on the given snake level.
    /// </summary>
    /// <param name="snakeLevel"></param>
    public SnakePlayTestSimulator(SnakeLevel snakeLevel) {
      this.snakeLevel = snakeLevel;
    }

    /// <summary>
    /// Runs a play test simulation of the saved agent and returns the result. 
    /// </summary>
    /// <param name="savedAgent"></param>
    /// <param name="testType"></param>
    /// <param name="record"></param>
    /// <returns></returns>
    public PlaytestResult RunSimulation(SavedAgent savedAgent, TestType testType, bool record) {
      int action = 0;
      int movesTotal = 0;
      int currentScore = 0;
      int movesSincePoint = 0;
      int maxStepsBeforeTerminating = 1000;
      double averageMovesPerFood = 0;
      bool isInBounds = true;
      PlaytestRecording recorder = null;
      TestResult testResult = TestResult.Failed;

      NetworkSettings networkSettings = (savedAgent.geneticSettings.FitnessCalculator as SnakeFitnessCalculator).NetworkSettings;
      SnakeSettings snakeSettings = (savedAgent.geneticSettings.FitnessCalculator as SnakeFitnessCalculator).SnakeSettings;

      // Setup new snake game and neural network
      NeuralNetwork neuralNetwork = new NeuralNetwork(networkSettings, savedAgent.agent.Chromosome.ToArray());
      double[] input = new double[networkSettings.numberOfInputNeurons]; 
      SnakeGame snakeGame = new SnakeGame(snakeLevel);

      // Make recorder
      if(record) {
        recorder = new PlaytestRecording(savedAgent.agent, networkSettings, snakeSettings);
        recorder.TakeSnapShotInitial(snakeGame);
      }

      if(record) { // Save round info.
        recorder.TakeSnapShotRound(snakeGame);
      }

      // Check within bounds.
      if(testType == TestType.WithinBounds) {
        if(snakeGame.Grid.PointWithinGrid(snakeGame.Snake.Head.Point)) {
          testResult = TestResult.Passed;
        }
        else {
          testResult = TestResult.Failed;
          isInBounds = false;
        }
      }

      if(isInBounds) {
        do {
          input = ConvertGridToInput(snakeGame.Grid, snakeGame.Snake, snakeGame.Food);
          action = neuralNetwork.CalculateOutput(input);
          snakeGame.UpdateDirection(action);

          // Check if got point
          if(snakeGame.Score != currentScore) {
            movesSincePoint = 0;
            currentScore = snakeGame.Score;
          }
          else {
            movesSincePoint++;
          }
          movesTotal++;

          if(record) { // Save round info.
            recorder.TakeSnapShotRound(snakeGame);
          }
          // Test within bounds. 
          if(testType == TestType.WithinBounds) {
            if(snakeGame.Grid.PointWithinGrid(snakeGame.Snake.Head.Point)) {
              testResult = TestResult.Passed;
            }
            else {
              testResult = TestResult.Failed;
              isInBounds = false;
            }
          }
        } while(snakeGame.Snake.IsAlive && movesSincePoint < maxStepsBeforeTerminating && isInBounds);

      }

      // Check if snake completed game.
      if(testType == TestType.CanComplete) {
        if(snakeGame.MaxScore != snakeGame.Score) {
          testResult = TestResult.Failed;
        }
        else {
          testResult = TestResult.Passed;
        }
      }

      // Get avg. moves per food.
      if(snakeGame.FoodEaten != 0) {
        averageMovesPerFood = movesTotal / (double)snakeGame.FoodEaten;
      }
      EndTestInfo endTestInfo = new EndTestInfo(snakeGame, movesTotal);

      if(record) {
        recorder.TakeSnapShotEndTest(endTestInfo);
      }

      PlaytestResult playtestResult = new PlaytestResult(testResult, recorder);

      return playtestResult;
    }

    private static double[] ConvertGridToInput(Grid grid, Snake snake, Food food) {
      double[] input = new double[ProgramSettings.NUMBER_OF_INPUT_NEURONS];

      int snakeRow = snake.Head.Point.Row;
      int snakeColumn = snake.Head.Point.Column;
      int foodRow = food.Point.Row;
      int foodColumn = food.Point.Column;

      // Look after obstacles in 4 directions
      input[0] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Up) is Wall)); // Wall up
      input[1] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Down) is Wall)); // Wall up
      input[2] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Left) is Wall)); // Wall up
      input[3] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Right) is Wall)); // Wall up

      input[4] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Up) is SnakeBodyPart)); // Wall up
      input[5] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Down) is SnakeBodyPart)); // Wall up
      input[6] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Left) is SnakeBodyPart)); // Wall up
      input[7] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Right) is SnakeBodyPart)); // Wall up

      // Look for food
      input[8] = Convert.ToInt32(foodRow < snakeRow); // Is food above snake
      input[9] = Convert.ToInt32(foodRow > snakeRow); // is food below snake
      input[10] = Convert.ToInt32(foodColumn < snakeColumn); // Is food left of snake
      input[11] = Convert.ToInt32(foodColumn > snakeColumn); // Is food right of snake

      input[12] = Convert.ToInt32(IsFieldInDirection(grid, snake.Head, typeof(SnakeBodyPart), Direction.Up));
      input[13] = Convert.ToInt32(IsFieldInDirection(grid, snake.Head, typeof(SnakeBodyPart), Direction.Down));
      input[14] = Convert.ToInt32(IsFieldInDirection(grid, snake.Head, typeof(SnakeBodyPart), Direction.Left));
      input[15] = Convert.ToInt32(IsFieldInDirection(grid, snake.Head, typeof(SnakeBodyPart), Direction.Right));

      return input;
    }

    // Search for field type in unknown location
    private static bool IsFieldInDirection(Grid grid, Field currentField, Type targetFieldType, Direction direction) { //fitnesscalc 
      bool targetFound = false;

      Point currentPoint = new Point(currentField.Point.Row, currentField.Point.Column);

      currentPoint.Move(direction);

      while(grid.PointWithinGrid(currentPoint) && !targetFound) {
        if(grid[currentPoint].GetType() == targetFieldType) { // ændres
          targetFound = true;
        }
        else {
          currentPoint.Move(direction);
        }
      }

      return targetFound;
    }  

  }

}
