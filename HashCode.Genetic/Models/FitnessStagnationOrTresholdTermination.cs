using GeneticSharp.Domain;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode.Genetic.Models
{
    public class FitnessStagnationOrTresholdTermination : FitnessStagnationTermination
    {
        private readonly double _fitnessThreshold;

        public FitnessStagnationOrTresholdTermination(int expectedStagnantGenerationsNumber, double fitnessThreshold) 
            : base(expectedStagnantGenerationsNumber)
        {
            _fitnessThreshold = fitnessThreshold;
        }

        protected override bool PerformHasReached(IGeneticAlgorithm geneticAlgorithm)
        {
            if (geneticAlgorithm?.BestChromosome?.Fitness != null &&
                geneticAlgorithm.BestChromosome.Fitness.Value >= _fitnessThreshold)
            {
                return true;
            }

            return base.PerformHasReached(geneticAlgorithm);
        }
    }
}
