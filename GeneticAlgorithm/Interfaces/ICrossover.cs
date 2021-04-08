using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmNS{
  /// <summary>
  /// Defines the method <see cref="MakeCrossovers(List{Agent}, int)"/> which returns a list of the crossed agents
  /// </summary>
  public interface ICrossover : IGeneticOperator {
    List<Agent> MakeCrossovers(List<Agent> agents, int populationSize, IRandomNumberGenerator random);
  }
}
