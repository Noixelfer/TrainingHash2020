using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode.Genetic.Models
{
    public class GeneticSolution
    {
        public GeneticConfiguration GeneticConfiguration { get; set; }

        public object[] Genes { get; set; }

        public double? Fitness { get; set; }
    }
}
