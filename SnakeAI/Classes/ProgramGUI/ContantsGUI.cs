using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ProgramGUI {
  public static class ConstantsGUI {

    // COLORS
    public static Color MAIN_BACKGROUND_COLOR = Color.White;
    public static Color SETTINGSBOX_BACKGROUND_COLOR = Color.White;

    // FONTS
    public static int BOX_SIZE = 30;
    public static Font FONT_SMALL = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
    public static Font FONT_SMALL_BOLD = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
    public static Font FONT_HEADER = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
  }
}
