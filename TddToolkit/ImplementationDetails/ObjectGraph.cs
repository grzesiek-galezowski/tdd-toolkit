using System;
using KellermanSoftware.CompareNetObjects;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public static class ObjectGraph
  {
    public static CompareObjects Comparison()
    {
      var comparisonMechanism = new CompareObjects
        {
          CompareChildren = true,
          CompareFields = true,
          ComparePrivateFields = true,
          ComparePrivateProperties = true,
          CompareProperties = true,
          CompareReadOnly = true,
          MaxDifferences = 1
        };
      AddCriteriaForComparingTypesReferenceTo(comparisonMechanism);
      return comparisonMechanism;
    }

    private static void AddCriteriaForComparingTypesReferenceTo(CompareObjects compareObjects)
    {
      compareObjects.IsUseCustomTypeComparer = type => IsPartOfReflectionApi(type) || IsDynamicProxy(type);

      compareObjects.CustomComparer = (objects, o1, o2, arg4) =>
        {
          if (!object.ReferenceEquals(o1, o2))
          {
            objects.Differences.Add(new Difference
              {
                Object1Value = o1.ToString(),
                Object2Value = o2.ToString(),
                ActualName = "Reference to " + o2.GetType() + ": ",
                ExpectedName = "Reference to " + o1.GetType(),
              });
          }
        };
    }

    private static bool IsPartOfReflectionApi(Type type)
    {
      return type.Namespace != null && type.Namespace.StartsWith("System.Reflection");
    }

    private static bool IsDynamicProxy(Type type)
    {
      return type.Namespace != null && type.Namespace.StartsWith("Castle.");
    }

  }
}