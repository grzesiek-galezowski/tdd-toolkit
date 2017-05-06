using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class CollectionGenerator
  {
    private readonly GenericMethodProxyCalls _genericMethodProxyCalls;

    public CollectionGenerator(GenericMethodProxyCalls genericMethodProxyCalls)
    {
      _genericMethodProxyCalls = genericMethodProxyCalls;
    }

    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included, IProxyBasedGenerator proxyBasedGenerator)
    {
      var list = new List<T>();
      list.Add(proxyBasedGenerator.Instance<T>());
      list.AddRange(included);
      list.Add(proxyBasedGenerator.Instance<T>());

      return list;
    }

    public IEnumerable<T> Enumerable<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return Enumerable<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public IEnumerable<T> Enumerable<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return AddManyTo(new List<T>(), length, proxyBasedGenerator);
    }

    public IEnumerable<T> EnumerableWithout<T>(T[] excluded, IProxyBasedGenerator proxyBasedGenerator)
    {
      var result = new List<T>
      {
        proxyBasedGenerator.OtherThan(excluded),
        proxyBasedGenerator.OtherThan(excluded),
        proxyBasedGenerator.OtherThan(excluded)
      };
      return result;
    }

    public T[] Array<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return Array<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public T[] Array<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return Enumerable<T>(length, proxyBasedGenerator).ToArray();
    }

    public T[] ArrayWithout<T>(T[] excluded, IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableWithout(excluded, proxyBasedGenerator).ToArray();
    }

    public T[] ArrayWith<T>(T[] included, IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableWith(included, proxyBasedGenerator).ToArray();
    }

    public T[] ArrayWithout<T>(IEnumerable<T> excluded, IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableWithout(excluded.ToArray(), proxyBasedGenerator).ToArray();
    }

    public T[] ArrayWith<T>(IProxyBasedGenerator proxyBasedGenerator, IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray(), proxyBasedGenerator).ToArray();
    }

    public List<T> List<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return List<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public List<T> List<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return Enumerable<T>(length, proxyBasedGenerator).ToList();
    }

    public List<T> ListWithout<T>(T[] excluded, IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableWithout(excluded, proxyBasedGenerator).ToList();
    }

    public List<T> ListWith<T>(T[] included, IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableWith(included, proxyBasedGenerator).ToList();
    }

    public List<T> ListWithout<T>(IEnumerable<T> excluded, IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableWithout(excluded.ToArray(), proxyBasedGenerator).ToList();
    }

    public List<T> ListWith<T>(IEnumerable<T> included, IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableWith(included.ToArray(), proxyBasedGenerator).ToList();
    }

    public IReadOnlyList<T> ReadOnlyList<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return ReadOnlyList<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return List<T>(length, proxyBasedGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ListWith(items, proxyBasedGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(T[] items, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ListWith(items, proxyBasedGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ListWithout(items, proxyBasedGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(T[] items, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ListWithout(items, proxyBasedGenerator);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return SortedList<TKey, TValue>(AllGenerator.Many, proxyBasedGenerator);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      var list = new SortedList<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        list.Add(proxyBasedGenerator.Instance<TKey>(), proxyBasedGenerator.Instance<TValue>());
      }
      return list;
    }

    public ISet<T> Set<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return new HashSet<T>(Enumerable<T>(length, proxyBasedGenerator));
    }

    public ISet<T> Set<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return Set<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public ISet<T> SortedSet<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return new SortedSet<T>(Enumerable<T>(length, proxyBasedGenerator));
    }

    public ISet<T> SortedSet<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return SortedSet<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return new SpecialCasesOfResolutions<Dictionary<TKey, TValue>>(_genericMethodProxyCalls)
        .AnyDictionary<TKey, TValue>(proxyBasedGenerator, length);
    }

    public Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys, IProxyBasedGenerator proxyBasedGenerator)
    {
      var dict = Dictionary<T, U>(0, proxyBasedGenerator);
      foreach (var key in keys)
      {
        dict.Add(key, proxyBasedGenerator.InstanceOf<U>());
      }

      return dict;
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return Dictionary<TKey, TValue>(AllGenerator.Many, proxyBasedGenerator);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return Dictionary<TKey, TValue>(length, proxyBasedGenerator);
    }

    public IReadOnlyDictionary<T, U> ReadOnlyDictionaryWithKeys<T, U>(IEnumerable<T> keys, IProxyBasedGenerator proxyBasedGenerator)
    {
      return DictionaryWithKeys<T, U>(keys, proxyBasedGenerator);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return ReadOnlyDictionary<TKey, TValue>(AllGenerator.Many, proxyBasedGenerator);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return new SpecialCasesOfResolutions<SortedDictionary<TKey, TValue>>(_genericMethodProxyCalls)
        .SortedDictionary<TKey, TValue>(proxyBasedGenerator, length);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return SortedDictionary<TKey, TValue>(AllGenerator.Many, proxyBasedGenerator);
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      var dict = new ConcurrentDictionary<TKey, TValue>();
      for (var i = 0; i < length; ++i)
      {
        dict.TryAdd(proxyBasedGenerator.Instance<TKey>(), proxyBasedGenerator.Instance<TValue>());
      }
      return dict;
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return ConcurrentDictionary<TKey, TValue>(AllGenerator.Many, proxyBasedGenerator);
    }

    public ConcurrentStack<T> ConcurrentStack<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return ConcurrentStack<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      var coll = new ConcurrentStack<T>();
      for (var i = 0; i < length; ++i)
      {
        coll.Push(proxyBasedGenerator.Instance<T>());
      }
      return coll;
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return ConcurrentQueue<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      var coll = new ConcurrentQueue<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Enqueue(proxyBasedGenerator.Instance<T>());
      }
      return coll;

    }

    public ConcurrentBag<T> ConcurrentBag<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return ConcurrentBag<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      var coll = new ConcurrentBag<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Add(proxyBasedGenerator.Instance<T>());
      }
      return coll;

    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length, IProxyBasedGenerator proxyBasedGenerator)
    {
      return SortedSet<T>(length, proxyBasedGenerator).ToList();
    }

    public IEnumerable<T> EnumerableSortedDescending<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return EnumerableSortedDescending<T>(AllGenerator.Many, proxyBasedGenerator);
    }

    public IEnumerator<T> Enumerator<T>(IProxyBasedGenerator proxyBasedGenerator)
    {
      return List<T>(proxyBasedGenerator).GetEnumerator();
    }

    public object List(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ResultOfGenericVersionOfMethod(
        type, 
        MethodBase.GetCurrentMethod(), 
        proxyBasedGenerator);
    }

    public object Set(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), proxyBasedGenerator);
    }

    public object SortedList(Type keyType, Type valueType, IProxyBasedGenerator proxyBasedGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] {proxyBasedGenerator});
    }

    public object SortedSet(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, MethodBase.GetCurrentMethod().Name); 
    }

    public object ConcurrentDictionary(Type keyType, Type valueType, IProxyBasedGenerator proxyBasedGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] {proxyBasedGenerator});
    }

    public object Array(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, MethodBase.GetCurrentMethod().Name, new object[] {proxyBasedGenerator}); 
    }

    public ICollection<T> AddManyTo<T>(ICollection<T> collection, int many, IProxyBasedGenerator proxyBasedGenerator)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(proxyBasedGenerator.Instance<T>());
      }
      return collection;
    }

    public object Enumerator(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name, new object[] {proxyBasedGenerator}); 
    }

    public object ConcurrentStack(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), proxyBasedGenerator);
    }

    public object ConcurrentQueue(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), proxyBasedGenerator);
    }

    public object ConcurrentBag(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), proxyBasedGenerator);
    }

    private object ResultOfGenericVersionOfMethod(Type type, MethodBase currentMethod, IProxyBasedGenerator proxyBasedGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, currentMethod.Name, new object[] { proxyBasedGenerator });
    }

  }
}