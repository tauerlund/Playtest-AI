using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticAlgorithmNS;
using NeuralNetworkNS;
using SnakeGameNS;
using HelperMethods;

namespace SnakeAI {
  public static class Program {

    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      MainMenuGUI mainMenu = new MainMenuGUI();
      Application.Run(mainMenu);
    }
  }
}
