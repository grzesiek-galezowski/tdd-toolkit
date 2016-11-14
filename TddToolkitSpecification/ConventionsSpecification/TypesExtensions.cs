using System;
using System.Linq;
using TestStack.ConventionTests.ConventionData;

namespace TddEbook.TddToolkitSpecification.ConventionsSpecification
{
  public static class TypesExtensions
  {
    public static Types Without(this Types types, Type excludedType)
    {
      var enumerable = types.Where(t => t != excludedType).ToArray();
      types = Types.InCollection(
        enumerable, types.Description);
      return types;
    }
  }
}