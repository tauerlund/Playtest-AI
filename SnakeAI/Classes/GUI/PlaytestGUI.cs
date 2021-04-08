using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using SnakeGameNS;
using NeuralNetworkNS;
using GeneticAlgorithmNS;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using Point = System.Drawing.Point;
using HelperMethods;

namespace SnakeAI {
  class PlaytestGUI : Form {

    public TextBox openAgentTextBox { get; set; }
    public TextBox openLevelTextBox { get; set; }
    public ComboBox playtestTypesList { get; set; }
    public NumericUpDown numberOfSimulations { get; set; }

    public Label failedSimulationsAbsolute { get; set; }
    public Label failedSimulationsPercent { get; set; }
    public Button showAllResults { get; set; }

    public Chart resultsChart { get; set; }
    public Series chartSeriesSuccess { get; set; }
    public Series chartSeriesFailed { get; set; }

    private SavedAgent agent;
    private SnakeLevel level;

    public TableLayoutPanel resultsLayout { get; set; }

    public PlaytestResults playtestResults { get; set; }

    private SnakeSettings SnakeSettings { get; set; }
    private NetworkSettings NetworkSettings { get; set; }

    private RandomNumberGenerator randomNumberGenerator;

    public PlaytestGUI() {
      Width = 800;
      Height = 380;

      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;

      Text = "Snake Playtest";

      Location = new Point(20, 20);

      randomNumberGenerator = new RandomNumberGenerator();

      InitializeComponent();
    }

    #region CREATE LAYOUT
    private void InitializeComponent() {

      TableLayoutPanel layout = CreateLayout(2, 1, SizeType.Percent);
      GroupBox resultsBox = CreateGroupBox("Playtest results");

      resultsLayout = CreateLayout(1, 4, SizeType.Percent);
      resultsLayout.Hide();
      resultsLayout.RowStyles[0].Height = 15F;
      resultsLayout.RowStyles[1].Height = 15F;
      resultsLayout.RowStyles[2].Height = 20F;
      resultsLayout.RowStyles[3].Height = 50F;

      failedSimulationsAbsolute = CreateLabel("Number of failed tests:", ConstantsGUI.FONT_SMALL);
      failedSimulationsPercent = CreateLabel("Percentage of tests failed:", ConstantsGUI.FONT_SMALL);
      showAllResults = new ButtonGUI("Show all test results", OnClickShowAllResult);
      showAllResults.Font = ConstantsGUI.FONT_SMALL;

      resultsChart = CreateChart(400);

      resultsLayout.Controls.Add(failedSimulationsAbsolute, 0, 0);
      resultsLayout.Controls.Add(failedSimulationsPercent, 0, 1);
      resultsLayout.Controls.Add(showAllResults, 0, 2);
      resultsLayout.Controls.Add(resultsChart, 0, 3);

      resultsBox.Controls.Add(resultsLayout);

      TableLayoutPanel layoutRight = CreateLayout(1, 1, SizeType.Percent);
      layoutRight.Controls.Add(resultsBox, 0, 0);

      TableLayoutPanel layoutLeft = CreateLayout(1, 2, SizeType.Percent);
      layoutLeft.RowStyles[0].Height = 80F;
      layoutLeft.RowStyles[1].Height = 20F;

      GroupBox settingsBox = CreateGroupBox("Playtest settings");
      Button startPlaytestButton = new ButtonGUI("Start Playtest", OnClickStartPlaytest);
      startPlaytestButton.Font = ConstantsGUI.FONT_SMALL;

      TableLayoutPanel settingsLayout = CreateLayout(2, 4, SizeType.Percent);

      Label playtestTypeText = CreateLabel("Choose playtest type:", ConstantsGUI.FONT_SMALL);
      Label loadAgentText = CreateLabel("Choose agent:", ConstantsGUI.FONT_SMALL);
      Label loadLevelText = CreateLabel("Choose level:", ConstantsGUI.FONT_SMALL);
      Label numberOfPlaytestsText = CreateLabel("Number of simulations:", ConstantsGUI.FONT_SMALL);

      List<string> listTest = new List<string>();
      foreach (var item in Enum.GetNames(typeof(TestType))) {
        listTest.Add(item);
      }

      TableLayoutPanel openAgentLayout = CreateLayout(2, 1, SizeType.Percent);
      openAgentLayout.ColumnStyles[0].Width = 70F;
      openAgentLayout.ColumnStyles[1].Width = 30F;

      playtestTypesList = CreateComboBox(listTest);
      openAgentTextBox = CreateTextBox("");
      ButtonGUI openAgentButton = new ButtonGUI("...", OnClickOpenAgent);
      openAgentButton.Font = ConstantsGUI.FONT_SMALL;

      openAgentLayout.Controls.Add(openAgentTextBox, 0, 0);
      openAgentLayout.Controls.Add(openAgentButton, 1, 0);

      TableLayoutPanel openLevelLayout = CreateLayout(2, 1, SizeType.Percent);
      openLevelLayout.ColumnStyles[0].Width = 70F;
      openLevelLayout.ColumnStyles[1].Width = 30F;

      openLevelTextBox = CreateTextBox("");

      ButtonGUI openLevelButton = new ButtonGUI("...", OnClickOpenLevel);
      openLevelButton.Font = ConstantsGUI.FONT_SMALL;

      openLevelLayout.Controls.Add(openLevelTextBox, 0, 0);
      openLevelLayout.Controls.Add(openLevelButton, 1, 0);

      numberOfSimulations = CreateNumericUpDown();

      settingsLayout.Controls.Add(playtestTypeText, 0, 0);
      settingsLayout.Controls.Add(loadLevelText, 0, 1);
      settingsLayout.Controls.Add(loadAgentText, 0, 2);
      settingsLayout.Controls.Add(numberOfPlaytestsText, 0, 3);

      settingsLayout.Controls.Add(playtestTypesList, 1, 0);
      settingsLayout.Controls.Add(openLevelLayout, 1, 1);
      settingsLayout.Controls.Add(openAgentLayout, 1, 2);
      settingsLayout.Controls.Add(numberOfSimulations, 1, 3);

      settingsBox.Controls.Add(settingsLayout);

      layoutLeft.Controls.Add(settingsBox, 0, 0);
      layoutLeft.Controls.Add(startPlaytestButton, 0, 1);
           
      layout.Controls.Add(layoutLeft, 0, 0);
      layout.Controls.Add(layoutRight, 1, 0);

      Controls.Add(layout);
    }

