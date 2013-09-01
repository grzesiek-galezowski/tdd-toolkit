using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;

namespace TddEbook.TddToolkit
{
  public static partial class Any
  {
    public static T Of<T>() where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();

      return _generator.Create<T>();
    }

    public static T Besides<T>(params T[] excludedValues) where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      return Any.ValueOtherThan(excludedValues);
    }

  }
}
