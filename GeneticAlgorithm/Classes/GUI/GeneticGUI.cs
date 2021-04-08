using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using Timer = System.Windows.Forms.Timer;
using GeneticAlgo.GeneticAlgorithmGUI;
using System.Diagnostics;
using System.IO;

namespace GeneticAlgorithmNS {

  public class GeneticGUI : Form {

    private readonly GeneticAlgorithm geneticAlgorithm;
    private readonly GeneticSettings geneticSettings;

    private Timer timer;

    private Label generationCounter;
    private Label currentBestFitness;
    private Label elapsedTime;
    private Label averageFitness;
    private Label medianFitness;
    private Label totalAgentsCounter;
    private Label algorithmState;

    private Button pauseButton;

    private FitnessGraph fitnessGraph;
    private double currentBest;
    private int lastGen;

    private Font fontLarge;
    private Font fontSmall;
    private Font fontButton;

    private Color borderColor;

    private int windowWidth;
    private int windowHeight;

    private AgentStorageManager agentStorageManager;

    public GeneticGUI(GeneticAlgorithm geneticAlgorithm, GeneticSettings geneticSettings) : this(geneticAlgorithm,
                                                                                                 geneticSettings,
                                                                                                 new Size(700, 500)) {

    }

    public GeneticGUI(GeneticAlgorithm geneticAlgorithm, GeneticSettings geneticSettings, Size size) {
      this.geneticAlgorithm = geneticAlgorithm;
      this.geneticSettings = geneticSettings;

      agentStorageManager = new AgentStorageManager();

      fontLarge = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      fontSmall = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      fontButton = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

      borderColor = SystemColors.ControlDark;

      AutoSize = true;
      AutoSizeMode = AutoSizeMode.GrowAndShrink;

      StartPosition = FormStartPosition.CenterScreen;

      Size = size;

      windowWidth = 1200;
      windowHeight = 650;

      MaximumSize = new Size(windowWidth, windowHeight);

      InitializeComponent();

      timer = new Timer();
      timer.Interval = 1;
      timer.Start();
      timer.Tick += UpdateGraph;

      FormClosed += OnFormClosed;
    }

    private void UpdateGraph(Object sender, EventArgs e) {

      elapsedTime.Text = $"{geneticAlgorithm.RunTimeStopWatch.Elapsed.ToString(@"hh\:mm\:ss")}--";
      totalAgentsCounter.Text = $"{Agent.TotalFitnessCalculations.ToString()}";

      if (geneticAlgorithm.TopKAgents.Best.Agent != null) {
        currentBest = geneticAlgorithm.TopKAgents.Best.Agent.Fitness;
      }

      if (geneticAlgorithm.GenerationCount != lastGen) {

        generationCounter.Text = $"{geneticAlgorithm.GenerationCount}";
        currentBestFitness.Text = $"{currentBest}";
        medianFitness.Text = String.Format("{0:N2}%", geneticAlgorithm.ConvergencePercent.ToString());

        fitnessGraph.UpdateGraph(0, geneticAlgorithm.GenerationCount, geneticAlgorithm.CurrentAverageFitnessPopulation);
        fitnessGraph.UpdateGraph(1, geneticAlgorithm.GenerationCount, geneticAlgorithm.CurrentAverageFitnessSelection);
        fitnessGraph.UpdateGraph(2, geneticAlgorithm.GenerationCount, currentBest);
      }
      lastGen = geneticAlgorithm.GenerationCount;
      averageFitness.Text = geneticAlgorithm.CurrentAverageFitnessPopulation.ToString();
    }

