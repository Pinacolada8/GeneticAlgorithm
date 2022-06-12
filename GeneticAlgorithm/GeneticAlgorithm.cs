using System;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm;

public class IndividualInformation
{
    public Individual Individual { get; set; } = null!;

    public double Value { get; set; }

    public double Fitness { get; set; }

    public double FitnessAccumulated { get; set; }
}

public class GeneticAlgorithm
{
    public List<Individual> Individuals { get; set; }

    public Individual Best { get; set; }

    public GeneticAlgorithm(List<Individual> individuals)
    {
        Individuals = individuals;
    }

    public bool RunOnce(double idealSolution, double errorRange)
    {
        var inds = Individuals.Select(x => new IndividualInformation()
        {
            Individual = x,
            Value = CalcIndividual(x)
        }).ToList();

        inds = CalcFitness(inds).ToList();

        var bestInf = inds.First();
        Best = new Individual()
        {
            X = bestInf.Individual.X.ToList()
        };

        if(Math.Abs(bestInf.Value - idealSolution) < errorRange)
            return true;

        var selectedIndividuals = SelectIndividuals(inds, Individuals.Count);

        var childs = CrossOver(selectedIndividuals);

        childs = Mutate(childs, 0.5, 10, 0);

        // Elitism
        childs[0] = Best;

        Individuals = childs;
        return false;
    }

    public double CalcIndividual(Individual individual)
    {
        var result = 1.0;
        foreach(var x in individual.X)
            result = result * Math.Sqrt(x) * Math.Sin(x);

        return result;
    }

    public IEnumerable<IndividualInformation> CalcFitness(List<IndividualInformation> individuals)
    {
        var min = individuals.Min(x => x.Value);

        min = min >= 0 ? 0 : Math.Abs(min);

        individuals.ForEach(x => x.Fitness = x.Value + min);

        var result = individuals.OrderByDescending(x => x.Fitness);

        return result;
    }

    public List<IndividualInformation> SelectIndividuals(List<IndividualInformation> individuals, int qtd)
    {
        var rnd = new Random();
        var sum = 0.0;
        foreach(var individual in individuals)
        {
            individual.FitnessAccumulated = sum;
            sum += individual.Fitness;
        }

        sum -= individuals.Last().Fitness;

        var selectedIndividuals = new List<IndividualInformation>();
        for(var i = 0; i < qtd; i++)
        {
            var rndValue = rnd.NextDouble() * sum;

            var selected = individuals.First(x => x.FitnessAccumulated >= rndValue);

            selectedIndividuals.Add(selected);
        }

        return selectedIndividuals;
    }

    public List<Individual> CrossOver(List<IndividualInformation> individuals)
    {
        var childs = new List<Individual>();
        var halfSize = individuals.Count / 2;
        for(var i = 0; i < halfSize; i++)
        {
            var parent1 = individuals[i];
            var parent2 = individuals[i + halfSize];

            var rnd = new Random();
            var alfa = rnd.NextDouble();
            childs.AddRange(CrossOver(parent1.Individual, parent2.Individual, alfa));

        }

        return childs;
    }

    public List<Individual> CrossOver(Individual parent1, Individual parent2, double alfa)
    {

        var x1Arr = parent1.X.Select((t, i) => alfa * t + (1 - alfa) * parent2.X[i]).ToList();

        var x2Arr = parent1.X.Select((t, i) => alfa * parent2.X[i] + (1 - alfa) * t).ToList();

        return new() { new() { X = x1Arr }, new() { X = x2Arr } };
    }

    public List<Individual> Mutate(List<Individual> individuals, double mutationRate, double maxValue, double minValue)
    {
        var rnd = new Random();
        individuals.ForEach(ind => ind.X = ind.X.Select(x =>
        {
            if(rnd.NextDouble() < mutationRate)
            {
                if(rnd.NextDouble() < 0.5)
                {
                    var newX = x + rnd.NextDouble() * 0.01;
                    return newX < maxValue ? newX : maxValue;
                }
                else
                {
                    var newX = x - rnd.NextDouble() * 0.01;
                    return newX > minValue ? newX : minValue;
                }
            }
            else
            {
                return x;
            }

        }).ToList());
        return individuals;
    }

}
