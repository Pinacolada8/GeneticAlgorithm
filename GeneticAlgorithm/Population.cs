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
    public List<Individual> Individuals { get; set; } = new();

    public static Population GenerateInitialPopulation(int popSize, int individualDimension, IndividualRestrictions? restrictions = null)
    {
        restrictions ??= new();

        for(var popIndex = 0; popIndex < popSize; popIndex++)
        {
            for(var i = 0; i < individualDimension; i++)
            {
                var value = 0.0;
                var rnd = new Random();

                switch (restrictions.ValueType)
                {
                    case IndividualValueType.doubleValue:
                        value = rnd.NextDouble();
                        break;
                    case IndividualValueType.intValue:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
