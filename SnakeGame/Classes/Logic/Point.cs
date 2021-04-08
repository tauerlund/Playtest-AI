using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class Point {
    public int Row { get; private set; }
    public int Column { get; private set; }

    public Point(int row, int column) {
      Row = row;
      Column = column;
    }

    // Move point based on passed direction
    public void Move(Direction direction) {
      switch(direction) {
        case Direction.Up:
          Row--;
          break;
        case Direction.Down:
          Row++;
          break;
        case Direction.Left:
          Column--;
          break;
        case Direction.Right:
          Column++;
          break;
        default:
          throw new Exception("Invalid direction passed to Move method");
      }
    }
  }
}
