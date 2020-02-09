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
			model.Slices = slices.Select(slice => int.Parse(slice));

			return model;
		}
	}
}
