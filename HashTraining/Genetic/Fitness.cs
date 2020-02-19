using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTraining.Genetic
{
    public class Fitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var pizzaIds = (chromosome as Chromosome).GetPizzaIds();

            return Scoring.GetScore(Program.MODEL.NumberOfSlices, pizzaIds.Select(i => Program.MODEL.Slices[i]));
        }
    }
}
