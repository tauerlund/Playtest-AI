using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  public class MutationMethods {
    public RandomResettingMutator RandomResettingMutator {
      get { return new RandomResettingMutator(); }
    }
  }
}