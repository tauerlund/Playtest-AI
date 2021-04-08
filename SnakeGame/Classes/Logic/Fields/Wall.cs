using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class Wall : Obstacle {

    public Wall(Point point) : base(point) {
    }
  }
}
