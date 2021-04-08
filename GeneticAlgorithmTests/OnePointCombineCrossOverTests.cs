using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithmNS;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneticAlgorithmTests {

  [TestClass]
  class Comparer : Comparer<Gene> {
    public override int Compare(Gene a, Gene b) {
      return a.Value.CompareTo(b.Value);
    }
  }

  [TestClass]
  public class OnePointCombineCrossOverTests {

    UnitTestRandomNumberGenerator fakeRandom;

    [TestInitialize]
    public void TestInitialize() {
      fakeRandom = new UnitTestRandomNumberGenerator(0, true);
    }

    [TestMethod]
    public void OnePointCombineCrossOver_TestCombineCrossOver_SuccessfullyMakesCrossover() {
      Gene[] genes1 = new Gene[6];
      genes1[0] = new Gene(1);
      genes1[1] = new Gene(2);
      genes1[2] = new Gene(3);
      genes1[3] = new Gene(4);
      genes1[4] = new Gene(5);
      genes1[5] = new Gene(6);
      
      Gene[] genes2 = new Gene[6];
      genes2[0] = new Gene(6);
      genes2[1] = new Gene(5);
      genes2[2] = new Gene(4);
      genes2[3] = new Gene(3);
      genes2[4] = new Gene(2);
      genes2[5] = new Gene(1);

      Gene[] genes3 = new Gene[6];
      genes3[0] = new Gene(1);
      genes3[1] = new Gene(2);
      genes3[2] = new Gene(4);
      genes3[3] = new Gene(3);
      genes3[4] = new Gene(2);
      genes3[5] = new Gene(1);

      Chromosome chromosome1 = new Chromosome(genes1);
      Chromosome chromosome2 = new Chromosome(genes2);
      Chromosome chromosome3 = new Chromosome(genes3);
      Agent child = new Agent(chromosome3);

      List<Agent> agents = new List<Agent>();
      agents.Add(new Agent(chromosome1));
      agents.Add(new Agent(chromosome2));

      OnePointCombineCrossoverRegular crossOver = new OnePointCombineCrossoverRegular();

      List<Agent> children = crossOver.MakeCrossovers(agents, 1, fakeRandom);

      List<Agent> actualResult = children;

      List<Agent> expectedResult = new List<Agent>();
      expectedResult.Add(child);

      CollectionAssert.AreEqual(actualResult[0].Chromosome.Genes, expectedResult[0].Chromosome.Genes, new GeneComparer());
    }
  }
}
