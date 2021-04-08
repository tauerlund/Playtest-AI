using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithmNS;
using HelperMethods;

namespace GeneticAlgorithmNS {

  /// <summary>
  /// Defines the method <see cref="MakeSelection(List{Agent}, int)"/> which returns a list of the selected agents
  /// </summary>
  public interface ISelector : IGeneticOperator {
   List<Agent> MakeSelection(List<Agent> agents, int selectionSize, IRandomNumberGenerator random);
  }
}
