using System;
using System.Collections.Generic;
using System.IO;
using HashTraining.Data;

namespace HashTraining
{
	class Program
	{
		static void Main(string[] args)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			
			//RunLevel("a.txt");
			//RunAllLevels();
			var dataManager = new DataManager();
			var model = dataManager.ReadFromFile("a.txt");

			var scoring = new ScoreEvaluator();
			var solver = new ClassicSolution();

			var outputModel = new OutputModel
			{
			};
			Console.WriteLine(ScoreEvaluator.GetScore(model, outputModel));
			Console.Read();
		}

	}
}
