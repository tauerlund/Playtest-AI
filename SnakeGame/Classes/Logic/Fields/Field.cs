using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public abstract class Field {
    public Point Point { get; }
    protected Field(Point point) {
      Point = point;
    }
  }
}
