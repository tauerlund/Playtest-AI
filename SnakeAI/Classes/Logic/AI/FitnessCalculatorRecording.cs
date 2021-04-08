using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGameNS;
using GeneticAlgorithmNS;
using NeuralNetworkNS;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SnakeAI {
  /// <summary>
  /// Stores information about a fitness calculation. 
  /// </summary>
  [Serializable]
  public class FitnessCalculatorRecording {

    public List<FitnessRoundInfo> FitnessRoundInfoList { get; private set; }
    public FitnessRoundInfo InitialFitnessRoundInfo { get; private set; }
    public Grid InitialGrid { get; private set; }
    public Agent Agent { get; }
    public NetworkSettings NetworkSettings { get; }
    public SnakeSettings SnakeSettings { get; }
    public EndGameInfo originalEndGameInfo { get;  }
    public EndGameInfo newEndGameInfo { get; private set; }

    private Grid previousGrid; 

    /// <summary>
    /// Initializes a new instance of the class <see cref="FitnessCalculatorRecording"/>
    /// which is able to save information about a fitness calculation. 
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="networkSettings"></param>
    /// <param name="snakeSettings"></param>
    public FitnessCalculatorRecording(Agent agent, NetworkSettings networkSettings, SnakeSettings snakeSettings) {
      Agent = agent;
      originalEndGameInfo = agent.FitnessInfo as EndGameInfo;
      NetworkSettings = networkSettings;
      SnakeSettings = snakeSettings;
      FitnessRoundInfoList = new List<FitnessRoundInfo>();
    }

    /// <summary>
    /// Saves information about the first recording.
    /// </summary>
    /// <param name="snakeGame"></param>
    /// <param name="movesSincePoint"></param>
    /// <param name="totalMoves"></param>
    /// <param name="inputNeuralNetwork"></param>
    /// <param name="neuralNetwork"></param>
    public void TakeSnapShotInitial(SnakeGame snakeGame, int movesSincePoint, int totalMoves, double[] inputNeuralNetwork,
                                    NeuralNetwork neuralNetwork) {

      List<Field> changedFields = new List<Field>();
      changedFields = GetChangedFieldsInitial(snakeGame.Grid);
      InitialFitnessRoundInfo = new FitnessRoundInfo(snakeGame, movesSincePoint, totalMoves, inputNeuralNetwork, neuralNetwork, changedFields);
      previousGrid = snakeGame.Grid.GetCopy();
    }

    /// <summary>
    /// Saves information during in the fitness calculation round.
    /// </summary>
    /// <param name="snakeGame"></param>
    /// <param name="movesSincePoint"></param>
    /// <param name="totalMoves"></param>
    /// <param name="inputNeuralNetwork"></param>
    /// <param name="neuralNetwork"></param>
    public void TakeSnapShotRound(SnakeGame snakeGame, int movesSincePoint, int totalMoves, double[] inputNeuralNetwork,
                                  NeuralNetwork neuralNetwork) {

      List<Field> changedFields = new List<Field>();
      changedFields = GetChangedFields(snakeGame.Grid);
      FitnessRoundInfoList.Add(new FitnessRoundInfo(snakeGame, movesSincePoint, totalMoves, inputNeuralNetwork, neuralNetwork, changedFields));
      previousGrid = snakeGame.Grid.GetCopy();
    }

    /// <summary>
    /// Saves information after the fitness calculation has ended.
    /// </summary>
    /// <param name="newEndGameInfo"></param>
    public void TakeSnapShotEndGame(EndGameInfo newEndGameInfo) {
      this.newEndGameInfo = newEndGameInfo;
    }

    private List<Field> GetChangedFieldsInitial(Grid initialGrid) {
      List<Field> changedFields = new List<Field>();

      // Updates fields that has been changed
      for(int i = 0; i < initialGrid.RowCount; i++) {
        for(int j = 0; j < initialGrid.ColumnCount; j++) {
          SnakeGameNS.Point currentPoint = new Point(i, j);
          changedFields.Add(initialGrid.GetFieldCopy(currentPoint)); 
        }
      }

      return changedFields;
    }

    private List<Field> GetChangedFields(Grid newGrid) {
      List<Field> changedFields = new List<Field>();
      // Compares the grid and saves changed fileds in the changedFields list. 
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
