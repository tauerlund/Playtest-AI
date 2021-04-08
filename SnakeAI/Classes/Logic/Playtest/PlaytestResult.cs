using SnakeAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI {
  /// <summary>
  /// Represents a playtest result containing if the test has passed/failed and a recording of the test.
  /// </summary>
  public class PlaytestResult {

    public TestResult TestResult { get; }
    public PlaytestRecording PlaytestRecorder { get; }

    public PlaytestResult(TestResult testResult, PlaytestRecording playtestRecorder) {
      TestResult = testResult;
      PlaytestRecorder = playtestRecorder;
    }
  }
}