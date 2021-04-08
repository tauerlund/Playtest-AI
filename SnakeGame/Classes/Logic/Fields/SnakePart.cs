using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  // Snake parts have a direction and move method.
  [Serializable]
  public abstract class SnakePart : Field {
    public Direction Direction { get; set; }  

    protected SnakePart(Point point) : base(point) {
    }

    public void Move(Direction direction) {
      Point.Move(direction);
    }
  }
}
