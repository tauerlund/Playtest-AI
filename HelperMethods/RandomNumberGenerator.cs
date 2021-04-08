using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMethods {
  /// <summary>
  /// Repesents a random number generator that is able to produce a random int or double.
  /// The random number produced are based on the .NET 'Random' class.
  /// </summary>
  [Serializable]
  public class RandomNumberGenerator : IRandomNumberGenerator {

    private Random RandomGenerator;

    public RandomNumberGenerator() {
      RandomGenerator = new Random();
    }

    /// <summary>
    ///  Makes a random double in a range between the specified lower limit(inclusive) and higher limit(exclusive).
    /// </summary>
    public double GetDouble(double lowerLimit, double upperLimit) {
      return RandomGenerator.NextDouble() * (upperLimit - lowerLimit) + lowerLimit;
    }

    /// <summary>
    ///  Makes a random int in a range between the specified lower limit(inclusice) and higher limit(exclusive).
    /// </summary>
    public int GetInt(int lowerLimit, int upperLimit) {
      return RandomGenerator.Next(lowerLimit, upperLimit);
    }

    public void ReplaceRandomizer(Random random) {
      RandomGenerator = random;
    }
  }
}
