using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithmNS;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneticAlgorithmTests {

  [TestClass]
  class GeneComparer : Comparer<Gene> {
    public override int Compare(Gene a, Gene b) {
      return a.Value.CompareTo(b.Value);
    }
  }

  [TestClass]
  public class RandomResettingMutatorTests {

    UnitTestRandomNumberGenerator fakeRandom;

    [TestInitialize]
    public void TestInitialize() {
      fakeRandom = new UnitTestRandomNumberGenerator(1);
    }

    [TestMethod]
    public void RandomResettingMutator_TestMakeMutation_SuccessfullyMutates() {
      Gene[] genes = new Gene[4];

      genes[0] = new Gene(2.6);
      genes[1] = new Gene(1.1);
      genes[2] = new Gene(6.3);
      genes[3] = new Gene(8.2);

      RandomResettingMutator mutator = new RandomResettingMutator();

      mutator.MakeMutation(genes, 2, fakeRandom);

      Gene[] expectedResult = new Gene[4];
      expectedResult[0] = new Gene(1);
      expectedResult[1] = new Gene(1);
      expectedResult[2] = new Gene(1);
      expectedResult[3] = new Gene(1);

      Gene[] actualResult = genes;

      CollectionAssert.AreEqual(expectedResult, actualResult, new GeneComparer());
    }
  }
}
