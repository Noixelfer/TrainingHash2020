using System;
using System.IO;
using System.Linq;

namespace HashTraining
{
	class DataManager
	{
		public BookScanning ReadFromFile(string levelName)
		{
			var model = new BookScanning();
			var inputPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\Input\\";

			var fullPath = inputPath + levelName;
			StreamReader sr = new StreamReader(fullPath);
			var allText = sr.ReadToEnd();
			var lines = allText.Split('\n');

			var firstLine = lines[0].Split(" ");
			//read the first line
			model.NumberOfBooks = int.Parse(firstLine[0]);
			model.NumberOfLibraries = int.Parse(firstLine[1]);
			model.NumberOfDays = int.Parse(firstLine[2]);

			//read the second line
			var secondLine = lines[1].Split(" ");
			foreach (var word in secondLine)
			{
				model.BookScores.Add(int.Parse(word));
			}

			for (int i = 2; i < (model.NumberOfLibraries + 2); i++)
			{
				var library = new Library();
				var libraryLine1 = lines[(i - 1) * 2].Split(" ");
				var libraryLine2 = lines[(i - 1) * 2 + 1].Split(" ");

				library.NumberOfBooks = int.Parse(libraryLine1[0]);
				library.SigningTime = int.Parse(libraryLine1[1]);
				library.BooksShippedPerDay = int.Parse(libraryLine1[2]);

				libraryLine2.ToList().ForEach(word => library.Books.Add(int.Parse(word)));

				model.Libraries.Add(library);
			}

			return model;
		}

		public void WriteToFile(string outputName, BookScanningOutput pizzaOutputModel)
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
