using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Defines the method <see cref="CalculateFitness(double[])"/> with the return value <see cref="IFitnessInfo"/>
  /// </summary>
  public interface IFitnessCalculator  { 
    IFitnessInfo CalculateFitness(double[] chromosome);
  }
}
