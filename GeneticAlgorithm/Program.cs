// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using GeneticAlgorithm;
using ScottPlot;

Console.WriteLine("Execution Started");

const int popSize = 1000;

var idealSolution = 7.88;
var errorRange = 0.001;

var individuals = Population.GenerateInitialPopulation(popSize, 2, new() { Min = 0, Max = 10 });

var dataXInit = individuals.Select(x => x.X[0]).ToArray();
var dataYInit = individuals.Select(x => x.X[1]).ToArray();
var pltInit = new ScottPlot.Plot(800, 600);
pltInit.AddScatter(dataXInit, dataYInit, lineStyle: LineStyle.None);
pltInit.SaveFig($"../../../../Images/Initial-Pop.png");


var genetic = new GeneticAlgorithm.GeneticAlgorithm(individuals);
var bestFound = false;

for(var i = 0; i < 1000; i++)
{
    bestFound = genetic.RunOnce(idealSolution, errorRange);

    if(new List<int> { 10, 100, 200, 500, 1000, 10000 }.Contains(i + 1))
    {
        var dataX = genetic.Individuals.Select(x => x.X[0]).ToArray();
        var dataY = genetic.Individuals.Select(x => x.X[1]).ToArray();
        var plt = new ScottPlot.Plot(400, 300);
        plt.AddScatter(dataX, dataY, lineStyle: LineStyle.None);
        plt.SaveFig($"../../../../Images/Iteration-{i + 1}.png");
    }

    if(bestFound)
        break;
}

if(bestFound)
{
    Console.WriteLine($"Result Found: {genetic.CalcIndividual(genetic.Best)}");
    Console.WriteLine($"X Values=> X[0]: {genetic.Best.X[0]} - X[1]:{genetic.Best.X[1]}");
}
else
{
    Console.WriteLine($"Result Found: {genetic.CalcIndividual(genetic.Best)}");
    Console.WriteLine($"X Values=> X[0]: {genetic.Best.X[0]} - X[1]:{genetic.Best.X[1]}");
    Console.WriteLine($"Result was not within {errorRange} margin of error");
}