using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class SnakeLevel {

    public Grid Grid { get; }
    public SnakeSettings SnakeSettings { get; }

    public SnakeLevel(Grid grid, SnakeSettings snakeSettings) {
      Grid = grid;
      SnakeSettings = snakeSettings;
    }
  }
}
