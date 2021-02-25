using System;
using System.IO;
using HashTraining.Data;
using HashTraining.Score;
using HashTraining.Solvers;

namespace HashTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            RunLevel("a.txt");
        }


        public static void RunLevel(string levelName)
        {
            try
            {
                var dataManager = new DataManager();
                var model = dataManager.ReadFromFile(levelName);
                var scoring = new ScoreEvaluator();
                var solver = new ClassicSolution();
                var outputModel = solver.Solve(model, scoring);
                Console.WriteLine(scoring.GetScore(model, outputModel));
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}