// See https://aka.ms/new-console-template for more information

using GeneticAlgorithm;
using GeneticAlgorithm.Models;

Console.WriteLine("Execution Started");

const int n = 2;

const int popSize = 100;

var idealSolution = 7.88;
var errorRange = 0.00001;

var individuals = Population.GenerateInitialPopulation(popSize, 2, new() { Min = -10, Max = 10});


