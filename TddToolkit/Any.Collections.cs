using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;

namespace TddEbook.TddToolkit
{
  public static partial class Any
  {
    public static SortedSet<T> SortedSet<T>()
    {
      return new SortedSet<T>
      {
        ValueOf<T>(),
        ValueOf<T>(),
        ValueOf<T>()
      };
    }

    public static IEnumerable<T> EnumerableOfDerivativesFrom<T>() where T : class
    {
      return ListOfDerivativesFrom<T>();
    }

    public static IEnumerable<T> ListOfDerivativesFrom<T>() where T : class
    {
      return new List<T>
      {
        InstanceOf<T>(),
        InstanceOf<T>(),
        InstanceOf<T>()
      };
    }

    public static IEnumerable<T> Enumerable<T>()
    {
      return Generator.CreateMany<T>();
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded) where T : class
    {
      var result = new List<T>
      {
        ValueOtherThan(excluded), 
        ValueOtherThan(excluded), 
        ValueOtherThan(excluded)
      };
      return result;
    }

    public static T[] Array<T>()
    {
      return Enumerable<T>().ToArray();
    }

    public static T[] ArrayWithout<T>(params T[] excluded) where T : class
    {
      return EnumerableWithout(excluded).ToArray();
    }

    public static List<T> List<T>()
    {
      return Enumerable<T>().ToList();
    }

    public static ISet<T> Set<T>()
    {
      return ValueOf<HashSet<T>>();
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return ValueOf<Dictionary<TKey, TValue>>();
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return SortedSet<T>().ToList();
    }

    public static object EnumerableOfDerivativesFrom(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object ListOfDerivativesFrom(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object List(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }
  }
}
