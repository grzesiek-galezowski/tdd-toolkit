using System;
using KellermanSoftware.CompareNetObjects;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public static class ObjectGraph
  {
    public static CompareLogic Comparison()
    {
      var comparisonMechanism = new CompareLogic
        {
          Config = new ComparisonConfig()
          {
            CompareChildren = true,
            CompareFields = true,
            ComparePrivateFields = true,
            ComparePrivateProperties = true,
            CompareProperties = true,
            CompareReadOnly = true,
            MaxDifferences = 1
          }
        };
      AddCriteriaForComparingTypesReferenceTo(comparisonMechanism);
      return comparisonMechanism;
    }

    private static void AddCriteriaForComparingTypesReferenceTo(CompareLogic compareObjects)
    {
      compareObjects.Config.CustomComparers.Add(new ReflectionOrProxyComparer());
    }


  }
}