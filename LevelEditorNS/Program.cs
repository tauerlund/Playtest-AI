using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGameNS;

namespace LevelEditorNS {
  class Program {
    [STAThread]
    static void Main(string[] args) {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      SnakeSettingsGUI snakeSettings = new SnakeSettingsGUI(20, 30, 20);
      SnakeLevelEditor levelEditor = new SnakeLevelEditor(snakeSettings);

      Application.Run(levelEditor);
      Console.ReadKey();
    }
  }
}
