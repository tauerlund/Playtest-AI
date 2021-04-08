using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using GeneticAlgorithmNS;
using NeuralNetworkNS;
using SnakeGameNS;

namespace SnakeAI {
  class ButtonGUI : Button {
    public ButtonGUI(string text, EventHandler onClick) {
      Padding padding = new Padding();
      padding.All = 15;
      Margin = padding;

      Text = text;
      Dock = DockStyle.Fill;
      Click += onClick;
      Font = ConstantsGUI.FONT_HEADER;
    }
  }
}
