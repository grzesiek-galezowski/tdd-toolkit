using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {

    public static IEnumerable<T> Enumerable<T>()
    {
      return _any.Enumerable<T>();
    }

    public static IEnumerable<T> Enumerable<T>(int length)
    {
      return _any.Enumerable<T>(length);
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      return _any.EnumerableWithout(excluded);
    }

    public static T[] Array<T>()
    {
      return _any.Array<T>();
    }

    public static T[] Array<T>(int length)
    {
      return _any.Array<T>(length);
    }

    public static T[] ArrayWithout<T>(params T[] excluded)
    {
      return _any.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(params T[] included)
    {
      return _any.ArrayWith(included);
    }

    public static T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return _any.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return _any.ArrayWith(included);
    }

    public static List<T> List<T>()
    {
      return _any.List<T>();
    }

    public static List<T> List<T>(int length)
    {
      return _any.List<T>(length);
    }

    public static List<T> ListWithout<T>(params T[] excluded)
    {
      return _any.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(params T[] included)
    {
      return _any.ListWith(included);
    }

    public static List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return _any.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(IEnumerable<T> included)
    {
      return _any.ListWith(included);
    }

    public static IReadOnlyList<T> ReadOnlyList<T>()
    {
      return _any.ReadOnlyList<T>();
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return _any.ReadOnlyList<T>(length);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return _any.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return _any.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return _any.ReadOnlyListWithout(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return _any.ReadOnlyListWithout(items);
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return _any.SortedList<TKey, TValue>();
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      return _any.SortedList<TKey, TValue>(length);
    }


    public static ISet<T> Set<T>(int length)
    {
      return _any.Set<T>(length);
    }

    public static ISet<T> Set<T>()
    {
      return _any.Set<T>();
    }

    public static ISet<T> SortedSet<T>(int length)
    {
      return _any.SortedSet<T>(length);
    }

    public static ISet<T> SortedSet<T>()
    {
      return _any.SortedSet<T>();
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      return _any.Dictionary<TKey, TValue>(length);
    }

    public static Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return _any.DictionaryWithKeys<T, U>(keys);
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return _any.Dictionary<TKey, TValue>();
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return _any.ReadOnlyDictionary<TKey, TValue>(length);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
    {
      return _any.ReadOnlyDictionaryWithKeys<TKey, TValue>(keys);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return _any.ReadOnlyDictionary<TKey, TValue>();
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      return _any.SortedDictionary<TKey, TValue>(length);
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return _any.SortedDictionary<TKey, TValue>();
    }
    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return _any.ConcurrentDictionary<TKey, TValue>(length);
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return _any.ConcurrentDictionary<TKey, TValue>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>()
    {
      return _any.ConcurrentStack<T>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return _any.ConcurrentStack<T>(length);
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return _any.ConcurrentQueue<T>();
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return _any.ConcurrentQueue<T>(length);
    }

    public static ConcurrentBag<T> ConcurrentBag<T>()
    {
      return _any.ConcurrentBag<T>();
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return _any.ConcurrentBag<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return _any.EnumerableSortedDescending<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return _any.EnumerableSortedDescending<T>();
    }

    public static IEnumerator<T> Enumerator<T>()
    {
      return _any.Enumerator<T>();
    }

    public static object List(Type type)
    {
      return _any.List(type);
    }

    public static object Set(Type type)
    {
      return _any.Set(type);
    }

    public static object Dictionary(Type keyType, Type valueType)
    {
      return _any.Dictionary(keyType, valueType);
    }


    public static object SortedList(Type keyType, Type valueType)
    {
      return _any.SortedList(keyType, valueType);
    }

    public static object SortedSet(Type type)
    {
      return _any.SortedSet(type);
    }

    public static object SortedDictionary(Type keyType, Type valueType)
    {
      return _any.SortedDictionary(keyType, valueType);
    }

    public static object ConcurrentDictionary(Type keyType, Type valueType)
    {
      return _any.ConcurrentDictionary(keyType, valueType);
    }

    public static object Array(Type type)
    {
      return _any.Array(type);
    }

    private static ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      return _any.AddManyTo(collection, many);
    }

    public static object KeyValuePair(Type keyType, Type valueType)
    {
      return _any.KeyValuePair(keyType, valueType);
    }

    public static object Enumerator(Type type)
    {
      return _any.Enumerator(type);
    }


    public static object ConcurrentStack(Type type)
    {
      return _any.ConcurrentStack(type);
    }

    public static object ConcurrentQueue(Type type)
    {
      return _any.ConcurrentQueue(type);
    }

    public static object ConcurrentBag(Type type)
    {
      return _any.ConcurrentBag(type);
    }


  }
}
