using System;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm;

public class IndividualInformation
{
    public Individual Individual { get; set; } = null!;

    public double Value { get; set; }

    public double Fitness { get; set; }
}

public class GeneticAlgorithm
{
    public void RunOnce(List<Individual> individuals, double idealSolution, double errorRange)
    {
        var inds = individuals.Select(x => new IndividualInformation()
        {
            Individual = x,
            Value = CalcIndividual(x)
        }).ToList();

        inds = CalcFitness(inds).ToList();

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

        var result = individuals.OrderBy(x => x.Fitness);

        return result;
    }
}
