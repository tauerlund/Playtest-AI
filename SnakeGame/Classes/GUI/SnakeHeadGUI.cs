using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  /// <summary>
  /// The head inherits from the <see cref="FieldGUI"/> class, to define it's location, side length and to method to draw to a surface.
  /// </summary>
  [Serializable]
  public class SnakeHeadGUI : FieldGUI {
    // A solid brush to draw the background of the snake head object, rotation of the head,
    // and the image of the snake head object.
    private readonly SolidBrush SolidBrush;
    private readonly Image ImageSnakeAlive;
    private readonly Image ImageSnakeDead;
    public bool IsAlive;

    /// <summary>
    /// Initializes a new instance of the <see cref="SnakeHeadGUI"/> class, within a specific field. 
    /// </summary>
    /// <param name="column">The column which the <see cref="SnakeHeadGUI"/> instance is placed.</param>
    /// <param name="row">The row which the <see cref="SnakeHeadGUI"/> instance is placed.</param>
    /// <param name="sideLength">The side length of the <see cref="SnakeHeadGUI"/> instance.</param>
    /// <param name="rotation">The rotation of the snake head.</param>
    public SnakeHeadGUI(SnakeGameNS.Point point, int sideLength, Direction direction, bool isAlive) : base(point, sideLength) {
      IsAlive = isAlive;

      SolidBrush = new SolidBrush(Color.Gray);
      ImageSnakeAlive = SnakeGameNS.Properties.Resources.Snake_Head;
      ImageSnakeDead = SnakeGameNS.Properties.Resources.Snake_Head_Dead;
      RotateHead(direction);
    }

    /// <summary>
    /// Method to rotate the image of the snake head, to the specific direction  <seealso cref="direction"/>.
    /// </summary>
    private void RotateHead(Direction direction) {
      switch(direction) {
        case Direction.Up:
          ImageSnakeAlive.RotateFlip(RotateFlipType.Rotate180FlipNone);
          ImageSnakeDead.RotateFlip(RotateFlipType.Rotate180FlipNone);
          break;
        case Direction.Down:
          ImageSnakeAlive.RotateFlip(RotateFlipType.RotateNoneFlipNone);
          ImageSnakeDead.RotateFlip(RotateFlipType.RotateNoneFlipNone);
          break;
        case Direction.Left:
          ImageSnakeAlive.RotateFlip(RotateFlipType.Rotate90FlipNone);
          ImageSnakeDead.RotateFlip(RotateFlipType.Rotate90FlipNone);
          break;
        case Direction.Right:
          ImageSnakeAlive.RotateFlip(RotateFlipType.Rotate270FlipNone);
          ImageSnakeDead.RotateFlip(RotateFlipType.Rotate270FlipNone);
          break;
        case Direction.None: // Same as up, random choice. Only for setup
          ImageSnakeAlive.RotateFlip(RotateFlipType.Rotate180FlipNone);
          ImageSnakeDead.RotateFlip(RotateFlipType.Rotate180FlipNone);
          break;
        default:
          throw new Exception("Unable to rotate head");
      }
    }

    /// <summary>
    /// Draws the specific <see cref="SnakeHeadGUI"/> object, to the graphical drawing surface.
    /// </summary>
    /// <param name="graphics">Graphical drawing surface.</param>
    public override void Draw(Graphics graphics) {
      graphics.FillRectangle(SolidBrush, new Rectangle(Point.Column * Sidelength, Point.Row * Sidelength, Sidelength, Sidelength));
      if(IsAlive) { 
        graphics.DrawImage(ImageSnakeAlive, new System.Drawing.Point(Point.Column * Sidelength, Point.Row * Sidelength));
      }
      else {
        graphics.DrawImage(ImageSnakeDead, new System.Drawing.Point(Point.Column * Sidelength, Point.Row * Sidelength));
      }
    }
  }
}
