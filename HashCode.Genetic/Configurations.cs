using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Selections;
using HashCode.Genetic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashCode.Genetic
{
    public static class Configurations
    {
        public static readonly List<ISelection> Selections = new List<ISelection> 
        {  
            new EliteSelection(), 
            new RouletteWheelSelection(), 
            new TournamentSelection(), 
            new StochasticUniversalSamplingSelection() 
        };

        public static readonly List<ICrossover> Crossovers = new List<ICrossover>
        {
            new AlternatingPositionCrossover(),
            new CutAndSpliceCrossover(),
            new CycleCrossover(),
            new OnePointCrossover(),
            new PartiallyMappedCrossover(),
            new ThreeParentCrossover(),
            new TwoPointCrossover(),
            new UniformCrossover(),
            new VotingRecombinationCrossover()
        };

        public static readonly List<IMutation> Mutations = new List<IMutation>
        {
            new DisplacementMutation(),
            new FlipBitMutation(),
            new InsertionMutation(),
            new PartialShuffleMutation(),
            new ReverseSequenceMutation(),
            new TworsMutation(),
            new UniformMutation()
        };

        public static IEnumerable<GeneticConfiguration> GeneticConfigurations
            => Selections.SelectMany(s => Crossovers.SelectMany(c => Mutations.Select(m => new GeneticConfiguration(s, c, m))));

    }
}
