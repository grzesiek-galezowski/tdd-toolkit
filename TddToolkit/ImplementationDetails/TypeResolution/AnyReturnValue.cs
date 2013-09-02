using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal abstract class AnyReturnValue
  {
    public static AnyReturnValue Of(Type type)
    {
      if (type.IsPrimitive || type.IsValueType)
      {
        return ValueTypeReturnValue.Of(type);
      }
      else
      {
        return ClassOrInterfaceReturnValue.Of(type);
      }
    }

    public abstract object Generate();
  }
}