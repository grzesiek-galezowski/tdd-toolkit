using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal abstract class AnyInstanceReturnValue
  {
    public static AnyInstanceReturnValue New(Type type)
    {
      if (type.IsPrimitive || type.IsValueType)
      {
        return ValueTypeReturnValue.New(type);
      }
      else
      {
        return ClassOrInterfaceReturnValue.New(type);
      }
    }

    public abstract object Generate();
  }
}