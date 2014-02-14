using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection
{
  public static class TypeOfType
  {
    public static bool Is<T>()
    {
      var type = typeof (T);
      return type.FullName == typeof (Type).FullName || IsTypeWithinBaseHierarchyOf(type);
    }

    private static bool IsTypeWithinBaseHierarchyOf(Type type)
    {
      var baseType = type.BaseType;
      if (baseType == null)
      {
        return false;
      }
      else if(baseType.FullName == typeof(Type).FullName)
      {
        return true;
      }
      else
      {
        return IsTypeWithinBaseHierarchyOf(baseType);
      }
      
    }
  }
}