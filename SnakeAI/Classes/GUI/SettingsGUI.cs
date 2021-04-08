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
using Point = System.Drawing.Point;

namespace SnakeAI {
  class SettingsGUI : Form {

    private GeneticSettings geneticSettings;
    private NetworkSettings networkSettings;
    private SnakeSettings snakeSettings;

    private SnakeAISettings snakeAISettings;

    private ListedGeneticSettingsGUI listedGeneticSettings;
    private ListedNetworkSettingsGUI listedNetworkSettings;
    private ListedSnakeSettingsGUI listedSnakeSettings;

    public SettingsGUI(SnakeAISettings snakeAISettings) {
      this.snakeAISettings = snakeAISettings;

      this.geneticSettings = snakeAISettings.GeneticSettings;
      this.networkSettings = snakeAISettings.NetworkSettings;
      this.snakeSettings = snakeAISettings.SnakeSettings;
      
      StartPosition = FormStartPosition.CenterScreen;

      InitializeComponent();
    }

    private void InitializeComponent() {
      Icon = SystemIcons.Shield;

      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;

      MinimumSize = new Size(600, 400);

      AutoSize = true;
      AutoSizeMode = AutoSizeMode.GrowAndShrink;

      EnableDoubleBuffering();

      Text = "Settings";

      Label title = new Label();
      title.Dock = DockStyle.Fill;
      title.Text = "Title";
      title.Font = ConstantsGUI.FONT_HEADER;

      TableLayoutPanel buttonLayout = new TableLayoutPanel();
      buttonLayout.ColumnCount = 2;
      buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      buttonLayout.RowCount = 1;
      buttonLayout.Dock = DockStyle.Fill;

      Button applyButton = new Button();
      applyButton.Text = "Apply settings";
      applyButton.Click += OnClickSave;
      applyButton.Dock = DockStyle.Fill;
      
      Button exitButton = new Button();
      exitButton.Text = "Exit";
      exitButton.Click += OnClickExit;
      exitButton.Dock = DockStyle.Fill;
           
      buttonLayout.Controls.Add(applyButton, 0, 0);
      buttonLayout.Controls.Add(exitButton, 1, 0);
      
      TableLayoutPanel layout = new TableLayoutPanel();
      layout.AutoSize = true;
      layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      layout.Dock = DockStyle.Fill;
      layout.RowCount = 2;
      layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

      layout.Controls.Add(buttonLayout, 0, 0);
      layout.Controls.Add(CreateTabs(), 0, 1);

      Controls.Add(layout);
               
      PerformLayout();
    }

    private void EnableDoubleBuffering() {
      this.SetStyle(ControlStyles.DoubleBuffer |
         ControlStyles.UserPaint |
         ControlStyles.AllPaintingInWmPaint,
         true);
      this.UpdateStyles();
    }

    private TabControl CreateTabs() {
      TabControl tabs = new TabControl();
      tabs.Dock = DockStyle.Fill;
      tabs.Font = ConstantsGUI.FONT_SMALL;

      listedGeneticSettings = new ListedGeneticSettingsGUI(snakeAISettings, true);
      listedNetworkSettings = new ListedNetworkSettingsGUI(snakeAISettings, true);
      listedSnakeSettings = new ListedSnakeSettingsGUI(snakeAISettings, true);

      TabPage geneticTab = new TabPage();
      geneticTab.Text = "Genetic Algorithm";
      geneticTab.Controls.Add(CreateLayout("Genetic Algorithm",
                                            listedGeneticSettings));
      
      TabPage networkTab = new TabPage();
      networkTab.Text = "Neural Network";
      networkTab.Controls.Add(CreateLayout("Neural Network",
                                            listedNetworkSettings));
      
      TabPage snakeTab = new TabPage();
      snakeTab.Text = "Snake Game";
      snakeTab.Controls.Add(CreateLayout("Snake Game",
                                          listedSnakeSettings));
      
      tabs.Controls.Add(geneticTab);
      tabs.Controls.Add(networkTab);
      tabs.Controls.Add(snakeTab);

      return tabs;
    }

    private TableLayoutPanel CreateLayout(string text, Control control) {
      TableLayoutPanel layout = new TableLayoutPanel();
      layout.RowCount = 2;
      layout.ColumnCount = 1;

      layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
      layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

      layout.AutoSize = true;
      layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;

      layout.HorizontalScroll.Maximum = 0;
      layout.AutoScroll = false;
      layout.VerticalScroll.Visible = false;
      layout.AutoScroll = true;

      layout.Dock = DockStyle.Fill;

      Label title = new Label();
      title.Text = text;
      title.Anchor = AnchorStyles.Top;
      title.AutoSize = true;
      title.Font = ConstantsGUI.FONT_HEADER;

      layout.Controls.Add(title, 0, 0);
      layout.Controls.Add(control, 0, 1);
      
      return layout;
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
      saveButton.Click += OnClickSave; // Fjern comment når impl.
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

      listedGeneticSettings = new ListedGeneticSettingsGUI(snakeAISettings, true);
      listedNetworkSettings = new ListedNetworkSettingsGUI(snakeAISettings, true);
      listedSnakeSettings = new ListedSnakeSettingsGUI(snakeAISettings, true);

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

    public void SaveSettings() {
      listedNetworkSettings.SaveSettings();
      listedGeneticSettings.SaveSettings();
      listedSnakeSettings.SaveSettings();
      MessageBox.Show("Settings applied!");

    }
  }
}
