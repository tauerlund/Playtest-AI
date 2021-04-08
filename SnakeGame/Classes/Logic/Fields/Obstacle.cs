using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SnakeGameNS {
  // Class for obstacles - snake will die if hit. Can be used for walls etc.
  [Serializable]
  public abstract class Obstacle : Field {
    protected Obstacle(Point point) : base(point) {
    }
  }
}
