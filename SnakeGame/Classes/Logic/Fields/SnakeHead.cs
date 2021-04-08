using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class SnakeHead : SnakePart {

    public SnakeHead(Point point, Direction direction) : base(point) {
      Direction = direction;
    }
  }
}
