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
        
        public IEnumerable<int> SelectedPizzaIds;

        public int NumberOfPizzas => SelectedPizzaIds.Count();

        public void WriteToFile()
        {
            var filePath = "./output" + problemId + ".txt";
            using (StreamWriter file = new StreamWriter(filePath, false))
            {
                file.Write(NumberOfPizzas + "\n");
                foreach (var pizzaId in SelectedPizzaIds)
                {
                    file.Write(pizzaId + " ");
                }
            }
        } 
    }
}
    