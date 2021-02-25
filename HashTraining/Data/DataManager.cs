using System.Collections.Generic;
using System.IO;
using System.Linq;
using HashTraining.Models;

namespace HashTraining.Data
{
    public class DataManager
    {
        public InputModel ReadFromFile(string levelName)
        {
            var fullInputPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Input", $"{levelName}.txt");
            if (!File.Exists(fullInputPath))
                throw new FileNotFoundException(fullInputPath);

            using var reader = new StreamReader(fullInputPath);
            var model = new InputModel();

            var firstLine = reader.ReadLine().Split(" ").Select(int.Parse).ToList();

            model.SimulationDuration = firstLine[0];
            model.IntersectionCount = firstLine[1];
            model.NumberOfStreets = firstLine[2];
            model.NumberOfCars = firstLine[3];
            model.CarBonusPoints = firstLine[4];

            var streets = new Dictionary<string, Street>();
            for (var i = 0; i < model.NumberOfStreets; i++)
            {
                var line = reader.ReadLine().Split(" ").ToList();
                var name = line[2];
                var street = new Street
                {
                    IntersectionStart = int.Parse(line[0]),
                    IntersectionEnd = int.Parse(line[1]),
                    Length = int.Parse(line[3])
                };
                streets[name] = street;
            }

            model.Streets = streets;

            var paths = new List<CarPath>();
            for (var i = 0; i < model.NumberOfCars; i++)
            {
                var line = reader.ReadLine().Split(" ").ToList();
                var path = new CarPath
                {
                    StreetCount = int.Parse(line[0]),
                    Streets = line.GetRange(1, line.Count - 1).ToList().Select(sn => model.Streets[sn]).ToList()
                };
                paths.Add(path);
            }

            model.CarPaths = paths;

            return model;
        }

        public void WriteToFile(string outputName, OutputModel outputModel)
        {
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Output");
            var archiveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Archive");

            var outputFile = Path.Combine(outputDirectory, $"{outputName}.out");
            var archiveFile = Path.Combine(archiveDirectory, $"{outputName}_{outputModel.Score}.out");

            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            if (!Directory.Exists(archiveDirectory))
                Directory.CreateDirectory(archiveDirectory);

            using (var writer = new StreamWriter(outputFile, false))
            {
                writer.WriteLine(outputModel.NumberOfIntersections);
                
                foreach (var schedule in outputModel.Schedules)
                {
                    writer.WriteLine(schedule.IntersectionId);
                    writer.WriteLine(schedule.StreetCount);
                    foreach (var (name, time) in schedule.Times)
                    {
                        writer.WriteLine($"{name} {time}");
                    }
                }
            }

            if (File.Exists(archiveFile))
                File.Delete(archiveFile);
            File.Copy(outputFile, archiveFile);
        }
    }
}