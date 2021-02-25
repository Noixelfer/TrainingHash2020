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

        public List<Street> Streets;
    }
    
    public class Street
    {
        public int IntersectionStart;
        public int IntersectionEnd;
        public string Name;
        public int Length;
    }

    public class CarPath
    {
        public int StreetCount;
        public List<string> StreetNames;
    }
}