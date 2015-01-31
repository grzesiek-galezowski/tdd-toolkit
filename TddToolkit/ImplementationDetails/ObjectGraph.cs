using KellermanSoftware.CompareNetObjects;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public static class ObjectGraph
  {
    private static readonly CompareLogic _comparisonMechanism;

    static ObjectGraph()
    {
      _comparisonMechanism = new CompareLogic
      {
        Config = new ComparisonConfig
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

      _comparisonMechanism.Config.CustomComparers.Add(new ReflectionOrProxyComparer());
    }

    public static CompareLogic Comparison()
    {
      return _comparisonMechanism;
    }



  }
}