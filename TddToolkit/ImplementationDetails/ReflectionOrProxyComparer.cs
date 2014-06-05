using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using System;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class ReflectionOrProxyComparer : BaseTypeComparer
  {
    public ReflectionOrProxyComparer()
      : base(null)
    {

    }

    public override void CompareType(
      ComparisonResult result, 
      object object1, 
      object object2, 
      string breadCrumb)
    {
        if (!ReferenceEquals(object1, object2))
        {
          result.Differences.Add(new Difference
          {
            Object1Value = object1.ToString(),
            Object2Value = object2.ToString(),
            ActualName = "Reference to " + object2.GetType() + ": ",
            ExpectedName = "Reference to " + object1.GetType(),
            MessagePrefix = breadCrumb
          });
        }
    }

    public override bool IsTypeMatch(Type type1, Type type2)
    {
      return ((IsPartOfReflectionApi(type1) && IsPartOfReflectionApi(type2))
        || (IsDynamicProxy(type1) && IsDynamicProxy(type2)));
    }

    private bool IsPartOfReflectionApi(Type type)
    {
      return type.Namespace != null && type.Namespace.StartsWith("System.Reflection");
    }

    private bool IsDynamicProxy(Type type)
    {
      return type.Namespace != null && type.Namespace.StartsWith("Castle.");
    }

  }
}
