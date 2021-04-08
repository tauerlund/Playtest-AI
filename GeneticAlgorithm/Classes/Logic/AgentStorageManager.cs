using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS {
  public class AgentStorageManager : StorageManager {

    private int fileNameCounter;

    public AgentStorageManager() {
      fileNameCounter = 0;
    }

    public void SaveRecordingBinary<T>(T recording) {
      string directory = GeneticConstants.INTERNAL_DIRECTORY_RECORDING;
      SaveObject(recording, directory + fileNameCounter);
      fileNameCounter++;
    }

    public SavedAgent LoadAgentBinary(string filePath) {
      SavedAgent loadedAgent = null;
      loadedAgent = LoadObject<SavedAgent>(filePath);
      return loadedAgent;
    }

    public void SaveAgentBinary(Agent agentToSave, GeneticSettings geneticSettings, string filePath) {
      SavedAgent savedAgent = new SavedAgent(agentToSave, geneticSettings);
      //string directory = GeneticConstants.INTERNAL_DIRECTORY_AGENTS_BINARY;
      SaveObject(savedAgent, filePath);
    }
  }
}
