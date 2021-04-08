using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  /// <summary>
  /// Obstacle inherits from the <see cref="FieldGUI"/> class, to define it's location, side length and to method to draw to a surface.
  /// </summary>
  [Serializable]
  public class WallGUI : FieldGUI {
    // A solid brush to draw the background of the snake body object, and the image of the snake body object.
    private readonly Image Image;

    /// <summary>
    /// Initializes a new instance of the <see cref="WallGUI"/> class, within a specific field. 
    /// </summary>
    /// <param name="column">The column which the <see cref="WallGUI"/> instance is placed.</param>
    /// <param name="row">The row which the <see cref="WallGUI"/> instance is placed.</param>
    /// <param name="sideLength">The side length of the <see cref="WallGUI"/> instance.</param>
    public WallGUI(SnakeGameNS.Point point, int sideLength) : base(point, sideLength) {
      Image = SnakeGameNS.Properties.Resources.Obstacle;
    }

    /// <summary>
    /// Draws the specific <see cref="WallGUI"/> object, to the graphical drawing surface.
    /// </summary>
    /// <param name="graphics">Graphical drawing surface.</param>
    public override void Draw(Graphics graphics) {
      graphics.DrawImage(Image, new System.Drawing.Point(Point.Column * Sidelength, Point.Row * Sidelength));
    }
  }
}
