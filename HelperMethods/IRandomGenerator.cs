using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMethods {
  /// <summary>
  /// Defines methods for getting a random int and a random double.
  /// </summary>
  public interface IRandomNumberGenerator {

    int GetInt(int lowerLimit, int upperLimit);
    double GetDouble(double lowerLimit, double upperLimit);
  }
}
