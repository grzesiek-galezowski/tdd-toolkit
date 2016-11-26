using System;
using System.Collections.Concurrent;
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

    public static T[] ArrayWith<T>(params T[] included)
    {
      return EnumerableWith(included).ToArray();
    }

    public static T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return EnumerableWithout(excluded.ToArray()).ToArray();
    }

    public static T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray()).ToArray();
    }

    public static List<T> List<T>()
    {
      return List<T>(Many);
    }

    public static List<T> List<T>(int length)
    {
      return Enumerable<T>(length).ToList();
    }

    public static List<T> ListWithout<T>(params T[] excluded)
    {
      return EnumerableWithout(excluded).ToList();
    }

    public static List<T> ListWith<T>(params T[] included)
    {
      return EnumerableWith(included).ToList();
    }

    public static List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return EnumerableWithout(excluded.ToArray()).ToList();
    }

    public static List<T> ListWith<T>(IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray()).ToList();
    }

    public static IReadOnlyList<T> ReadOnlyList<T>()
    {
      return ReadOnlyList<T>(Many);
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return Any.List<T>(length);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return Any.ListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return Any.ListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return Any.ListWithout(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return Any.ListWithout(items);
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
        list.Add(Instance<TKey>(), Instance<TValue>());
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
        dict.Add(Instance<TKey>(), Instance<TValue>());
      }
      return dict;
    }

    public static Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      var dict = Dictionary<T, U>(0);
      foreach (var key in keys)
      {
        dict.Add(key, InstanceOf<U>());
      }

      return dict;
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return Dictionary<TKey, TValue>(Many);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return Dictionary<TKey, TValue>(length);
    }

    public static IReadOnlyDictionary<T, U> ReadOnlyDictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return Any.DictionaryWithKeys<T, U>(keys);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return ReadOnlyDictionary<TKey, TValue>(Many);
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      var dict = new SortedDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(Instance<TKey>(), Instance<TValue>());
      }
      return dict;
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return SortedDictionary<TKey, TValue>(Many);
    }
    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      var dict = new ConcurrentDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.TryAdd(Instance<TKey>(), Instance<TValue>());
      }
      return dict;

    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return ConcurrentDictionary<TKey, TValue>(Many);
    }

    public static ConcurrentStack<T> ConcurrentStack<T>()
    {
      return ConcurrentStack<T>(Many);
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      var coll = new ConcurrentStack<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Push(Instance<T>());
      }
      return coll;
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return ConcurrentQueue<T>(Many);
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      var coll = new ConcurrentQueue<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Enqueue(Instance<T>());
      }
      return coll;

    }

    public static ConcurrentBag<T> ConcurrentBag<T>()
    {
      return ConcurrentBag<T>(Many);
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      var coll = new ConcurrentBag<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Add(Instance<T>());
      }
      return coll;

    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return SortedSet<T>(length).ToList();
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return EnumerableSortedDescending<T>(Many);
    }

    public static IEnumerator<T> Enumerator<T>()
    {
      return List<T>().GetEnumerator();
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

    public static object ConcurrentDictionary(Type keyType, Type valueType)
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
        collection.Add(Instance<T>());
      }
      return collection;
    }

    public static object KeyValuePair(Type keyType, Type valueType)
    {
      return Activator.CreateInstance(
        typeof (KeyValuePair<,>).MakeGenericType(keyType, valueType), 
        Instance(keyType), Instance(valueType)
        );
    }

    public static object Enumerator(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name); 
    }


    public static object ConcurrentStack(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object ConcurrentQueue(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }

    public static object ConcurrentBag(Type type)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod().Name);
    }


  }
}
