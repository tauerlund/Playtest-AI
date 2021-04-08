using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithmNS;
using HelperMethods;

namespace SnakeAI {
  /// <summary>
  /// Variables used in the program.
  /// </summary>
  public static class ProgramSettings {
    // GENETIC SETTINGS 
    public const int POPULATION_SIZE = 5000;
    public const int SELECTION_SIZE  = 100;
    public const double MUTATION_PROBABALITY_AGENT = 0.9;
    public const double MUTATION_PROBABILITY_GENE  = 0.03;
    public const int TOP_K_AGENTS_COUNT = 10;
    // GENETIC OPERATORS
    public static readonly ICrossover CROSSOVER_METHOD = new CrossoverMethods().OnePointCombinePlusElitismCrossover;
    public static readonly ISelector  SELECTION_METHOD = new SelectionMethods().TopPerformersSelector;
    public static readonly IMutator MUTATION_METHOD    = new MutationMethods().RandomResettingMutator;

    public static readonly IAgentViewer AGENT_VIEWER = new SnakeReplay();

    // GENERAL HELPERS
    public static readonly IRandomNumberGenerator RANDOM_GENERATOR = new RandomNumberGenerator();

    // CALC FITNESS METHOD 
    public const int MAX_STEPS = 300;

    // NEURAL NETWORK SETTINGS
    public static readonly int NUMBER_OF_INPUT_NEURONS    = 16;
    public static readonly int[] HIDDEN_LAYER_STRUCTURE = { 10,5};
    public static readonly int[] OUTPUT_LABELS = { 1, 2, 3, 4 };

    // SNAKE GAME SETTINGS 
    public const int GRID_ROWS             = 20;
    public const int GRID_COLUMNS          = 30;
    public const int SIDE_LENGTH           = 20;
    public const int POINTS_PER_FOOD_EATEN = 1;
  }
}