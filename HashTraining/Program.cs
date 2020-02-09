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
			Console.Read();
		}
	}
}
