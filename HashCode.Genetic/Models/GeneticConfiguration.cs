using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Selections;

namespace HashCode.Genetic.Models
{
    public class GeneticConfiguration
    {
        public ISelection Selection { get; set; }
        public ICrossover Crossover { get; set; }
        public IMutation Mutation { get; set; }

        public GeneticConfiguration(ISelection selection, ICrossover crossover, IMutation mutation)
        {
            Selection = selection;
            Crossover = crossover;
            Mutation = mutation;
        }

        public override string ToString()
        {
            return $"{Selection.GetType().Name}_{Crossover.GetType().Name}_{Mutation.GetType().Name}";
        }
    }
}
