using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  /// <summary>
  /// Defines the property <see cref="Fitness"/> 
  /// </summary>
  public interface IFitnessInfo {
    double Fitness { get; }
  }
}
