using System;
using System.Reflection;

namespace TddEbook.TypeReflection
{
  public static class TypeOfType
  {
    public static bool Is<T>()
    {
      var type = typeof (T);
      return type.FullName == typeof (Type).FullName || IsTypeOfTypeWithinBaseHierarchyOf(type);
    }

    private static bool IsTypeOfTypeWithinBaseHierarchyOf(Type type)
    {
      var baseType = type.GetTypeInfo().BaseType;
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
        return IsTypeOfTypeWithinBaseHierarchyOf(baseType);
      }
      
    }
  }
}