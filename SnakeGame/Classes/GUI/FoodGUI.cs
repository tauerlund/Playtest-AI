using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  /// <summary>
  /// Food inherits from the <see cref="FieldGUI"/> class, to define it's location, sidelength and to method to draw to a surface.
  /// </summary>
  [Serializable]
  public class FoodGUI : FieldGUI {
    // A solid brush to draw the background of the food object, and the image of the food object.
    private readonly SolidBrush SolidBrush;
    private readonly Image Image;

    /// <summary>
    /// Initializes a new instance of the <see cref="FoodGUI"/> class, within a specific field. 
    /// </summary>
    /// <param name="column">The column which the <see cref="FoodGUI"/> instance is placed.</param>
    /// <param name="row">The row which the <see cref="FoodGUI"/> instance is placed.</param>
    /// <param name="sideLength">The side length of the <see cref="FoodGUI"/> instance.</param>
    public FoodGUI(SnakeGameNS.Point point, int sideLength) : base(point, sideLength) {
      SolidBrush = new SolidBrush(Color.Gray);
      Image = SnakeGameNS.Properties.Resources.Apple;
    }

    /// <summary>
    /// Draws the specific <see cref="FoodGUI"/> object, to the graphical drawing surface.
    /// </summary>
    /// <param name="graphics">Graphical drawing surface.</param>
    public override void Draw(Graphics graphics) {
      graphics.FillRectangle(SolidBrush, new Rectangle(Point.Column * Sidelength, Point.Row * Sidelength, Sidelength, Sidelength));
      graphics.DrawImage(Image, new System.Drawing.Point(Point.Column * Sidelength, Point.Row * Sidelength));
    }

  }


}
