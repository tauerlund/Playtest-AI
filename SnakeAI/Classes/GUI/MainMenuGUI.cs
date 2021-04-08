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
using LevelEditorNS;

namespace SnakeAI {
  class MainMenuGUI : Form {

    public GeneticAlgorithm GeneticAlgorithm { get; set; }
    public GeneticSettings GeneticSettings { get; set; }

    public NeuralNetwork NeuralNetwork { get; set; }
    public NetworkSettings NetworkSettings { get; set; }

    public SnakeGame SnakeGame { get; set; }
    public SnakeSettings SnakeSettings { get; set; }

    public SnakeAISettings SnakeAISettings { get; set; }

    public MainMenuGUI() {
      Width = 450;
      Height = 400;
      StartPosition = FormStartPosition.CenterScreen;

      Text = "Snake AI Playtest";

      NetworkSettings = MakeNetworkSettings();
      SnakeSettings = MakeSnakeSettings();
      GeneticSettings = MakeGeneticSettings(NetworkSettings, SnakeSettings);

      SnakeAISettings = new SnakeAISettings(GeneticSettings, NetworkSettings, SnakeSettings);

      InitializeComponent();
    }

    private void InitializeComponent() {
      TableLayoutPanel layout = new TableLayoutPanel();

      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;

      layout.AutoSize = true;
      layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      layout.RowCount = 5;
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
      layout.Dock = DockStyle.Fill;

      Padding padding = new Padding();
      padding.All = 20;
      layout.Padding = padding;

      Label title = new Label();
      title.Dock = DockStyle.Bottom;
      title.TextAlign = ContentAlignment.MiddleCenter;
      title.Font = ConstantsGUI.FONT_HEADER;
      title.Text = "Welcome to Snake AI Playtest!";

      layout.Controls.Add(title, 0, 0);
      layout.Controls.Add(new ButtonGUI("Playtest", OnClickPlaytest), 0, 1);
      layout.Controls.Add(new ButtonGUI("Train AI", OnClickTrainAI), 0, 2);
      layout.Controls.Add(new ButtonGUI("Level Editor", OnClickLevelEditor), 0, 3);
      layout.Controls.Add(new ButtonGUI("Exit", OnClickExit), 0, 4);

      Controls.Add(layout);
    }

    private void OnClickPlaytest(object sender, EventArgs e) {
      PlaytestGUI playtestGUI = new PlaytestGUI();
      Hide();
      playtestGUI.ShowDialog();
      Show();
    }

    private void OnClickTrainAI(object sender, EventArgs e) {
      TrainSnakeGUI trainSnakeGUI = new TrainSnakeGUI(SnakeAISettings);
      Hide();
      trainSnakeGUI.ShowDialog();
      Show();
    }

    private void OnClickLevelEditor(object sender, EventArgs e) {
      SnakeLevelEditor snakeLevelEditor = new SnakeLevelEditor(SnakeLevelEditor.ConvertSettingsLogicToGUI(SnakeSettings));
      Hide();
      snakeLevelEditor.ShowDialog();
      Show();
    }

    private void OnClickExit(object sender, EventArgs e) {
      Application.Exit();
    }

    private GeneticSettings MakeGeneticSettings(NetworkSettings networkSettings, SnakeSettings snakeSettings) {

      GeneticSettings geneticSettings = new GeneticSettings(ProgramSettings.POPULATION_SIZE,
                                                            networkSettings.numberOfWeights,
                                                            ProgramSettings.SELECTION_SIZE,
                                                            ProgramSettings.MUTATION_PROBABALITY_AGENT,
                                                            ProgramSettings.MUTATION_PROBABILITY_GENE,
                                                            ProgramSettings.RANDOM_GENERATOR,
                                                            new SnakeFitnessCalculator(networkSettings, snakeSettings),
                                                            ProgramSettings.MUTATION_METHOD,
                                                            ProgramSettings.SELECTION_METHOD,
                                                            ProgramSettings.CROSSOVER_METHOD,
                                                            ProgramSettings.AGENT_VIEWER,
                                                            ProgramSettings.TOP_K_AGENTS_COUNT);
      return geneticSettings;
    }

    private NetworkSettings MakeNetworkSettings() {
      NetworkSettings networkSettings = new NetworkSettings(ProgramSettings.NUMBER_OF_INPUT_NEURONS,
                                                            ProgramSettings.HIDDEN_LAYER_STRUCTURE,
                                                            ProgramSettings.OUTPUT_LABELS);
      return networkSettings;
    }

    private SnakeSettings MakeSnakeSettings() {
      SnakeSettings snakeSettings = new SnakeSettings(ProgramSettings.GRID_ROWS, ProgramSettings.GRID_COLUMNS, ProgramSettings.SIDE_LENGTH,
                                                      ProgramSettings.POINTS_PER_FOOD_EATEN, ProgramSettings.RANDOM_GENERATOR);
      return snakeSettings;
    }
  }
}
