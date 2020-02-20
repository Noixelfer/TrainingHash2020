using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode.Genetic.Models
{
    public class ParallelGeneticConfiguration
    {
        public GeneticConfiguration GeneticConfiguration { get; set; }
        public int TaskCount { get; set; } = 1;

        public ParallelGeneticConfiguration(GeneticConfiguration geneticConfiguration, int taskCount = 1)
        {
            GeneticConfiguration = geneticConfiguration;
            TaskCount = taskCount;
        }
    }
}
