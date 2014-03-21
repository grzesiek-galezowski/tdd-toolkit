using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    private const int Many = 3;

    public static IEnumerable<T> Enumerable<T>()
    {
      return Enumerable<T>(length: Many);
    }

    public static IEnumerable<T> Enumerable<T>(int length)
    {
      return AddManyTo(new List<T>(), length);
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      var result = new List<T>
      {
        OtherThan(excluded), 
        OtherThan(excluded), 
        OtherThan(excluded)
      };
      return result;
    }

    public static T[] Array<T>()
    {
      return Array<T>(Many);
    }

    public static T[] Array<T>(int length)
    {
      return Enumerable<T>(length).ToArray();
    }


    public static T[] ArrayWithout<T>(params T[] excluded)
    {
      return EnumerableWithout(excluded).ToArray();
    }

    public static List<T> List<T>()
    {
      return List<T>(Many);
    }

    public static List<T> List<T>(int length)
    {
      return Enumerable<T>(length).ToList();
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return SortedList<TKey, TValue>(Many);
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      var list = new SortedList<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        list.Add(Any.Instance<TKey>(), Any.Instance<TValue>());
      }
      return list;
    }


    public static ISet<T> Set<T>(int length)
    {
      return new HashSet<T>(Enumerable<T>(length));
    }

    public static ISet<T> Set<T>()
    {
      return Set<T>(Many);
    }

    public static ISet<T> SortedSet<T>(int length)
    {
      return new SortedSet<T>(Enumerable<T>(length));
    }

    public static ISet<T> SortedSet<T>()
    {
      return SortedSet<T>(Many);
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      var dict = new Dictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(Any.Instance<TKey>(), Any.Instance<TValue>());
      }
      return dict;
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return Dictionary<TKey, TValue>(Many);
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      var dict = new SortedDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(Any.Instance<TKey>(), Any.Instance<TValue>());
      }
      return dict;
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return SortedDictionary<TKey, TValue>(Many);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return SortedSet<T>(length).ToList();
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return EnumerableSortedDescending<T>(Many);
    }


    public static object List(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object Set(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object Dictionary(Type keyType, Type valueType)
    {
      return ResultOfGenericVersionOfMethod(keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public static object SortedList(Type keyType, Type valueType)
    {
      return ResultOfGenericVersionOfMethod(keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public static object SortedSet(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name); 
    }

    public static object SortedDictionary(Type keyType, Type valueType)
    {
      return ResultOfGenericVersionOfMethod(keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public static object Array(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name); 
    }

    private static ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(Any.Instance<T>());
      }
      return collection;
    }

    public static object KeyValuePair(Type keyType, Type valueType)
    {
      return Activator.CreateInstance(
        typeof (KeyValuePair<,>).MakeGenericType(keyType, valueType), 
        Any.Instance(keyType), Any.Instance(valueType)
        );
    }
  }
}
