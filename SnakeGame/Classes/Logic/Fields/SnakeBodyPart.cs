using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class SnakeBodyPart : SnakePart {
    public bool NewBody { get; set; }

    public SnakeBodyPart(Point point) : base(point) {
      NewBody = true;
      Direction = Direction.None;
    }
  }
}
