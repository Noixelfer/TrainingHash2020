using GeneticSharp.Domain.Populations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode.Genetic
{
    public interface IPopulationFactory
    {
        IPopulation GeneratePopulation();
    }
}
