using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm;

public enum IndividualValueType
{
    doubleValue,
    intValue
}

public class IndividualRestrictions
{
    public IndividualValueType ValueType { get; set; } = IndividualValueType.doubleValue;

    public double Min { get; set; } = double.MinValue;
    public double Max { get; set; } = double.MaxValue;
}

public class Population
{

    public static List<Individual> GenerateInitialPopulation(int popSize, int individualDimension, IndividualRestrictions? restrictions = null)
    {
        restrictions ??= new();
        var population = new List<Individual>();

        for(var popIndex = 0; popIndex < popSize; popIndex++)
        {
            var individual = new Individual();
            for(var i = 0; i < individualDimension; i++)
            {
                var value = 0.0;
                var rnd = new Random();

                switch(restrictions.ValueType)
                {
                    case IndividualValueType.doubleValue:
                        var range = restrictions.Max - restrictions.Min;
                        value = rnd.NextDouble() * Math.Abs(range) + restrictions.Min;
                        break;
                    case IndividualValueType.intValue:
                        value = rnd.Next(Convert.ToInt32(restrictions.Min), Convert.ToInt32(restrictions.Max));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                individual.X.Add(value);
            }

            population.Add(individual);
        }

        return population;
    }
}
