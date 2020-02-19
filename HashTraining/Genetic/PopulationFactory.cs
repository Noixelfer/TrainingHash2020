using GeneticSharp.Domain.Populations;
using HashCode.Genetic;
using System;
using System.Collections.Generic;
using System.Text;

namespace HashTraining.Genetic
{
    public class PopulationFactory : IPopulationFactory
    {
        public IPopulation GeneratePopulation()
        {
            return new Population(100, 200, new Chromosome(Program.MODEL.NumberOfTypes));
        }
    }
}
