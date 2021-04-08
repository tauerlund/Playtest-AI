using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithmNS;

namespace GeneticAlgorithmTests {
  public class UnitTestFitnessCalculator : IFitnessCalculator {

    public IFitnessInfo fitnessInfo;

    public UnitTestFitnessCalculator(double fitness) {
      fitnessInfo = new UnitTestFitnessInfo(fitness);
    }

    public IFitnessInfo CalculateFitness(double[] genes) {
      return fitnessInfo;
    }
  }

  public class UnitTestFitnessInfo : IFitnessInfo {

    public double Fitness { get; private set; }

    public UnitTestFitnessInfo(double fitness) {
      Fitness = fitness;
    }
  }
}
