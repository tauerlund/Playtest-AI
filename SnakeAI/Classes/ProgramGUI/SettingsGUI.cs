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
using GeneticAlgo.GeneticAlgorithmGUI;
using Point = System.Drawing.Point;

namespace Main.ProgramGUI {
  class SettingsGUI : Form {

    private GeneticSettings geneticSettings;
    private NetworkSettings networkSettings;
    private SnakeSettings snakeSettings;

    private ListedGeneticSettingsGUI listedGeneticSettings;
    private ListedNetworkSettingsGUI listedNetworkSettings;
    private ListedSnakeSettingsGUI listedSnakeSettings;

    public SettingsGUI(GeneticSettings geneticSettings, NetworkSettings networkSettings, SnakeSettings snakeSettings) {
      this.geneticSettings = geneticSettings;
      this.networkSettings = networkSettings;
      this.snakeSettings = snakeSettings;         

      InitializeComponent();
    }

    private void InitializeComponent() {
        Icon = SystemIcons.Shield;

      MinimumSize = new Size(500, 100);
      MaximumSize = new Size(500, 600);

      AutoSize = true;
      AutoSizeMode = AutoSizeMode.GrowAndShrink;

      EnableDoubleBuffering();

      Text = "Settings";

      Controls.Add(CreateTableLayout());
               
      PerformLayout(); // Not sure why
    }

    private void EnableDoubleBuffering() {
      // Set the value of the double-buffering style bits to true.
      this.SetStyle(ControlStyles.DoubleBuffer |
         ControlStyles.UserPaint |
         ControlStyles.AllPaintingInWmPaint,
         true);
      this.UpdateStyles();
    }

    private TableLayoutPanel CreateTableLayout() {
      TableLayoutPanel layout = new TableLayoutPanel();
      TableLayoutPanel buttonsLayout = new TableLayoutPanel();

      buttonsLayout.Height = 30;
      buttonsLayout.AutoSize = true;
      buttonsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      buttonsLayout.Dock = DockStyle.Top;

      buttonsLayout.ColumnCount = 2;
      buttonsLayout.RowCount = 1;

      buttonsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

      buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

      Button saveButton = new Button();
      saveButton.Click += OnClickSave;
      saveButton.Dock = DockStyle.Top;
      saveButton.Text = "Apply changes";

      Button exitButton = new Button();
      exitButton.Click += OnClickExit;
      exitButton.Dock = DockStyle.Top;
      exitButton.Text = "Exit";

      buttonsLayout.Controls.Add(saveButton);
      buttonsLayout.Controls.Add(exitButton);

      layout.AutoSize = true;
      layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      layout.AutoScroll = true;
      layout.AutoScrollMinSize = new Size(0, 600);
      layout.Dock = DockStyle.Fill;

      layout.ColumnCount = 1;
      layout.RowCount = 7;

      Label geneticTitle = new Label();
      geneticTitle.Text = "Genetic Algorithm";
      geneticTitle.Anchor = AnchorStyles.Top;
      geneticTitle.AutoSize = true;
      geneticTitle.Font = ConstantsGUI.FONT_HEADER;

      Label networkTitle = new Label();
      networkTitle.Text = "Neural Network";
      networkTitle.Anchor = AnchorStyles.Top;
      networkTitle.AutoSize = true;
      networkTitle.Font = ConstantsGUI.FONT_HEADER;

      Label snakeTitle = new Label();
      snakeTitle.Text = "Snake Game";
      snakeTitle.Anchor = AnchorStyles.Top;
      snakeTitle.AutoSize = true;
      snakeTitle.Font = ConstantsGUI.FONT_HEADER;

      listedGeneticSettings = new ListedGeneticSettingsGUI(geneticSettings, true);
      listedNetworkSettings = new ListedNetworkSettingsGUI(networkSettings, true);
      listedSnakeSettings = new ListedSnakeSettingsGUI(snakeSettings, true);

      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

      layout.Controls.Add(geneticTitle, 0, 0);
      layout.Controls.Add(listedGeneticSettings, 0, 1);
      layout.Controls.Add(networkTitle, 0, 2);
      layout.Controls.Add(listedNetworkSettings, 0, 3);
      layout.Controls.Add(snakeTitle, 0, 4);
      layout.Controls.Add(listedSnakeSettings, 0, 5);
      layout.Controls.Add(buttonsLayout, 0, 6);

      return layout;
    } 

    public void OnClickSave(object sender, EventArgs e) {
      SaveSettings();
    }

    public void OnClickExit(object sender, EventArgs e) {
      DialogResult result = MessageBox.Show("Are you sure you wish to exit? Unsaved changes will be lost.", "WARNING", MessageBoxButtons.YesNo);

      if(result == DialogResult.Yes) {
        Close();
      }
    }

    private void SaveSettings() {
      listedNetworkSettings.SaveSettings();
      networkSettings.numberOfWeights = networkSettings.CalculateNumberOfWeights();
      geneticSettings.GeneCount = networkSettings.numberOfWeights;
      listedGeneticSettings.SaveSettings();
      listedSnakeSettings.SaveSettings();

      MessageBox.Show("Settings applied!");
    }
  }
}
