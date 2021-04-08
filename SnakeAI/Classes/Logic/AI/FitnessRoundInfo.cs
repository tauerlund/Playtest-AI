using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGameNS;
using NeuralNetworkNS;
using GeneticAlgorithmNS;
using System.Diagnostics;

namespace SnakeAI {
  /// <summary>
  /// Describes relevant information about a round in a fitness calculation.
  /// </summary>
  [Serializable]
  public class FitnessRoundInfo {

    // A list of only the fields that has been changed. 
    public List<Field> ChangedFields { get; }

    // Snake game info.
    public int Score { get; }
    public bool IsAlive { get; }
    public Point SnakeHeadPoint { get; }
    public Direction CurrentDirection { get; }

    // Info for AI GUI
    public int MovesSincePoint { get; }
    public int TotalMoves { get; }
    public double[] InputNeuralNetwork { get; }
    public Dictionary<int, double> OutputNeuralNetwork { get; }

    /// <summary>
    /// Copies recieved information to its properties.
    /// </summary>
    public FitnessRoundInfo(SnakeGame snakeGame, int movesSincePoint, int totalMoves, double[] inputNeuralNetwork,
                             NeuralNetwork neuralNetwork, List<Field> changedFields) {

      Score = snakeGame.Score;
      IsAlive = snakeGame.Snake.IsAlive;
      CurrentDirection = snakeGame.Snake.Head.Direction;
      MovesSincePoint = movesSincePoint;
      TotalMoves = totalMoves;
      SnakeHeadPoint = snakeGame.Snake.Head.Point;

      InputNeuralNetwork = new double[inputNeuralNetwork.Length];
      Array.Copy(inputNeuralNetwork, InputNeuralNetwork, inputNeuralNetwork.Length);
      OutputNeuralNetwork = neuralNetwork.GetOutputValues();

      ChangedFields = changedFields;
    }
  }
}
