using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public class StructComparisonOperator : IBinaryOperator
  {
    public object Evaluate(object instance1, object instance2)
    {
      return ValueType.Equals(instance1, instance2);
    }
  }
}
