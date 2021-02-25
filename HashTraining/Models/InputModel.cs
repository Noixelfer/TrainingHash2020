using System.Collections.Generic;

namespace HashTraining.Models
{
    public class InputModel
    {
        public int SimulationDuration;
        public int IntersectionCount;
        public int NumberOfStreets;
        public int NumberOfCars;
        public int CarBonusPoints;

        public Dictionary<string, Street> Streets = new Dictionary<string, Street>();
        public List<CarPath> CarPaths = new List<CarPath>();
    }
    
    public class Street
    {
        public int IntersectionStart;
        public int IntersectionEnd;
        public int Length;
    }

    public class CarPath
    {
        public int StreetCount;
        public List<Street> Streets;
    }
}