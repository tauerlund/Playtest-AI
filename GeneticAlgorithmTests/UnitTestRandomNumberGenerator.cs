using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;

namespace GeneticAlgorithmTests {
  class UnitTestRandomNumberGenerator : IRandomNumberGenerator {

    private double number;
    private bool runCounter;

    private double[] numberArray;
    private int counter;

    public UnitTestRandomNumberGenerator(double number, bool counter) {
      runCounter = counter;
      this.number = number;
      numberArray = new double[10];
      numberArray[0] = 0;
      numberArray[1] = 1;
      numberArray[2] = 2;
      numberArray[3] = 3;
      numberArray[4] = 4;
      numberArray[5] = 5;

      this.counter = 0;
    }

    public UnitTestRandomNumberGenerator(double number) {
      runCounter = false;
      this.number = number;
    }

    public int GetInt(int lowerLimit, int upperLimit) {
      if(runCounter) {
        int returned = (int)numberArray[counter];
        counter++;
        return returned;
      }
      else {
        return (int)number;
      }
    }

    public double GetDouble(double lowerLimit, double upperLimit) {
      if(runCounter) {
        double returned = numberArray[counter];
        counter++;
        return returned;
      }
      else {
        return number;
      }
    }
  }
}