    #region CREATE LAYOUT
    private void InitializeComponent() {

      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;

      Text = "Genetic Algorithm";
      Icon = SystemIcons.Exclamation;

      AutoScaleMode = AutoScaleMode.Dpi;

      TableLayoutPanel overallLayout = CreateLayout(2, 1, 10);
      overallLayout.BackColor = SystemColors.Control;

      TableLayoutPanel upperLayout = CreateLayout(1, 2, 10);
      upperLayout.BackColor = Color.Transparent;

      TableLayoutPanel counterLayout = CreateCounterLayout();
      counterLayout.BackColor = Color.Transparent;

      TableLayoutPanel settingsLayout = CreateSettingsLayout();
      settingsLayout.BackColor = Color.White;

      TableLayoutPanel graphLayout = CreateGraphLayout();
      graphLayout.BackColor = Color.Transparent;

      upperLayout.Controls.Add(counterLayout, 0, 0);
      upperLayout.Controls.Add(settingsLayout, 1, 0);

      overallLayout.Controls.Add(upperLayout, 0, 0);
      overallLayout.Controls.Add(graphLayout, 0, 1);

      Controls.Add(overallLayout);

      PerformLayout();

    }

    private TableLayoutPanel CreateLayout(int rowCount, int columnCount, int padding) {

      Padding margin = new Padding();
      margin.All = padding;

      TableLayoutPanel layout = new TableLayoutPanel();
      layout.RowCount = rowCount;
      layout.ColumnCount = columnCount;
      layout.BackColor = Color.White;

      for (int i = 0; i < rowCount; i++) {
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, (100 / rowCount)));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, (100 / rowCount)));
      }

      for (int i = 0; i < columnCount; i++) {
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (100 / columnCount)));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (100 / columnCount)));
      }

      layout.Dock = DockStyle.Fill;
      layout.AutoSize = true;
      layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;

      return layout;
    }

    private TableLayoutPanel CreateUpperRightLayout() {
      TableLayoutPanel upperRightLayout = CreateLayout(3, 1, 0);
      upperRightLayout.RowStyles[0] = new RowStyle(SizeType.AutoSize);
      upperRightLayout.RowStyles[1] = new RowStyle(SizeType.AutoSize);
      upperRightLayout.RowStyles[2] = new RowStyle(SizeType.AutoSize);

      Label title = CreateLabel("Genetic Algorithm Settings", fontLarge);

      TableLayoutPanel settingsLayout = CreateSettingsLayout();

      upperRightLayout.Controls.Add(title, 0, 0);
      upperRightLayout.Controls.Add(settingsLayout, 0, 1);

      return upperRightLayout;
    }

    private TableLayoutPanel CreateSettingsLayout() {
      TableLayoutPanel settingsLayout = CreateLayout(9, 2, 10);

      settingsLayout.Paint += PaintBorder;
      settingsLayout.CellPaint += TableLayoutPaintBorderNew;

      ListViewItem crossOverMethod = new ListViewItem("Crossover method");
      crossOverMethod.SubItems.Add(": " + geneticSettings.Crossover.ToString());
      crossOverMethod.ToolTipText = "The chosen method for crossover."; // BESKRIV SPECIFIK METHOD

      Label title = CreateLabel("Genetic Algorithm Settings", fontLarge);
      title.Anchor = AnchorStyles.None;
      title.TextAlign = ContentAlignment.MiddleCenter;

      Label populationSizeText = CreateLabel("Population size:", fontSmall);
      Label selectionSizeText = CreateLabel("Selection size:", fontSmall);
      Label geneMutationChanceText = CreateLabel("Gene mutation chance:", fontSmall);
      Label agentMutationChanceText = CreateLabel("Agent mutation chance:", fontSmall);
      Label chromosomeSizeText = CreateLabel("Chromesome size:", fontSmall);
      Label selectionMethodText = CreateLabel("Selection method:", fontSmall);
      Label crossoverMethodText = CreateLabel("Crossover method:", fontSmall);

      Label populationSizeValue = CreateLabel(geneticSettings.PopulationSize.ToString(),
                                              fontSmall);
      NumericUpDown selectionSizeValue = CreateNumericUpDown(geneticSettings.SelectionSize.ToString(),
                                                             fontSmall, false);
      selectionSizeValue.Maximum = geneticSettings.PopulationSize - 1;
      NumericUpDown geneMutationChanceValue = CreateNumericUpDownPercent((geneticSettings.MutationProbabilityGenes * 100).ToString(),
                                                                          fontSmall, false);
      NumericUpDown agentMutationChanceValue = CreateNumericUpDownPercent((geneticSettings.MutationProbabiltyAgents * 100).ToString(),
                                                                           fontSmall, false);
      Label chromosomeSizeValue = CreateLabel(geneticSettings.GeneCount.ToString(),
                                              fontSmall);
      Label selectionMethodValue = CreateLabel(geneticSettings.Selector.ToString(),
                                               fontSmall);
      Label crossoverMethodValue = CreateLabel(geneticSettings.Crossover.ToString(),
                                               fontSmall);


      TableLayoutPanel checkBoxTable = CreateLayout(1, 2, 0);
      checkBoxTable.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
      checkBoxTable.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);

      settingsLayout.Controls.Add(title, 0, 0);
      settingsLayout.SetColumnSpan(title, 2);

      settingsLayout.Controls.Add(populationSizeText, 0, 1);
      settingsLayout.Controls.Add(selectionSizeText, 0, 2);
      settingsLayout.Controls.Add(geneMutationChanceText, 0, 3);
      settingsLayout.Controls.Add(agentMutationChanceText, 0, 4);
      settingsLayout.Controls.Add(chromosomeSizeText, 0, 5);
      settingsLayout.Controls.Add(selectionMethodText, 0, 6);
      settingsLayout.Controls.Add(crossoverMethodText, 0, 7);

      settingsLayout.Controls.Add(checkBoxTable, 0, 8);
      settingsLayout.SetColumnSpan(checkBoxTable, 2);

      settingsLayout.Controls.Add(populationSizeValue, 1, 1);
      settingsLayout.Controls.Add(selectionSizeValue, 1, 2);
      settingsLayout.Controls.Add(geneMutationChanceValue, 1, 3);
      settingsLayout.Controls.Add(agentMutationChanceValue, 1, 4);
      settingsLayout.Controls.Add(chromosomeSizeValue, 1, 5);
      settingsLayout.Controls.Add(selectionMethodValue, 1, 6);
      settingsLayout.Controls.Add(crossoverMethodValue, 1, 7);

      return settingsLayout;
    }

    private TableLayoutPanel CreateCounterLayout() {

      Padding padding = new Padding();
      padding.All = 0;

      TableLayoutPanel counterLayout = CreateLayout(2, 1, 10);
      counterLayout.Margin = padding;

      TableLayoutPanel generationCounterLayout = CreateLayout(5, 2, 0);
      generationCounterLayout.Paint += PaintBorder;

      algorithmState = CreateLabel("Algorithm running", fontLarge);
      algorithmState.Anchor = AnchorStyles.None;
      algorithmState.TextAlign = ContentAlignment.MiddleCenter;
      generationCounterLayout.Controls.Add(algorithmState, 0, 0);
      generationCounterLayout.SetColumnSpan(algorithmState, 2);

      generationCounter = CreateLabel("", fontSmall);
      Label generationCounterText = CreateLabel("Current generation:", fontSmall);
      generationCounterLayout.Controls.Add(generationCounterText, 0, 1);
      generationCounterLayout.Controls.Add(generationCounter, 1, 1);

      elapsedTime = CreateLabel("", fontSmall);
      Label elapsedTimeText = CreateLabel("Total time elapsed:", fontSmall);
      generationCounterLayout.Controls.Add(elapsedTimeText, 0, 2);
      generationCounterLayout.Controls.Add(elapsedTime, 1, 2);

      totalAgentsCounter = CreateLabel("", fontSmall);
      Label totalAgentsCounterText = CreateLabel("Total number of agents:", fontSmall);
      generationCounterLayout.Controls.Add(totalAgentsCounterText, 0, 3);
      generationCounterLayout.Controls.Add(totalAgentsCounter, 1, 3);

      pauseButton = CreateButton("Pause Algorithm", PauseAlgorithmClick);
      Button exitButton = CreateButton("Exit Algorithm", menuItemExit_Click);

      generationCounterLayout.Controls.Add(pauseButton, 0, 4);
      generationCounterLayout.Controls.Add(exitButton, 1, 4);

      TableLayoutPanel agentsCounterLayout = CreateLayout(4, 2, 0);
      agentsCounterLayout.Paint += PaintBorder;

      currentBestFitness = CreateLabel("", fontSmall);
      Label currentBestFitnessText = CreateLabel("Current best agent fitness:", fontSmall);
      agentsCounterLayout.Controls.Add(currentBestFitnessText, 0, 0);
      agentsCounterLayout.Controls.Add(currentBestFitness, 1, 0);

      averageFitness = CreateLabel("", fontSmall);
      Label averageFitnessText = CreateLabel("Population average fitness:", fontSmall);
      agentsCounterLayout.Controls.Add(averageFitnessText, 0, 1);
      agentsCounterLayout.Controls.Add(averageFitness, 1, 1);

      medianFitness = CreateLabel("", fontSmall);
      Label medianFitnessText = CreateLabel("Selection convergence percent:", fontSmall);
      agentsCounterLayout.Controls.Add(medianFitnessText, 0, 2);
      agentsCounterLayout.Controls.Add(medianFitness, 1, 2);

      Button playButton = CreateButton("Show best agent", PlayBestClick);
      Button saveButton = CreateButton("Save best agent", SaveBestClick);
      agentsCounterLayout.Controls.Add(playButton, 0, 3);
      agentsCounterLayout.Controls.Add(saveButton, 1, 3);

      counterLayout.Controls.Add(generationCounterLayout, 0, 0);
      counterLayout.Controls.Add(agentsCounterLayout, 0, 1);

      return counterLayout;
    }

    private Button CreateButton(string text, EventHandler onClick) {
      Button button = new Button();
      button.AutoSize = true;
      button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      button.Dock = DockStyle.Fill;
      button.Text = text;
      button.Font = fontSmall;
      button.Click += onClick;
      return button;
    }

    private NumericUpDown CreateNumericUpDown(string text, Font font) {
      return CreateNumericUpDown(text, font, true);
    }

    private NumericUpDown CreateNumericUpDown(string text, Font font, bool enabled) {
      NumericUpDown numericUpDown = new NumericUpDown();
      numericUpDown.Maximum = int.MaxValue;
      numericUpDown.AutoSize = true;
      numericUpDown.Dock = DockStyle.Fill;
      numericUpDown.Text = text;
      numericUpDown.Font = fontSmall;
      numericUpDown.Enabled = enabled;
      return numericUpDown;
    }

    private CustomNumericUpDown CreateNumericUpDownPercent(string text, Font font, bool enabled) {
      CustomNumericUpDown numericUpDown = new CustomNumericUpDown();
      numericUpDown.AutoSize = true;
      numericUpDown.Dock = DockStyle.Fill;
      numericUpDown.Text = text;
      numericUpDown.Font = fontSmall;
      numericUpDown.Enabled = enabled;
      numericUpDown.DecimalPlaces = 2;
      return numericUpDown;
    }

    private Label CreateLabel(string text, Font font, string toolTip) {
      return CreateLabel(text, font, AnchorStyles.Left, toolTip);
    }

    private Label CreateLabel(string text, Font font, AnchorStyles anchor) {
      return CreateLabel(text, font, anchor, "");
    }

    private Label CreateLabel(string text, Font font) {
      return CreateLabel(text, font, AnchorStyles.Left, "");
    }

    private Label CreateLabel(string text, Font font, AnchorStyles anchor, string toolTip) {
      Label label = new Label();
      label.BackColor = Color.Transparent;
      label.Font = font;
      label.Anchor = anchor;
      label.AutoSize = true;
      label.Text = text;
      new ToolTip().SetToolTip(label, toolTip);
      return label;
    }

    private TableLayoutPanel CreateGraphLayout() {
      TableLayoutPanel graphLayout = CreateLayout(1, 1, 20);

      fitnessGraph = new FitnessGraph(new Size(windowWidth, windowHeight / 2));
      ((System.ComponentModel.ISupportInitialize)(fitnessGraph)).BeginInit(); // Dunno what this does

      graphLayout.Controls.Add(fitnessGraph);

      return graphLayout;
    }
    #endregion

    #region EVENT HANDLERS
    private void OnFormClosed(object sender, EventArgs e) {
      geneticAlgorithm.IsPaused = false;
      geneticAlgorithm.IsRunning = false;
    }

    private void PaintBorder(object sender, PaintEventArgs e) {
      e.Graphics.DrawRectangle(new Pen(borderColor),
                               new Rectangle(0, 0, ((TableLayoutPanel)sender).Width - 1,
                               ((TableLayoutPanel)sender).Height - 1));
    }

    private void TableLayoutPaintBorder(object sender, PaintEventArgs e) {
      TableLayoutPanel table = ((TableLayoutPanel)sender);
      Graphics g = table.CreateGraphics();
      foreach (Control c in table.Controls) {
        Rectangle rec = c.Bounds;
        rec.Inflate(2, 2);
        ControlPaint.DrawBorder(g, rec, borderColor, ButtonBorderStyle.Solid);
      }
    }

    private void TableLayoutPaintBorderNew(object sender, TableLayoutCellPaintEventArgs e) {

      TableLayoutPanel table = (TableLayoutPanel)sender;

      if (e.Row != 0 && e.Row != 8) {
        e.Graphics.DrawRectangle(new Pen(borderColor), e.CellBounds);
      }
    }

    // SKAL KUNNE STOPPE ALGO HER
    private void menuItemExit_Click(object sender, EventArgs e) {
      DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit? Unsaved agents will be lost.", "WARNING", MessageBoxButtons.YesNo);

      if (dialogResult == DialogResult.Yes) {
        geneticAlgorithm.IsPaused = false;
        geneticAlgorithm.IsRunning = false;
        Close();
      }
    }

    private void PauseAlgorithmClick(object sender, EventArgs e) {
      geneticAlgorithm.IsPaused = true;
      algorithmState.Text = "Algorithm paused";
      pauseButton.Text = "Resume algorithm";
      pauseButton.Click += ResumeAlgorithmClick;
    }

    private void ResumeAlgorithmClick(object sender, EventArgs e) {
      geneticAlgorithm.IsPaused = false;
      algorithmState.Text = "Algorithm running";
      pauseButton.Text = "Pause algorithm";
      pauseButton.Click += PauseAlgorithmClick;
    }

    private void PlayBestClick(object sender, EventArgs e) {
      if(geneticAlgorithm.IsPaused) {
        Agent bestAgent = geneticAlgorithm.TopKAgents.Best.Agent;
        geneticSettings.AgentViewer.ViewAgent(bestAgent, geneticSettings);
      }
      else {
        MessageBox.Show("Show agent failed! Please pause algorithm before trying to view agent.");
      }
    }

    private void SaveBestClick(object sender, EventArgs e) {
      geneticAlgorithm.IsPaused = true;

      Agent bestAgent = geneticAlgorithm.TopKAgents.Best.Agent;

      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.RestoreDirectory = true;
      saveFileDialog.InitialDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\SavedAgents\"));

      if (saveFileDialog.ShowDialog() == DialogResult.OK) {
        agentStorageManager.SaveAgentBinary(bestAgent, geneticSettings, saveFileDialog.FileName);
      }
      geneticAlgorithm.IsPaused = false;
    }
    #endregion
  }

  public class CustomNumericUpDown : NumericUpDown {
    protected override void UpdateEditText() {
      Text = Value.ToString() + "%";
    }
  }
}