    private Chart CreateChart(int width) {
      Chart chart = new Chart();
      chart.Width = width;
      chart.BorderlineColor = SystemColors.ControlDark;
      chart.BorderlineDashStyle = ChartDashStyle.Solid;

      ChartArea chartArea = new ChartArea();
      chartArea.Name = "chartArea";

      Legend legend = new Legend();
      legend.Name = "legend";

      chart.ChartAreas.Add(chartArea);
      chart.Legends.Add(legend);

      chartSeriesSuccess = new Series();
      chartSeriesSuccess.ChartType = SeriesChartType.StackedBar100;
      chartSeriesSuccess.Color = Color.Red;
      chartSeriesFailed = new Series();
      chartSeriesFailed.ChartType = SeriesChartType.StackedBar100;
      chartSeriesFailed.Color = Color.Green;

      chartSeriesSuccess.ChartArea = "chartArea";
      chartSeriesSuccess.Legend = "legend";
      chartSeriesSuccess.Name = "Failed";

      chartSeriesFailed.ChartArea = "chartArea";
      chartSeriesFailed.Legend = "legend";
      chartSeriesFailed.Name = "Passed";

      chart.Series.Add(chartSeriesSuccess);
      chart.Series.Add(chartSeriesFailed);

      return chart;
    }

    private NumericUpDown CreateNumericUpDown() {
      NumericUpDown numericUpDown = new NumericUpDown();

      numericUpDown.Dock = DockStyle.Fill;
      numericUpDown.AutoSize = true;
      numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      numericUpDown.Minimum = 1;
      numericUpDown.Maximum = 2000;

      return numericUpDown;
    }

    private TextBox CreateTextBox(string text) {
      TextBox textBox = new TextBox();

      textBox.Dock = DockStyle.Fill;
      textBox.Text = text;
      textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;

      return textBox;
    }

    private GroupBox CreateGroupBox(string title) {
      GroupBox groupBox = new GroupBox();

      groupBox.Dock = DockStyle.Fill;
      groupBox.AutoSize = true;
      groupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      groupBox.Text = title;
      groupBox.Font = ConstantsGUI.FONT_SMALL;

      return groupBox;
    }

