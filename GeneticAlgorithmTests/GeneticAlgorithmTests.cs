using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithmNS;
using HelperMethods;

namespace GeneticAlgorithmTests {

  public class RandomGeneratorTest : IRandomNumberGenerator {
    public int GetInt(int min, int max) {
      return 1;
    }
    public double GetDouble(double min, double max) {
      return 1.0;
    }
  }

  public class FitnessInfoTest : IFitnessInfo {
    public double Fitness { get; set; }

    public FitnessInfoTest(double fitness) {
      Fitness = fitness;
    }
  }

  // Måske som helper???
  public class FitnessCalculatorTest : IFitnessCalculator {
    public IFitnessInfo CalculateFitness(double[] genes) {
      double fitness = 0;
      for(int i = 0; i < genes.Length; i++) {
        fitness = genes[i] * 2; 
      }
      FitnessInfoTest fitnessInfoTest = new FitnessInfoTest(fitness);
      return fitnessInfoTest;
    }
  }
}
