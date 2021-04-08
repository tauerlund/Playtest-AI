using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithmNS;
using System.Collections.Generic;

namespace GeneticAlgorithmTests {
  [TestClass]
  public class TopPerformsSelectorTests {

    List<Agent> agents;

    [TestInitialize]
    public void TestInitialize() {
      Gene[] genes = new Gene[1];
      genes[0] = new Gene(1);

      Chromosome chromosome = new Chromosome(genes);

      agents = new List<Agent>();

      agents.Add(new Agent(chromosome));
      agents.Add(new Agent(chromosome));
      agents.Add(new Agent(chromosome));
      agents.Add(new Agent(chromosome));

      agents[0].CalculateFitness(new UnitTestFitnessCalculator(20));
      agents[1].CalculateFitness(new UnitTestFitnessCalculator(1));
      agents[2].CalculateFitness(new UnitTestFitnessCalculator(75));
      agents[3].CalculateFitness(new UnitTestFitnessCalculator(4));

    }

    [TestMethod]
    public void TopPerformersSelector_MakeSelection_SuccessfullySelectsTopPerformers() {
      TopPerformersSelector topPerformersSelector = new TopPerformersSelector();

      List<Agent> expectedResult = new List<Agent>();
      expectedResult.Add(agents[2]);
      expectedResult.Add(agents[0]);

      List<Agent> actualResult = topPerformersSelector.MakeSelection(agents, 2, new RandomGeneratorTest());

      CollectionAssert.Equals(expectedResult, actualResult);
    }
  }
}
