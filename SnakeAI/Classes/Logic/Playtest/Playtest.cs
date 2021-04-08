using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticAlgorithmNS;
using NeuralNetworkNS;
using SnakeGameNS;

namespace SnakeAI {

  /// <summary>
  /// Represents a playtest. 
  /// </summary>
  public class Playtest {

    public int Progress { get; private set; }

    private TestType testType;
    private SnakePlayTestSimulator playTestSimulator;
    private int simulationCount;
    private ProgressBar progressBar;
    private Form progressForm;

    /// <summary>
    /// Initializes a new instance of the class <see cref="Playtest"/>
    /// based on the specified simulator and test type.
    /// </summary>
    /// <param name="playTestSimulator"></param>
    /// <param name="testType"></param>
    public Playtest(SnakePlayTestSimulator playTestSimulator, TestType testType) {
      this.playTestSimulator = playTestSimulator;
      this.testType = testType;
    }

    /// <summary>
    /// Runs a playtest using the specified agent. 
    /// </summary>
    /// <param name="savedAgent"></param>
    /// <param name="simulationCount">The number of playtests</param>
    /// <returns></returns>
    public PlaytestResults RunPlaytest(SavedAgent savedAgent, int simulationCount) {
      List<PlaytestResult> testResults = new List<PlaytestResult>();

      ShowProgressBar();

      for(int i = 0; i < simulationCount; i++) {
        testResults.Add(playTestSimulator.RunSimulation(savedAgent, testType, true));

        progressBar.Maximum = simulationCount + 1;
        progressBar.Value = simulationCount + 1;
        progressBar.Maximum = simulationCount;
        progressBar.Value = i;
        progressBar.Refresh();
        progressForm.Refresh();
      }

      PlaytestResults playtestResults = new PlaytestResults(testResults);
      progressForm.Close();

      return playtestResults;
    }

    private void ShowProgressBar() {
      progressBar = new ProgressBar();
      progressBar.Minimum = 0;
      progressBar.Maximum = simulationCount;

      progressBar.Value = 0;
      progressBar.Step = 1;

      progressForm = new Form();
      progressForm.Controls.Add(progressBar);
      progressForm.StartPosition = FormStartPosition.CenterScreen;
      progressForm.AutoSize = true;
      progressForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      progressForm.FormBorderStyle = FormBorderStyle.None;

      progressForm.Show();
    }
  }
}
