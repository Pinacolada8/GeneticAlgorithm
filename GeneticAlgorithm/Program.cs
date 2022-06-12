// See https://aka.ms/new-console-template for more information

using GeneticAlgorithm;
using GeneticAlgorithm.Models;

Console.WriteLine("Execution Started");

const int n = 2;

const int popSize = 100;

var idealSolution = 7.88;
var errorRange = 0.01;

var individuals = Population.GenerateInitialPopulation(popSize, 2, new() { Min = 0, Max = 10});


var genetic = new GeneticAlgorithm.GeneticAlgorithm(individuals);
var bestFound = false;

for (var i = 0; i < 10000; i++)
{
    bestFound = genetic.RunOnce(idealSolution,errorRange);
    if (bestFound)
        break;
}

if (bestFound)
{
    Console.WriteLine($"Func Result: {genetic.CalcIndividual(genetic.Best)}");
    Console.WriteLine($"Best solution found. X[0]: {genetic.Best.X[0]} - X[1]:{genetic.Best.X[1]}");
}
else
{
    Console.WriteLine($"Func Result: {genetic.CalcIndividual(genetic.Best)}");
    Console.WriteLine($"Best solution was NOT found. X[0]: {genetic.Best.X[0]} - X[1]:{genetic.Best.X[1]}");
}