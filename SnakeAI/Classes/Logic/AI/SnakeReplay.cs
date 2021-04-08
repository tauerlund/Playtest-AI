using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkNS;
using SnakeGameNS;
using GeneticAlgorithmNS;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HelperMethods;

namespace SnakeAI {
  /// <summary>
  /// Shows GUI replays of fitness calculations. 
  /// Can be used to show replays of all agents using the settings specified at instantiation. 
  /// </summary>
  [Serializable]
  class SnakeReplay : IAgentViewer {
    private SnakeSettings snakeSettings;
    private NetworkSettings networkSettings;

    private Grid currentGrid;
    private int speedMS;

    /// <summary>
    /// Show replay of the given agent.
    /// </summary>
    /// <param name="agentToShow"></param>
    /// <param name="geneticSettings"></param>
    public void ViewAgent(Agent agentToShow, GeneticSettings geneticSettings) {

      networkSettings = (geneticSettings.FitnessCalculator as SnakeFitnessCalculator).NetworkSettings;
      snakeSettings = (geneticSettings.FitnessCalculator as SnakeFitnessCalculator).SnakeSettings;
      SnakeFitnessCalculator fitnessCalculator = geneticSettings.FitnessCalculator as SnakeFitnessCalculator;

      FitnessCalculatorRecording recording = fitnessCalculator.RecordCalculation(agentToShow);

      MilisecondsForm milisecondsForm = new MilisecondsForm();
      milisecondsForm.ShowDialog();
      speedMS = milisecondsForm.speedMS;

      ShowPlay(recording);
    }

    private void ShowPlay(FitnessCalculatorRecording recorder) {
      List<FitnessRoundInfo> fitnessRoundInfoList = recorder.FitnessRoundInfoList;

      int score = recorder.InitialFitnessRoundInfo.Score;
      bool isAlive = recorder.InitialFitnessRoundInfo.IsAlive;
      UpdateCurrentGridInitial(recorder.InitialFitnessRoundInfo.ChangedFields);

      SnakeGameGUI snakeGameGUI = new SnakeGameGUI(snakeSettings, currentGrid, score, isAlive);

      // START GUI
      snakeGameGUI.OpenGameWindow();

      // Loop throug frames.
      for(int i = 0; i < fitnessRoundInfoList.Count; i++) {
        UpdateCurrentGrid(fitnessRoundInfoList[i].ChangedFields);
        snakeGameGUI.UpdateView(currentGrid, fitnessRoundInfoList[i].Score, fitnessRoundInfoList[i].IsAlive, fitnessRoundInfoList[i].SnakeHeadPoint);
        Thread.Sleep(speedMS);
      }
    }

    private void UpdateCurrentGrid(List<Field> changedFields) {
      for(int i = 0; i < changedFields.Count; i++) {
        if(currentGrid[changedFields[i].Point].GetType() != changedFields[i].GetType()) {
          currentGrid[changedFields[i].Point] = changedFields[i];
        }
      }
    }

    private void UpdateCurrentGridInitial(List<Field> changedFields) {
      Grid initialGrid = new Grid(snakeSettings.RowCount, snakeSettings.ColumnCount);
      int k = 0;
      // Updates fields that has been changed
      for(int i = 0; i < snakeSettings.RowCount; i++) {
        for(int j = 0; j < snakeSettings.ColumnCount; j++) {
          SnakeGameNS.Point currentPoint = new Point(i, j);
          initialGrid[currentPoint] = changedFields[k];
          k++;
        }
      }
      currentGrid = initialGrid;
    }
  }
}
