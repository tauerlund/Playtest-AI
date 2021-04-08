using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame {
    public class EmptyField : Field {
        private readonly SolidBrush SolidBrush;

        /// <summary>
        /// Initializes a new instance of the <see cref="Empty"/> class, within a specific field. 
        /// </summary>
        /// <param name="column">The column which the food is placed.</param>
        /// <param name="row">The row which the food is placed.</param>
        /// <param name="sideLength">The side length of the <see cref="Empty"/> instance.</param>
        public EmptyField(int column, int row, int sideLength) : base(column, row, sideLength, State.Empty) {
            SolidBrush = new SolidBrush(Color.Gray);
        }

        public override void Clear() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the specific <see cref="Empty"/> object, to the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="graphics">Graphicle drawing surface.</param>
        public override void Draw(Graphics graphics) {
            graphics.FillRectangle(SolidBrush, new Rectangle(Column * Sidelength, Row * Sidelength, Sidelength - 1, Sidelength - 1));
        }
    }
}
