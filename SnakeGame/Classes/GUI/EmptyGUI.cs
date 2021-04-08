using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  /// <summary>
  /// Empty inherits from the <see cref="FieldGUI"/> class, to define it's location, sidelength and to method to draw to a surface.
  /// </summary>
  [Serializable]
  public class EmptyGUI : FieldGUI {
      // A solid brush to draw the background.
      private readonly SolidBrush SolidBrush;

		public EmptyGUI(SnakeGameNS.Point point, int sideLength) : base(point, sideLength) {
			SolidBrush = new SolidBrush(Color.Gray);
		}

    /// <summary>
    /// Draws the specific <see cref="EmptyGUI"/> object, to the graphical drawing surface.
    /// </summary>
    /// <param name="graphics">Graphical drawing surface.</param>
    public override void Draw(Graphics graphics) {
			graphics.FillRectangle(SolidBrush, new Rectangle(Point.Column * Sidelength, Point.Row * Sidelength, Sidelength, Sidelength));
		}
	}
}
