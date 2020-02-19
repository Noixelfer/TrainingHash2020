using GeneticSharp.Domain.Populations;
using HashCode.Genetic;
using HashTraining.Genetic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HashTraining
{
    public class Program
    {
        public static PizzaModel MODEL { get; set; }

        public static void Main(string[] args)
        {
            DataManager DataManager = new DataManager();
            MODEL = DataManager.ReadFromFile("e_also_big.in");

            var solver = new GeneticSolver(new PopulationFactory(), new Fitness(), stagnantGenerationsNumber: 5);
            solver.Solve((chromosome) =>
            {
                DataManager.WriteToFile("vlad", new PizzaOutputModel("vlad")
                {
                    SelectedPizzaIds = (chromosome as Chromosome).GetPizzaIds().ToList()
                });
            });
        }

        private static void RunAllLevels()
        {
            //RunLevel("a_example.in");
            //RunLevel("b_small.in");
            //RunLevel("c_medium.in");
            //RunLevel("d_quite_big.in");
            //RunLevel("e_also_big.in");
        }

        //private static void RunLevel(string levelName)
        //{
        //	DataManager DataManager = new DataManager();
        //	var model = DataManager.ReadFromFile(levelName);
        //	ClasicSolutionD clasicSolutionD = new ClasicSolutionD(model);
        //	var outputModel = clasicSolutionD.CreateOuputModel(levelName);
        //	DataManager.WriteToFile(levelName, outputModel);
        //}
    }
}
