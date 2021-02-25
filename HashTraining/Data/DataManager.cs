using System;
using System.IO;
using System.IO.Compression;
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

            var allText = reader.ReadToEnd();

            var model = new InputModel();
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
                writer.Write("test");
            }
            if(File.Exists(archiveFile))
                File.Delete(archiveFile);
            File.Copy(outputFile, archiveFile);
        }
    }
}