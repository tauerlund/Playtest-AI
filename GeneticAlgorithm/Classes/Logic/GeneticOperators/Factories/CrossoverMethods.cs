using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS{
  [Serializable]
  public class CrossoverMethods { 

    public OnePointCombineCrossoverRegular OnePointCombineCrossoverRegular {
      get { return new OnePointCombineCrossoverRegular(); }
    }

    public OnePointCombineCrossoverElitism OnePointCombinePlusElitismCrossover {
      get { return new OnePointCombineCrossoverElitism(); }
    }

  }
}
