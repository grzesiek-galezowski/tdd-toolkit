using System;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  internal static class ValueObjectWhiteList
  {
    public static bool Contains<T>()
    {
      var type = typeof(T);
      if (type == typeof(object))
      {
        return true;
      }

      if (type == typeof(string))
      {
        return true;
      }

      if (type == typeof(Guid))
      {
        return true;
      }

      if (type.IsEnum)
      {
        return true;
      }

      if (type.IsPrimitive)
      {
        return true;
      }

      return false;
    }
  }
}