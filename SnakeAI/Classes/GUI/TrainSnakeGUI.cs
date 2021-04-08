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
using System.Diagnostics;
using System.Threading;

namespace SnakeAI {
  class TrainSnakeGUI : Form {

    private GeneticAlgorithm geneticAlgorithm;
    private SnakeAISettings snakeAISettings;

    public TrainSnakeGUI(SnakeAISettings snakeAISettings) {
      this.snakeAISettings = snakeAISettings;

      Width = 450;
      Height = 400;

      Text = "Train AI";

      StartPosition = FormStartPosition.CenterScreen;

      InitializeComponent();
    }

    private void InitializeComponent() {
      TableLayoutPanel layout = new TableLayoutPanel();

      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;

      Padding padding = new Padding();
      padding.All = 20;
      layout.Padding = padding;

      layout.AutoSize = true;
      layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      layout.RowCount = 4;
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      layout.Dock = DockStyle.Fill;

      layout.Controls.Add(new ButtonGUI("Start Algorithm", OnClickStartAlgorithm), 0, 1);
      layout.Controls.Add(new ButtonGUI("Settings", OnClickSettings), 0, 2);
      layout.Controls.Add(new ButtonGUI("Back", OnClickBack), 0, 3);

      Controls.Add(layout);
    }

    private void OnClickStartAlgorithm(object sender, EventArgs e) {
      geneticAlgorithm = new GeneticAlgorithm(snakeAISettings.GeneticSettings);
                                             
      geneticAlgorithm.Train();
    }
    
    private void OnClickSettings(object sender, EventArgs e) {
      Hide();
      SettingsGUI settingsGUI = new SettingsGUI(snakeAISettings);
      settingsGUI.ShowDialog();
      Show();
    }

    private void OnClickBack(object sender, EventArgs e) {
      Close();
    }
  }
}
