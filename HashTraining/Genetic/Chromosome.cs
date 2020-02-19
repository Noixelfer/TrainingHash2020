using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace HashTraining.Genetic
{
    public class Chromosome : BinaryChromosomeBase
    {
        public Chromosome(int length) : base(length)
        {
            this.CreateGenes();
        }

        public override IChromosome CreateNew()
        {
            return new Chromosome(Length);
        }

        public IEnumerable<int> GetPizzaIds()
        {
            return GetGenes().Take(Program.MODEL.NumberOfTypes).Select((g, i) => (int)g.Value > 0 ? i : -1)
                             .Where(i => i > -1);
        }
    }
}
