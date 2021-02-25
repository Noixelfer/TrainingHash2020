using System;
using System.Collections.Generic;

namespace HashTraining
{
	class Program
	{
		static void Main(string[] args)
		{
			//RunLevel("a.txt");
			//RunAllLevels();
			DataManager DataManager = new DataManager();
			var model = DataManager.ReadFromFile("a.txt");


			var outputModel = new BookScanningOutput();
			outputModel.NumberOfScannedLibraries = 2;
			outputModel.ScannedLibraries.Add((1, new List<int> { 5, 2, 3 }));
			outputModel.ScannedLibraries.Add((0, new List<int> { 0, 1, 2, 3, 4 }));
			Console.WriteLine(Scoring.GetScore(model, outputModel));
			Console.Read();
		}

		private static void RunAllLevels()
		{
			//RunLevel("a_example.in");
			//RunLevel("b_small.in");
			//RunLevel("c_medium.in");
			RunLevel("d_quite_big.in");
			//RunLevel("e_also_big.in");
		}

		private static void RunLevel(string levelName)
		{
			DataManager DataManager = new DataManager();
			var model = DataManager.ReadFromFile(levelName);
			ClasicSolutionD clasicSolutionD = new ClasicSolutionD(model);
			//var outputModel = clasicSolutionD.CreateOuputModel(levelName);
			//DataManager.WriteToFile(levelName, outputModel);
		}
	}
}
