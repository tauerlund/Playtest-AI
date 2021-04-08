
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeGameNS {
  /// <summary>
  /// An abstract class, with properties to define rows, columns, side length and the state of a field.
  /// Also har an abstract method to draw a field on a graphical surface.
  /// </summary>
  [Serializable]
  public abstract class FieldGUI {

    public int Sidelength { get; private set; }

    public SnakeGameNS.Point Point { get; set; }

    public FieldGUI(SnakeGameNS.Point point, int sideLength) {
      Point = point;
      Sidelength = sideLength;
    }

    /// <summary>
    /// Abstract method to draw the field.
    /// </summary>
    /// <param name="graphics">The graphical drawing surface, to draw the field on.</param>
    public abstract void Draw(Graphics graphics);
  }
}