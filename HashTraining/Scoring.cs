using System.Collections.Generic;

namespace HashTraining
{
    public class Scoring
    {
        public static int GetScore(int maximumSlices, IEnumerable<int> pizzas)
        {
            var sum = 0;
            foreach (var pizza in pizzas)
            {
                sum += pizza;
            }
            return sum > maximumSlices ? 0 : sum;
        }
    }
}