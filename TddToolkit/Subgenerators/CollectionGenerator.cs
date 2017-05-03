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
    private readonly IProxyBasedGenerator _proxyBasedGenerator;
    private readonly GenericMethodProxyCalls _genericMethodProxyCalls;

    public CollectionGenerator(
      IProxyBasedGenerator proxyBasedGenerator, 
      GenericMethodProxyCalls genericMethodProxyCalls)
    {
      _proxyBasedGenerator = proxyBasedGenerator;
      _genericMethodProxyCalls = genericMethodProxyCalls;
    }

    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      var list = new List<T>();
      list.Add(_proxyBasedGenerator.Instance<T>());
      list.AddRange(included);
      list.Add(_proxyBasedGenerator.Instance<T>());

      return list;
    }

    public IEnumerable<T> Enumerable<T>()
    {
      return Enumerable<T>(length: AllGenerator.Many);
    }

    public IEnumerable<T> Enumerable<T>(int length)
    {
      return AddManyTo(new List<T>(), length);
    }

    public IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      var result = new List<T>
      {
        _proxyBasedGenerator.OtherThan(excluded),
        _proxyBasedGenerator.OtherThan(excluded),
        _proxyBasedGenerator.OtherThan(excluded)
      };
      return result;
    }

    public T[] Array<T>()
    {
      return Array<T>(AllGenerator.Many);
    }

    public T[] Array<T>(int length)
    {
      return Enumerable<T>(length).ToArray();
    }

    public T[] ArrayWithout<T>(params T[] excluded)
    {
      return EnumerableWithout(excluded).ToArray();
    }

    public T[] ArrayWith<T>(params T[] included)
    {
      return EnumerableWith(included).ToArray();
    }

    public T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return EnumerableWithout(excluded.ToArray()).ToArray();
    }

    public T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray()).ToArray();
    }

    public List<T> List<T>()
    {
      return List<T>(AllGenerator.Many);
    }

    public List<T> List<T>(int length)
    {
      return Enumerable<T>(length).ToList();
    }

    public List<T> ListWithout<T>(params T[] excluded)
    {
      return EnumerableWithout(excluded).ToList();
    }

    public List<T> ListWith<T>(params T[] included)
    {
      return EnumerableWith(included).ToList();
    }

    public List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return EnumerableWithout(excluded.ToArray()).ToList();
    }

    public List<T> ListWith<T>(IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray()).ToList();
    }

    public IReadOnlyList<T> ReadOnlyList<T>()
    {
      return ReadOnlyList<T>(AllGenerator.Many);
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return List<T>(length);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return ListWith(items);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return ListWith(items);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return ListWithout(items);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return ListWithout(items);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return SortedList<TKey, TValue>(AllGenerator.Many);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      var list = new SortedList<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        list.Add(_proxyBasedGenerator.Instance<TKey>(), _proxyBasedGenerator.Instance<TValue>());
      }
      return list;
    }

    public ISet<T> Set<T>(int length)
    {
      return new HashSet<T>(Enumerable<T>(length));
    }

    public ISet<T> Set<T>()
    {
      return Set<T>(AllGenerator.Many);
    }

    public ISet<T> SortedSet<T>(int length)
    {
      return new SortedSet<T>(Enumerable<T>(length));
    }

    public ISet<T> SortedSet<T>()
    {
      return SortedSet<T>(AllGenerator.Many);
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      return new SpecialCasesOfResolutions<Dictionary<TKey, TValue>>(_genericMethodProxyCalls)
        .AnyDictionary<TKey, TValue>(_proxyBasedGenerator, length);
    }

    public Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      var dict = Dictionary<T, U>(0);
      foreach (var key in keys)
      {
        dict.Add(key, _proxyBasedGenerator.InstanceOf<U>());
      }

      return dict;
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return Dictionary<TKey, TValue>(AllGenerator.Many);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return Dictionary<TKey, TValue>(length);
    }

    public IReadOnlyDictionary<T, U> ReadOnlyDictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return DictionaryWithKeys<T, U>(keys);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return ReadOnlyDictionary<TKey, TValue>(AllGenerator.Many);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      var dict = new SortedDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(_proxyBasedGenerator.Instance<TKey>(), _proxyBasedGenerator.Instance<TValue>());
      }
      return dict;
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return SortedDictionary<TKey, TValue>(AllGenerator.Many);
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      var dict = new ConcurrentDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.TryAdd(_proxyBasedGenerator.Instance<TKey>(), _proxyBasedGenerator.Instance<TValue>());
      }
      return dict;

    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return ConcurrentDictionary<TKey, TValue>(AllGenerator.Many);
    }

    public ConcurrentStack<T> ConcurrentStack<T>()
    {
      return ConcurrentStack<T>(AllGenerator.Many);
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      var coll = new ConcurrentStack<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Push(_proxyBasedGenerator.Instance<T>());
      }
      return coll;
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return ConcurrentQueue<T>(AllGenerator.Many);
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      var coll = new ConcurrentQueue<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Enqueue(_proxyBasedGenerator.Instance<T>());
      }
      return coll;

    }

    public ConcurrentBag<T> ConcurrentBag<T>()
    {
      return ConcurrentBag<T>(AllGenerator.Many);
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      var coll = new ConcurrentBag<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Add(_proxyBasedGenerator.Instance<T>());
      }
      return coll;

    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return SortedSet<T>(length).ToList();
    }

    public IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return EnumerableSortedDescending<T>(AllGenerator.Many);
    }

    public IEnumerator<T> Enumerator<T>()
    {
      return List<T>().GetEnumerator();
    }

    public object List(Type type)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, MethodBase.GetCurrentMethod().Name);
    }

    public object Set(Type type)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name);
    }

    public object SortedList(Type keyType, Type valueType)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public object SortedSet(Type type)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, MethodBase.GetCurrentMethod().Name); 
    }

    public object SortedDictionary(Type keyType, Type valueType)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public object ConcurrentDictionary(Type keyType, Type valueType)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name);
    }

    public object Array(Type type)
    {
      //bug is this implementation OK?
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, MethodBase.GetCurrentMethod().Name); 
    }

    public ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(_proxyBasedGenerator.Instance<T>());
      }
      return collection;
    }

    public object Enumerator(Type type)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name); 
    }

    public object ConcurrentStack(Type type)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name);
    }

    public object ConcurrentQueue(Type type)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name);
    }

    public object ConcurrentBag(Type type)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name);
    }
  }
}