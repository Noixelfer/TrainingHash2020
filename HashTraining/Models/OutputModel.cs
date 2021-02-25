using System.Collections.Generic;

namespace HashTraining.Models
{
    public class OutputModel
    {
        public int Score;

        public int NumberOfIntersections => Schedules.Count;

        public List<Schedule> Schedules = new List<Schedule>();
    }

    public class Schedule
    {
        public int IntersectionId;
        public int StreetCount => Times.Count;
        public List<(string StreetName, int GreenTime)> Times = new List<(string StreetName, int GreenTime)>();
    }
}