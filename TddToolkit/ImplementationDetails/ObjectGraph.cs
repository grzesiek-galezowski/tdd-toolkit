using KellermanSoftware.CompareNetObjects;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public static class ObjectGraph
  {
    public static CompareLogic Comparison()
    {
      var comparisonMechanism = new CompareLogic
      {
        Config = new ComparisonConfig
        {
          CompareChildren = true,
          CompareFields = true,
          ComparePrivateFields = true,
          ComparePrivateProperties = true,
          CompareProperties = true,
          CompareReadOnly = true,
          CompareStaticFields = false,
          CompareStaticProperties = false,
          MaxDifferences = 1
        }
      };
      comparisonMechanism.Config.CustomComparers.Add(new ReflectionOrProxyComparer());
      return comparisonMechanism;
    }



  }
}