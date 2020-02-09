using System;
using System.IO;
using System.Linq;

namespace HashTraining
{
	class DataManager
	{
		public PizzaModel ReadFromFile(string levelName)
		{
			var model = new PizzaModel();
			var inputPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\Input\\";

			var fullPath = inputPath + levelName;
			StreamReader sr = new StreamReader(fullPath);
			var allText = sr.ReadToEnd();
			var lines = allText.Split('\n');

			var firstLine = lines[0].Split(" ");
			//read the first line
			model.NumberOfSlices = int.Parse(firstLine[0]);
			model.NumberOfTypes = int.Parse(firstLine[1]);

			var slices = lines[1].Split(" ");
			model.Slices = slices.Select(slice => int.Parse(slice)).ToList();

			return model;
		}

		public void WriteToFile(string outputName, PizzaOutputModel pizzaOutputModel)
		{
			var outputPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\Output\\" + outputName + ".out";
			StreamWriter sr = new StreamWriter(outputPath, false);
			sr.Write(pizzaOutputModel.NumberOfPizzas);
			sr.WriteLine();
			sr.Write(String.Join(" ", pizzaOutputModel.SelectedPizzaIds));

			//Archive - TODO
			var score = pizzaOutputModel.Score;
			var archivePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\Archive\\" + outputName.Substring(0, outputName.Length - 3);

			if (!Directory.Exists(archivePath))
			{
				Directory.CreateDirectory(archivePath);
			}

			var fileArchivePath = archivePath + "\\" + score.ToString();
			StreamWriter srArchive = new StreamWriter(fileArchivePath, false);
			srArchive.Write(pizzaOutputModel.NumberOfPizzas);
			srArchive.WriteLine();
			srArchive.Write(String.Join(" ", pizzaOutputModel.SelectedPizzaIds));
			srArchive.Close();
		}
	}
}
