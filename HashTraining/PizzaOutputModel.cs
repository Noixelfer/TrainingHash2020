using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HashTraining
{
    public class PizzaOutputModel
    {
        public PizzaOutputModel(string problemId)
        {
            this.problemId = problemId;
        }
        
        public string problemId;
        
        public List<int> SelectedPizzaIds;
        public int Score { get; set; }

        public int NumberOfPizzas => SelectedPizzaIds.Count();
    }
}
    