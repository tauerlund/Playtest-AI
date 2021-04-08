using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithmNS;
using SnakeGameNS;

namespace SnakeAI {
  /// <summary>
  /// Defines properties which represent information about a fitness calculation of a snake game.
  /// </summary>
  [Serializable]
  public class EndGameInfo : IFitnessInfo {

    public double Fitness { get; }
    public int FoodEaten { get; }
    public double Score { get; }
    public double AverageMovesPerFood { get; }
    public double MovesTotal { get;  }
    public SnakeCauseOfDeath SnakeCauseOfDeath { get; private set; }

    /// <summary>
    /// Initializes a new instance of the class <see cref="EndGameInfo"/> 
    /// which contains the given information and exposes it in its properties.
    /// </summary>
    /// <param name="snakegame"></param>
    /// <param name="fitness"></param>
    /// <param name="averageMovesPerPoint"></param>
    /// <param name="movesTotal"></param>
    public EndGameInfo(SnakeGame snakegame, double fitness, double averageMovesPerPoint, int movesTotal) {
      Fitness = fitness;
      this.FoodEaten = snakegame.FoodEaten;
      this.Score = snakegame.Score;
      this.AverageMovesPerFood = averageMovesPerPoint;
      MovesTotal = movesTotal;
      SnakeCauseOfDeath = GetCauseOfDeath(snakegame.Snake);
    }

    private SnakeCauseOfDeath GetCauseOfDeath(Snake snake) {
      if(!snake.IsAlive) {
        return SnakeCauseOfDeath.KilledByGame;
      } // If still alive, exceeding step limit must have killed the snake
      else {
        return SnakeCauseOfDeath.KilledBySteps;
      }
    }
  }
}
