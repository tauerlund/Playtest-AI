using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticAlgorithmNS;
using SnakeGameNS;
using NeuralNetworkNS;

namespace SnakeAI {

  /// <summary>
  /// Represents methods used for calculating the fitness value of a snake game simulation.
  /// </summary>
  [Serializable]
  public class SnakeFitnessCalculator : IFitnessCalculator {

    public NetworkSettings NetworkSettings { get; }
    public SnakeSettings SnakeSettings { get; }
    private SnakeGame snakeGame;
    private NeuralNetwork neuralNetwork;

    private bool record; 
    private Agent agentToRecord;
    private FitnessCalculatorRecording recorder;

    public static int FitnessRoundsCount { get; private set; }

    /// <summary>
    /// Initializes a new instance of the class <see cref="SnakeFitnessCalculator"/>
    /// which is able to calculate the fitness value of an agent using the specified settings. 
    /// </summary>
    /// <param name="networkSettings"></param>
    /// <param name="snakeSettings"></param>
    public SnakeFitnessCalculator(NetworkSettings networkSettings, SnakeSettings snakeSettings) {
      this.NetworkSettings = networkSettings;
      this.SnakeSettings = snakeSettings;
    }

    /// <summary>
    /// Records a fitness calculation of the passed agent and returns the recorder containing all relevant info about the calculation
    /// </summary>
    public FitnessCalculatorRecording RecordCalculation(Agent agent) {
      record = true;
      agentToRecord = agent;
      CalculateFitness(agent.Chromosome.ToArray());
      record = false;
      return recorder;
    }

    /// <summary>
    /// Calculates the resulting fitness value by using the passed weights 
    /// and returns the value in a FitnessInfo class together with other relevant info about the calculation.
    /// </summary>
    public IFitnessInfo CalculateFitness(double[] weightsForNeuralNetwork) {
      int action = 0;
      int currentScore = 0;
      int movesSincePoint = 0;
      int movesTotal = 0;
      double fitness = 0;
      double averageMovesPerFood = 0;
      double[] input = new double[ProgramSettings.NUMBER_OF_INPUT_NEURONS];
      EndGameInfo endGameInfo;
      // Setup new snake game and neural network
      neuralNetwork = new NeuralNetwork(NetworkSettings, weightsForNeuralNetwork);
      snakeGame = new SnakeGame(SnakeSettings);

      // Make recorder
      if(record) {
        recorder = new FitnessCalculatorRecording(agentToRecord, NetworkSettings, SnakeSettings);
        recorder.TakeSnapShotInitial(snakeGame, movesSincePoint, movesTotal, input, neuralNetwork);
      }

      // Simulation begins
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
          recorder.TakeSnapShotRound(snakeGame, movesSincePoint, movesTotal, input, neuralNetwork);
        }
        FitnessRoundsCount++;


      } while(snakeGame.Snake.IsAlive && movesSincePoint < GetMaxMoves(snakeGame.Snake.Lenght));

      if(snakeGame.FoodEaten != 0) {
        averageMovesPerFood = movesTotal / (double)snakeGame.FoodEaten;
      }

      fitness = snakeGame.Score;
      endGameInfo = new EndGameInfo(snakeGame, fitness, averageMovesPerFood, movesTotal);

      if(record) {
        recorder.TakeSnapShotEndGame(endGameInfo);
      }
      return endGameInfo;
    }

    /// <summary>
    /// Gets the maximum number of moves the snake is allowed to move. 
    /// </summary>
    /// <param name="snakeLenght"></param>
    /// <returns></returns>
    private int GetMaxMoves(int snakeLenght) {
      int maxStepsSincePoint;
      // If not recording, set specified limit.
      if(!record) {
        maxStepsSincePoint = snakeLenght + ProgramSettings.MAX_STEPS;
      }
      else {
        maxStepsSincePoint = 1000;
      }
      return maxStepsSincePoint;
    }

    // Converts the grid to input for the neural network.
    private static double[] ConvertGridToInput(Grid grid, Snake snake, Food food) {
      double[] input = new double[ProgramSettings.NUMBER_OF_INPUT_NEURONS];

      int snakeRow = snake.Head.Point.Row;
      int snakeColumn = snake.Head.Point.Column;
      int foodRow = food.Point.Row;
      int foodColumn = food.Point.Column;

      // Look after obstacles in next field in 4 directions
      input[0] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Up) is Wall));
      input[1] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Down) is Wall));
      input[2] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Left) is Wall));
      input[3] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Right) is Wall));

      input[4] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Up) is SnakeBodyPart));
      input[5] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Down) is SnakeBodyPart));
      input[6] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Left) is SnakeBodyPart));
      input[7] = Convert.ToInt32((grid.GetNextFieldInDirection(snake.Head.Point, Direction.Right) is SnakeBodyPart));

      // Look for food
      input[8] = Convert.ToInt32(foodRow < snakeRow); // Is food above snake
      input[9] = Convert.ToInt32(foodRow > snakeRow); // is food below snake
      input[10] = Convert.ToInt32(foodColumn < snakeColumn); // Is food left of snake
      input[11] = Convert.ToInt32(foodColumn > snakeColumn); // Is food right of snake

      // Look for body in 4 directions
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
        if(grid[currentPoint].GetType() == targetFieldType) {
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
