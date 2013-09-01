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
      return new SortedSet<T> {
        Any.ValueOf<T>(),
        Any.ValueOf<T>(),
        Any.ValueOf<T>()
      };
    }

    public static IEnumerable<T> EnumerableOfDerivativesFrom<T>() where T : class
    {
      return Any.ListOfDerivativesFrom<T>();
    }

    public static IEnumerable<T> ListOfDerivativesFrom<T>() where T : class
    {
      return new List<T>() {
        Any.InstanceOf<T>(),
        Any.InstanceOf<T>(),
        Any.InstanceOf<T>()
      };
    }

    public static IEnumerable<T> Enumerable<T>()
    {
      return _generator.CreateMany<T>();
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded) where T : class
    {
      var result = new List<T>();
      result.Add(Any.ValueOtherThan(excluded));
      result.Add(Any.ValueOtherThan(excluded));
      result.Add(Any.ValueOtherThan(excluded));
      return result;
    }

    public static T[] Array<T>()
    {
      return Any.Enumerable<T>().ToArray();
    }

    public static T[] ArrayWithout<T>(params T[] excluded) where T : class
    {
      return Any.EnumerableWithout(excluded).ToArray();
    }

    public static List<T> List<T>()
    {
      return Any.Enumerable<T>().ToList();
    }

    public static ISet<T> Set<T>()
    {
      return Any.ValueOf<HashSet<T>>();
    }

    public static Dictionary<T, U> Dictionary<T, U>()
    {
      return Any.ValueOf<Dictionary<T, U>>();
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return Any.SortedSet<T>().ToList();
    }

    public static object EnumerableOfDerivativesFrom(Type type)
    {
      return Any.ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object ListOfDerivativesFrom(Type type)
    {
      return Any.ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object List(Type type)
    {
      return Any.ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

  }
}
