using NeuralNetworkNS;
using SnakeGameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI {
  /// <summary>
  /// Describes relevant information about a round in a playtest simulation.
  /// </summary>
  public class PlaytestRoundInfo {
    public List<Field> ChangedFields { get; }
    public int Score { get; }
    public bool IsAlive { get; }
    public Point SnakeHeadPoint { get; }
    public Direction CurrentDirection { get; }
    public int TotalMoves { get; }

    /// <summary>
    /// Copies recieved information to the properties of the class. 
    /// </summary>
    public PlaytestRoundInfo(SnakeGame snakeGame, List<Field> changedFields) {
      Score = snakeGame.Score;
      IsAlive = snakeGame.Snake.IsAlive;
      CurrentDirection = snakeGame.Snake.Head.Direction;
      SnakeHeadPoint = snakeGame.Snake.Head.Point;
      ChangedFields = changedFields;
    }
  }
}
