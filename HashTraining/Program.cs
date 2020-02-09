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
			DataManager DataManager = new DataManager();
			var model = DataManager.ReadFromFile("c_medium.in");
			ClasicSolutionD clasicSolutionD = new ClasicSolutionD(model);
			var outputModel = clasicSolutionD.CreateOuputModel("c_medium.in");
			DataManager.WriteToFile("c_medium.in", outputModel);
		}
	}
}
