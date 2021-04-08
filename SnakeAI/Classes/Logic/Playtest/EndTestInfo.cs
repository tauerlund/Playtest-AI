using SnakeGameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI {
  /// <summary>
  /// Defines properties which represent information about a test simulation of a snake game.
  /// </summary>
  [Serializable]
  public class EndTestInfo {
    public double Score { get;  }
    public double MovesTotal { get;  }

    public EndTestInfo(SnakeGame snakegame, int movesTotal) {
      Score = snakegame.Score;
      MovesTotal = movesTotal;
    }
  }
}