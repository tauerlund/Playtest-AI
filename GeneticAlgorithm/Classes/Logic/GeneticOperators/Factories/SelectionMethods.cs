using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmNS {
  public class SelectionMethods {
    public TopPerformersSelector TopPerformersSelector {
      get { return new TopPerformersSelector(); }
    }

    public RouletteWheelSelector RouletteWheelSelector {
      get { return new RouletteWheelSelector(); }
    }
  }
}
