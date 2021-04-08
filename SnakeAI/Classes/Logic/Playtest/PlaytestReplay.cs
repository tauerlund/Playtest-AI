using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeneticAlgorithmNS;
using HelperMethods;
using NeuralNetworkNS;
using SnakeGameNS;
using System.Windows.Forms;

namespace SnakeAI {
  [Serializable]
  public class PlaytestReplay {
    private Grid currentGrid;
    private PlaytestRecording recording;
    private NetworkSettings networkSettings;
    private SnakeSettings snakeSettings;

    /// <summary>
    /// Represents a class which is able to play a recording of a playtest.
    /// </summary>
    /// <param name="recording"></param>
    public PlaytestReplay(PlaytestRecording recording) {
      this.recording = recording;
      networkSettings = recording.NetworkSettings;
      snakeSettings = recording.SnakeSettings;
    }

    /// <summary>
    /// Shows a recording of the playtest. 
    /// </summary>
    /// <param name="speedMS"></param>
    public void ShowReplay(int speedMS) {
      Agent agent = recording.Agent;
      ShowPlay(recording, speedMS);
    }

    private void ShowPlay(PlaytestRecording recording, int speedMS) {
      List<PlaytestRoundInfo> fitnessRoundInfoList = recording.PlaytestRoundInfoList;


      int score = recording.InitialPlaytestRoundInfo.Score;
      bool isAlive = recording.InitialPlaytestRoundInfo.IsAlive;
      UpdateCurrentGridInitial(recording.InitialPlaytestRoundInfo.ChangedFields);

      SnakeGameGUI snakeGameGUI = new SnakeGameGUI(snakeSettings, currentGrid, score, isAlive);
      int snakeSpeedMS = speedMS;

      // START GUI
      snakeGameGUI.OpenGameWindow();

      // Loop throug frames.
      for(int i = 0; i < fitnessRoundInfoList.Count && snakeGameGUI.WindowRunning; i++) {
        UpdateCurrentGrid(fitnessRoundInfoList[i].ChangedFields);
        snakeGameGUI.UpdateView(currentGrid, fitnessRoundInfoList[i].Score, fitnessRoundInfoList[i].IsAlive, fitnessRoundInfoList[i].SnakeHeadPoint);
        Thread.Sleep(snakeSpeedMS);
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
