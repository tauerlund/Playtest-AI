using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Contains and invokes methods on the current population of agents. 
  /// </summary>
  [Serializable]
  public class GeneticAlgorithm {

    public Population CurrentPopulation { get; private set; }
    public int GenerationCount { get; private set; }
    public Stopwatch RunTimeStopWatch { get; private set; } // Remember to stop when not calculating! (calling menu etc.)
    public TopKAgents TopKAgents { get; private set; } // The all time top agents of this genetic algorithm. 
    public double ConvergencePercent { get; private set; } // The latest calculated convergence percent representing how many genes every agent shares 
    public double CurrentAverageFitnessSelection { get; private set; }
    public double CurrentAverageFitnessPopulation { get; private set; }
    public GeneticSettings GeneticSettings { get; private set; }
    
    public bool IsRunning { get; set; }
    public bool IsPaused { get; set; }
    public Thread threadGUI { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GeneticAlgorithm"/> class containing
    /// a random population using the specified <see cref="GeneticAlgorithmNS.GeneticSettings"/>. 
    /// </summary>
    /// <param name="geneticSettings">The settings that the algorithm should use.</param>
    public GeneticAlgorithm(GeneticSettings geneticSettings) {
      // Start timer
      RunTimeStopWatch = new Stopwatch();
      // Set up variables
      GenerationCount = 0;
      this.GeneticSettings = geneticSettings;
      TopKAgents = new TopKAgents(geneticSettings.TopKAgentsCount);
      // Make random start population
      MakeRandomPopulation(geneticSettings);

      IsRunning = true;
      IsPaused = false;
    }

    /// <summary>
    /// Starts the genetic algorithm.
    /// </summary>
    public void Train() {

      // Start genetic GUI
      CalculateFitness(); 
      ShowGUI();
      // Start genetic algorithm
      do {
        if(IsPaused) {
          RunTimeStopWatch.Stop();
          while (IsPaused) { }
        }
        RunTimeStopWatch.Start();
        CalculateFitness();
        MakeSelection();
        MakeCrossovers();
        MakeMutations();
      } while(IsRunning); 
    }

    // Makes a new random population from the recieved settings.
    private void MakeRandomPopulation(GeneticSettings geneticSettings) {
      CurrentPopulation = new Population(geneticSettings.PopulationSize, geneticSettings.GeneCount, GenerationCount, geneticSettings.RandomNumberGenerator);
    }


    // Invokes the calculate fitness method on the current population and 
    private void CalculateFitness() {
      CurrentPopulation.CalculateFitness(GeneticSettings.FitnessCalculator);
      CurrentAverageFitnessPopulation = CurrentPopulation.AverageFitness;
      TopKAgents.Update(CurrentPopulation.Agents);
    }

    // Makes selection on the current population and updates the average fitness and convergence.
    private void MakeSelection() {
      CurrentPopulation.MakeSelection(GeneticSettings.Selector, GeneticSettings.SelectionSize, GeneticSettings.RandomNumberGenerator);
      double totalFitness = 0;
      for(int i = 0; i < CurrentPopulation.Agents.Count; i++) {
        totalFitness += CurrentPopulation.Agents[i].Fitness;
      }
      CurrentAverageFitnessSelection = totalFitness / CurrentPopulation.Agents.Count;
      ConvergencePercent = CurrentPopulation.GetConvergencePercent();
    }

    // Makes a new generation by invoking the crossover method on the current population.
    private void MakeCrossovers() {
      List<Agent> children = new List<Agent>();
      GenerationCount++;
      children = CurrentPopulation.MakeCrossovers(GeneticSettings.Crossover, GeneticSettings.PopulationSize, GeneticSettings.RandomNumberGenerator);
      CurrentPopulation = new Population(children, GenerationCount);
    }

    // Make mutations on the current population.
    private void MakeMutations() {
      CurrentPopulation.MakeMutations(GeneticSettings.Mutator, GeneticSettings.MutationProbabiltyAgents,
                                      GeneticSettings.MutationProbabilityGenes, GeneticSettings.RandomNumberGenerator);
    }

    private void ShowGUI() {
      threadGUI = new Thread(RunThreadGUI);
      threadGUI.SetApartmentState(ApartmentState.STA);
      threadGUI.Start();
    }

    private void RunThreadGUI() {
      Application.Run(new GeneticGUI(this, GeneticSettings));
    }
  }
}
