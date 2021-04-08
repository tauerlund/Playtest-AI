using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SnakeAI {
  /// <summary>
  /// Represents a collection of play test results. 
  /// </summary>
  public class PlaytestResults {

    public PlaytestResult this[int i] {
      get { return TestResults[i]; }
      set { TestResults[i] = value; }
    }

    public List<PlaytestResult> TestResults { get; private set; }

    public int NumberOfFailedTests { get; private set; }
    public double FailedPercentage { get; private set; }

    /// <summary>
    /// Initializes a new instance of the class <see cref="PlaytestResult"/>
    /// containing the given test results. Calculates and assignes 
    /// the failed test count and percentage to its properties. 
    /// </summary>
    /// <param name="testResults"></param>
    public PlaytestResults(List<PlaytestResult> testResults) {
      TestResults = testResults;

      NumberOfFailedTests = CalculateFailedTests();
      FailedPercentage = CalculateFailPercentage();
    }

    private int CalculateFailedTests() {
      return TestResults.FindAll(result => result.TestResult == TestResult.Failed).Count;
    }

    private double CalculateFailPercentage() {
      return ((double)NumberOfFailedTests / TestResults.Count) * 100;
    }
  }
}
