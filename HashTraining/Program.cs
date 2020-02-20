using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HashTraining
{
	class Program
	{
		static void Main(string[] args)
		{
			RunLevel("a.txt");
			//RunAllLevels();
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
