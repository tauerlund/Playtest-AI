using GeneticAlgorithmNS;
using NeuralNetworkNS;
using SnakeAI;
using SnakeGameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI {
  /// <summary>
  /// Gathers information about a playtest. 
  /// </summary>
  public class PlaytestRecording {

    public List<PlaytestRoundInfo> PlaytestRoundInfoList { get; private set; }
    public PlaytestRoundInfo InitialPlaytestRoundInfo { get; private set; }
    public Grid InitialGrid { get; private set; }
    public Agent Agent { get; }
    public NetworkSettings NetworkSettings { get;  }
    public SnakeSettings SnakeSettings { get;  }
    public EndTestInfo EndTestInfo { get; private set; }

    private Grid previousGrid;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaytestRecording"/> class. After instantiation
    /// the class is ready for taking snapshots of a playtest simulation.
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="networkSettings"></param>
    /// <param name="snakeSettings"></param>
    public PlaytestRecording(Agent agent, NetworkSettings networkSettings, SnakeSettings snakeSettings) {
      Agent = agent;
      NetworkSettings = networkSettings;
      SnakeSettings = snakeSettings;
      PlaytestRoundInfoList = new List<PlaytestRoundInfo>(); 
    }

    /// <summary>
    /// Saves information about the snake game before the simulation round begins. 
    /// </summary>
    /// <param name="snakeGame"></param>
    public void TakeSnapShotInitial(SnakeGame snakeGame) {
      List<Field> changedFields = new List<Field>();
      changedFields = GetChangedFieldsInitial(snakeGame.Grid);
      InitialPlaytestRoundInfo = new PlaytestRoundInfo(snakeGame, changedFields);
      previousGrid = snakeGame.Grid.GetCopy();
    }

    /// <summary>
    /// Saves information about the snake game during the simulation round.
    /// </summary>
    /// <param name="snakeGame"></param>
    public void TakeSnapShotRound(SnakeGame snakeGame) {
      List<Field> changedFields = new List<Field>();
      changedFields = GetChangedFields(snakeGame.Grid);
      PlaytestRoundInfoList.Add(new PlaytestRoundInfo(snakeGame, changedFields));
      previousGrid = snakeGame.Grid.GetCopy();
    }

    /// <summary>
    /// Saves information about the snake game after the simulation round has ended. 
    /// </summary>
    /// <param name="snakeGame"></param>
    public void TakeSnapShotEndTest(EndTestInfo endTestInfo) {
      EndTestInfo = endTestInfo;
    }

    // Updates fields that has been changed
    private List<Field> GetChangedFieldsInitial(Grid initialGrid) {
      List<Field> changedFields = new List<Field>();

      for(int i = 0; i < initialGrid.RowCount; i++) {
        for(int j = 0; j < initialGrid.ColumnCount; j++) {
          SnakeGameNS.Point currentPoint = new Point(i, j);
          changedFields.Add(initialGrid.GetFieldCopy(currentPoint));
        }
      }
      return changedFields;
    }

    // Compares the grid and saves changed fileds in the changedFields list. 
    private List<Field> GetChangedFields(Grid newGrid) {
      List<Field> changedFields = new List<Field>();
      for(int i = 0; i < newGrid.RowCount; i++) {
        for(int j = 0; j < newGrid.ColumnCount; j++) {
          SnakeGameNS.Point currentPoint = new Point(i, j);
          if(previousGrid[currentPoint].GetType() != newGrid[currentPoint].GetType()) {
            changedFields.Add(newGrid.GetFieldCopy(currentPoint));
          }
        }
      }
      return changedFields;
    }
  }
}
