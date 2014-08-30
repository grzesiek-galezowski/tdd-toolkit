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

    public override bool IsTypeMatch(Type type1, Type type2)
    {
      return ((IsPartOfReflectionApi(type1) && IsPartOfReflectionApi(type2))
        || (IsDynamicProxy(type1) && IsDynamicProxy(type2)));
    }

    public override void CompareType(CompareParms _)
    {
      if (!ReferenceEquals(_.Object1, _.Object2))
      {
        _.Result.Differences.Add(new Difference
        {
          Object1Value = _.Object1.ToString(),
          Object2Value = _.Object2.ToString(),
          ActualName = "Reference to " + _.Object2Type + ": ",
          ExpectedName = "Reference to " + _.Object1Type,
          MessagePrefix = _.BreadCrumb
        });
      }

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
