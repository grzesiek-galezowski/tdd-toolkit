using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {

    public static IEnumerable<T> Enumerable<T>()
    {
      return Generate.Enumerable<T>();
    }

    public static IEnumerable<T> Enumerable<T>(int length)
    {
      return Generate.Enumerable<T>(length);
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      return Generate.EnumerableWithout(excluded);
    }

    public static T[] Array<T>()
    {
      return Generate.Array<T>();
    }

    public static T[] Array<T>(int length)
    {
      return Generate.Array<T>(length);
    }

    public static T[] ArrayWithout<T>(params T[] excluded)
    {
      return Generate.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(params T[] included)
    {
      return Generate.ArrayWith(included);
    }

    public static T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return Generate.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return Generate.ArrayWith(included);
    }

    public static List<T> List<T>()
    {
      return Generate.List<T>();
    }

    public static List<T> List<T>(int length)
    {
      return Generate.List<T>(length);
    }

    public static List<T> ListWithout<T>(params T[] excluded)
    {
      return Generate.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(params T[] included)
    {
      return Generate.ListWith(included);
    }

    public static List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return Generate.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(IEnumerable<T> included)
    {
      return Generate.ListWith(included);
    }

    public static IReadOnlyList<T> ReadOnlyList<T>()
    {
      return Generate.ReadOnlyList<T>();
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return Generate.ReadOnlyList<T>(length);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return Generate.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return Generate.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return Generate.ReadOnlyListWithout(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return Generate.ReadOnlyListWithout(items);
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return Generate.SortedList<TKey, TValue>();
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      return Generate.SortedList<TKey, TValue>(length);
    }


    public static ISet<T> Set<T>(int length)
    {
      return Generate.Set<T>(length);
    }

    public static ISet<T> Set<T>()
    {
      return Generate.Set<T>();
    }

    public static ISet<T> SortedSet<T>(int length)
    {
      return Generate.SortedSet<T>(length);
    }

    public static ISet<T> SortedSet<T>()
    {
      return Generate.SortedSet<T>();
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      return Generate.Dictionary<TKey, TValue>(length);
    }

    public static Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return Generate.DictionaryWithKeys<T, U>(keys);
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return Generate.Dictionary<TKey, TValue>();
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return Generate.ReadOnlyDictionary<TKey, TValue>(length);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
    {
      return Generate.ReadOnlyDictionaryWithKeys<TKey, TValue>(keys);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return Generate.ReadOnlyDictionary<TKey, TValue>();
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      return Generate.SortedDictionary<TKey, TValue>(length);
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return Generate.SortedDictionary<TKey, TValue>();
    }
    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return Generate.ConcurrentDictionary<TKey, TValue>(length);
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return Generate.ConcurrentDictionary<TKey, TValue>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>()
    {
      return Generate.ConcurrentStack<T>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return Generate.ConcurrentStack<T>(length);
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return Generate.ConcurrentQueue<T>();
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return Generate.ConcurrentQueue<T>(length);
    }

    public static ConcurrentBag<T> ConcurrentBag<T>()
    {
      return Generate.ConcurrentBag<T>();
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return Generate.ConcurrentBag<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return Generate.EnumerableSortedDescending<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return Generate.EnumerableSortedDescending<T>();
    }

    public static IEnumerator<T> Enumerator<T>()
    {
      return Generate.Enumerator<T>();
    }

    public static object List(Type type)
    {
      return Generate.List(type);
    }

    public static object Set(Type type)
    {
      return Generate.Set(type);
    }

    public static object Dictionary(Type keyType, Type valueType)
    {
      return Generate.Dictionary(keyType, valueType);
    }


    public static object SortedList(Type keyType, Type valueType)
    {
      return Generate.SortedList(keyType, valueType);
    }

    public static object SortedSet(Type type)
    {
      return Generate.SortedSet(type);
    }

    public static object SortedDictionary(Type keyType, Type valueType)
    {
      return Generate.SortedDictionary(keyType, valueType);
    }

    public static object ConcurrentDictionary(Type keyType, Type valueType)
    {
      return Generate.ConcurrentDictionary(keyType, valueType);
    }

    public static object Array(Type type)
    {
      return Generate.Array(type);
    }

    private static ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      return Generate.AddManyTo(collection, many);
    }

    public static object KeyValuePair(Type keyType, Type valueType)
    {
      return Generate.KeyValuePair(keyType, valueType);
    }

    public static object Enumerator(Type type)
    {
      return Generate.Enumerator(type);
    }


    public static object ConcurrentStack(Type type)
    {
      return Generate.ConcurrentStack(type);
    }

    public static object ConcurrentQueue(Type type)
    {
      return Generate.ConcurrentQueue(type);
    }

    public static object ConcurrentBag(Type type)
    {
      return Generate.ConcurrentBag(type);
    }


  }
}
