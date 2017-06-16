using System;

namespace TddEbook.TypeReflection
{
  public class DefaultValue
  {
    public static object Of(Type t)
    {
      if (t.IsValueType)
      {
        return Activator.CreateInstance(t);
      }

      return null;
    }
  }
}