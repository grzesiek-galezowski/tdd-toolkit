using System;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    public static T Of<T>() where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();

      return ValueOf<T>();
    }

    public static T Besides<T>(params T[] excludedValues) where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      return ValueOtherThan(excludedValues);
    }
  }
}