    private ComboBox CreateComboBox<T>(List<T> list) {
      ComboBox comboBox = new ComboBox();

      comboBox.Dock = DockStyle.Fill;
      comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

      foreach (T obj in list) {
        comboBox.Items.Add(obj);
      }

      comboBox.SelectedIndex = 0;

      return comboBox;
    }

    private Label CreateLabel(string text, Font font) {
      return CreateLabel(text, font, AnchorStyles.Left);
    }

    private Label CreateLabel(string text, Font font, AnchorStyles anchor) {
      Label label = new Label();
      label.Text = text;
      label.AutoSize = true;
      label.Font = font;
      label.Anchor = anchor;
      return label;
    }

    private TableLayoutPanel CreateLayout(int columns, int rows, SizeType sizeType) {
      TableLayoutPanel layout = new TableLayoutPanel();
      layout.ColumnCount = columns;
      layout.RowCount = rows;
      layout.Dock = DockStyle.Fill;
      layout.AutoSize = true;
      layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;

      if (sizeType == SizeType.Percent) {
        for (int i = 0; i < rows; i++) {
          layout.RowStyles.Add(new RowStyle(sizeType, 100 / rows));
        }

        for (int i = 0; i < columns; i++) {
          layout.ColumnStyles.Add(new ColumnStyle(sizeType, 100 / columns));
        }
      }

      if (sizeType == SizeType.AutoSize) {
        for (int i = 0; i < rows; i++) {
          layout.RowStyles.Add(new RowStyle(sizeType));
        }

        for (int i = 0; i < columns; i++) {
          layout.ColumnStyles.Add(new ColumnStyle(sizeType));
        }
      }
      return layout;
    }
    #endregion
    
    private void OnClickStartPlaytest(object sender, EventArgs e) {
      try {
        AgentStorageManager storageManager = new AgentStorageManager();

        agent = storageManager.LoadAgentBinary(openAgentTextBox.Text);
        level = storageManager.LoadObject<SnakeLevel>(openLevelTextBox.Text);

        level.SnakeSettings.RandomNumberGenerator = new RandomNumberGenerator();

        SnakePlayTestSimulator snakePlayTestSimulator = new SnakePlayTestSimulator(level);
        Playtest playtest = new Playtest(snakePlayTestSimulator, (TestType)playtestTypesList.SelectedIndex);
        playtestResults = playtest.RunPlaytest(agent, Convert.ToInt32(numberOfSimulations.Value));

        failedSimulationsAbsolute.Text = $"Number of failed tests: {playtestResults.NumberOfFailedTests} of {playtestResults.TestResults.Count}";
        failedSimulationsPercent.Text = $"Percentage tests failed: {playtestResults.FailedPercentage}%";

        resultsChart.Series[0].Points.Clear();
        resultsChart.Series[1].Points.Clear();
        resultsChart.Series[0].Points.AddXY("Failed", playtestResults.FailedPercentage);
        resultsChart.Series[1].Points.AddXY("Passed", 100 - playtestResults.FailedPercentage);

        resultsLayout.Show();
      }
      catch (Exception exception) {
        MessageBox.Show($"Playtest failed! Error: {exception.Message}");
      }
    }

    private void OnClickOpenAgent(object sender, EventArgs e) {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.RestoreDirectory = true;
      openFileDialog.InitialDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\SavedAgents\"));

      if (openFileDialog.ShowDialog() == DialogResult.OK) {
        openAgentTextBox.Text = openFileDialog.FileName;
      }
    }

    private void OnClickOpenLevel(object sender, EventArgs e) {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.RestoreDirectory = true;
      openFileDialog.InitialDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\SavedLevels\"));

      if (openFileDialog.ShowDialog() == DialogResult.OK) {
        openLevelTextBox.Text = openFileDialog.FileName;
      }
    }

    private void OnClickShowAllResult(object sender, EventArgs e) {
      showAllResults.Enabled = false;

      PlaytestResultsDetailsGUI resultDetails = new PlaytestResultsDetailsGUI(playtestResults);
      resultDetails.StartPosition = FormStartPosition.Manual;
      resultDetails.Location = new Point(Location.X + Width, Location.Y);
      resultDetails.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a) {
        showAllResults.Enabled = true;
        });
      resultDetails.Show();
    }
  }
}
