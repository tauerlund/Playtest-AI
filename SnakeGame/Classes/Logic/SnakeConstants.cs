using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public static class SnakeConstants {
    public const int MIN_SNAKE_FIELDS = 12; // Min snake fields so room for walls.
    public const int NUMBER_OF_ACTIONS = 4; // Up, down, left, right
  }
}
