﻿using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Framework.Threading;
using HashCode.Genetic.Converters;
using HashCode.Genetic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HashCode.Genetic
{
    public class GeneticSolver
    {
        private readonly IPopulationFactory _populationFactory;
        private readonly IFitness _fitness;

        public int StagnantGenerationsNumber { get; set; }
        public int MaxFitness { get; set; }
        public int MaxEpoch { get; set; }
        public bool SaveEpochFile { get; set; } = true;
        public string OutputDirectory { get; set; } = "";

        private static object _lock = new object();
        private static double _bestFitness = 0;
        private static int _currentEpoch = 0;
        private static List<GeneticSolution> _solutions { get; set; } = new List<GeneticSolution>();

        public GeneticSolver(IPopulationFactory populationFactory, IFitness fitness, int maxEpoch = 10, int stagnantGenerationsNumber = 30, int maxFitness = int.MaxValue)
        {
            _populationFactory = populationFactory;
            _fitness = fitness;

            StagnantGenerationsNumber = stagnantGenerationsNumber;
            MaxFitness = maxFitness;
            MaxEpoch = maxEpoch;
        }

        public void Solve(Action<IChromosome> onBestSolutionReached)
        {
            Solve(Configurations.GeneticConfigurations.Select(g => new ParallelGeneticConfiguration(g)).ToList(), onBestSolutionReached);
        }

        public void Solve(List<ParallelGeneticConfiguration> tasks, Action<IChromosome> onBestSolutionReached)
        {
            var taskTotal = tasks.Sum(t => t.TaskCount);

            Console.WriteLine("Starting solve...");
            Parallel.Invoke(tasks.SelectMany(t => Enumerable.Range(1, t.TaskCount)
                                 .Select(taskNumber => (Action)(() => Solve(t.GeneticConfiguration, OnTerminationReached, taskNumber))))
                                 .ToArray());

            void OnTerminationReached(IChromosome chromosome, GeneticConfiguration configuration)
            {
                Console.WriteLine($"Algorithm {_solutions.Count}/{taskTotal} finished. Configuration: {configuration.ToString().PadRight(70, ' ')}, Fitness: {chromosome.Fitness.Value}");
                if (chromosome != null && chromosome.Fitness >= _bestFitness)
                {
                    Console.WriteLine("Fitness was one of the best, saving solution...");

                    try
                    {
                        onBestSolutionReached(chromosome);
                        _bestFitness = chromosome.Fitness.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Something went wrong while saving solution for configuration {configuration.ToString()}");
                        Console.WriteLine();
                        Console.WriteLine($"Fitness was: {chromosome.Fitness}, solution was: {string.Join(",", chromosome.GetGenes())}");
                    }
                }

                _solutions.Add(new GeneticSolution
                {
                    GeneticConfiguration = configuration,
                    Fitness = chromosome?.Fitness,
                    Genes = chromosome?.GetGenes()?.Select(g => g.Value)?.ToArray()
                });

                if (_solutions.Count == tasks.Sum(t => t.TaskCount))
                {
                    _solutions = _solutions.Where(s => s.Fitness != null).ToList();
                    OnEpochEnded(onBestSolutionReached);
                }
            }
        }

        private void OnEpochEnded(Action<IChromosome> onBestSolutionReached)
        {
            Console.WriteLine($"Epoch {_currentEpoch} ended");
            if(SaveEpochFile) SaveEpoch();

            if (_currentEpoch < MaxEpoch)
            {
                Console.WriteLine($"Starting epoch {_currentEpoch + 1} with the top 50% best solutions from last epoch");

                _currentEpoch++;
                var bestSolutions = _solutions.OrderByDescending(s => s.Fitness).Take((int)Math.Floor(_solutions.Count / 2m));

                Solve(bestSolutions.Select(s => new ParallelGeneticConfiguration(s.GeneticConfiguration, 2)).ToList(), onBestSolutionReached);
            }

            void SaveEpoch()
            {
                try
                {
                    File.WriteAllText(OutputDirectory + $"solutions-{_currentEpoch}.json", JsonSerializer.Serialize(_solutions, new JsonSerializerOptions
                    {
                        Converters = { new GeneticConfigurationJsonConverter() }
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong while saving epoch");
                }
            }
        }

        private void Solve(GeneticConfiguration configuration, Action<IChromosome, GeneticConfiguration> onTerminationReached, int taskNumber = 0)
        {
            var algorithm = new GeneticAlgorithm(_populationFactory.GeneratePopulation(), _fitness, 
                                                 configuration.Selection, configuration.Crossover, configuration.Mutation)
            {
                Termination = new FitnessStagnationOrTresholdTermination(StagnantGenerationsNumber, MaxFitness),
                //TaskExecutor = new ParallelTaskExecutor()
                //{
                //    MinThreads = 4,
                //    MaxThreads = 16
                //}
            };

            algorithm.GenerationRan += (sender, e) =>
            {
                if (algorithm.BestChromosome.Fitness.Value > _bestFitness)
                {
                    _bestFitness = algorithm.BestChromosome.Fitness.Value;
                    Console.WriteLine($"{configuration.ToString().PadRight(70, ' ')}, Gen: {algorithm.GenerationsNumber.ToString().PadRight(4, ' ')}, Fitness: {_bestFitness}");
                }
            };

            algorithm.TerminationReached += (sender, e) =>
            {
                lock (_lock)
                {
                    onTerminationReached(algorithm.BestChromosome, configuration);
                }
            };

            try
            {
                Console.WriteLine($"Staring configuration: {configuration.ToString().PadRight(70, ' ')}, Task: {taskNumber}");
                algorithm.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while running genetic algorithm for configuration {configuration.ToString().PadRight(70, ' ')}, task: {taskNumber}");
                onTerminationReached(null, configuration);
            }

        }

    }
}
