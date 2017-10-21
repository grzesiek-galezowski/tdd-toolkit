using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TddToolkit.TypeResolution.Interfaces;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class CollectionGenerator : ICollectionGenerator
  {
    private readonly GenericMethodProxyCalls _genericMethodProxyCalls;

    public CollectionGenerator(GenericMethodProxyCalls genericMethodProxyCalls)
    {
      _genericMethodProxyCalls = genericMethodProxyCalls;
    }

    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included, IInstanceGenerator instanceGenerator)
    {
      var list = new List<T>();
      list.Add(instanceGenerator.Instance<T>());
      list.AddRange(included);
      list.Add(instanceGenerator.Instance<T>());

      return list;
    }

    public IEnumerable<T> Enumerable<T>(IInstanceGenerator instanceGenerator)
    {
      return Enumerable<T>(AllGenerator.Many, instanceGenerator);
    }

    public IEnumerable<T> Enumerable<T>(int length, IInstanceGenerator instanceGenerator)
    {
      return AddManyTo(new List<T>(), length, instanceGenerator);
    }

    public IEnumerable<T> EnumerableWithout<T>(T[] excluded, IInstanceGenerator instanceGenerator)
    {
      var result = new List<T>
      {
        instanceGenerator.OtherThan(excluded),
        instanceGenerator.OtherThan(excluded),
        instanceGenerator.OtherThan(excluded)
      };
      return result;
    }

    public T[] Array<T>(IInstanceGenerator instanceGenerator)
    {
      return Array<T>(AllGenerator.Many, instanceGenerator);
    }

    public T[] Array<T>(int length, IInstanceGenerator instanceGenerator)
    {
      return Enumerable<T>(length, instanceGenerator).ToArray();
    }

    public T[] ArrayWithout<T>(T[] excluded, IInstanceGenerator instanceGenerator)
    {
      return EnumerableWithout(excluded, instanceGenerator).ToArray();
    }

    public T[] ArrayWith<T>(T[] included, IInstanceGenerator instanceGenerator)
    {
      return EnumerableWith(included, instanceGenerator).ToArray();
    }

    public T[] ArrayWithout<T>(IEnumerable<T> excluded, IInstanceGenerator instanceGenerator)
    {
      return EnumerableWithout(excluded.ToArray(), instanceGenerator).ToArray();
    }

    public T[] ArrayWith<T>(IInstanceGenerator instanceGenerator, IEnumerable<T> included)
    {
      return EnumerableWith(included.ToArray(), instanceGenerator).ToArray();
    }

    public List<T> List<T>(IInstanceGenerator instanceGenerator)
    {
      return List<T>(AllGenerator.Many, instanceGenerator);
    }

    public List<T> List<T>(int length, IInstanceGenerator instanceGenerator)
    {
      return Enumerable<T>(length, instanceGenerator).ToList();
    }

    public List<T> ListWithout<T>(T[] excluded, IInstanceGenerator instanceGenerator)
    {
      return EnumerableWithout(excluded, instanceGenerator).ToList();
    }

    public List<T> ListWith<T>(T[] included, IInstanceGenerator instanceGenerator)
    {
      return EnumerableWith(included, instanceGenerator).ToList();
    }

    public List<T> ListWithout<T>(IEnumerable<T> excluded, IInstanceGenerator instanceGenerator)
    {
      return EnumerableWithout(excluded.ToArray(), instanceGenerator).ToList();
    }

    public List<T> ListWith<T>(IEnumerable<T> included, IInstanceGenerator instanceGenerator)
    {
      return EnumerableWith(included.ToArray(), instanceGenerator).ToList();
    }

    public IReadOnlyList<T> ReadOnlyList<T>(IInstanceGenerator instanceGenerator)
    {
      return ReadOnlyList<T>(AllGenerator.Many, instanceGenerator);
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length, IInstanceGenerator instanceGenerator)
    {
      return List<T>(length, instanceGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items, IInstanceGenerator instanceGenerator)
    {
      return ListWith(items, instanceGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(T[] items, IInstanceGenerator instanceGenerator)
    {
      return ListWith(items, instanceGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items, IInstanceGenerator instanceGenerator)
    {
      return ListWithout(items, instanceGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(T[] items, IInstanceGenerator instanceGenerator)
    {
      return ListWithout(items, instanceGenerator);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(IInstanceGenerator instanceGenerator)
    {
      return SortedList<TKey, TValue>(AllGenerator.Many, instanceGenerator);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length, IInstanceGenerator instanceGenerator)
    {
      var list = new SortedList<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        list.Add(instanceGenerator.Instance<TKey>(), instanceGenerator.Instance<TValue>());
      }
      return list;
    }

    public ISet<T> Set<T>(int length, IInstanceGenerator instanceGenerator)
    {
      return new HashSet<T>(Enumerable<T>(length, instanceGenerator));
    }

    public ISet<T> Set<T>(IInstanceGenerator instanceGenerator)
    {
      return Set<T>(AllGenerator.Many, instanceGenerator);
    }

    public ISet<T> SortedSet<T>(int length, IInstanceGenerator instanceGenerator)
    {
      return new SortedSet<T>(Enumerable<T>(length, instanceGenerator));
    }

    public ISet<T> SortedSet<T>(IInstanceGenerator instanceGenerator)
    {
      return SortedSet<T>(AllGenerator.Many, instanceGenerator);
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator)
    {
      var dict = new Dictionary<TKey, TValue>();
      for (var i = 0; i < length; ++i)
      {
        dict.Add(instanceGenerator.Instance<TKey>(), instanceGenerator.Instance<TValue>());
      }
      return dict;

    }

    public object Dictionary(Type keyType, Type valueType, IInstanceGenerator generator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] { generator });
    }

    public Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys, IInstanceGenerator instanceGenerator)
    {
      var dict = Dictionary<T, U>(0, instanceGenerator);
      foreach (var key in keys)
      {
        dict.Add(key, instanceGenerator.InstanceOf<U>());
      }

      return dict;
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(IInstanceGenerator instanceGenerator)
    {
      return Dictionary<TKey, TValue>(AllGenerator.Many, instanceGenerator);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator)
    {
      return Dictionary<TKey, TValue>(length, instanceGenerator);
    }

    public IReadOnlyDictionary<T, U> ReadOnlyDictionaryWithKeys<T, U>(IEnumerable<T> keys, IInstanceGenerator instanceGenerator)
    {
      return DictionaryWithKeys<T, U>(keys, instanceGenerator);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(IInstanceGenerator instanceGenerator)
    {
      return ReadOnlyDictionary<TKey, TValue>(AllGenerator.Many, instanceGenerator);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator)
    {
      var dict = new SortedDictionary<TKey, TValue>();
      for (int i = 0; i < length; ++i)
      {
        dict.Add(instanceGenerator.Instance<TKey>(), instanceGenerator.Instance<TValue>());
      }
      return dict;
    }

    public object SortedDictionary(Type keyType, Type valueType, IInstanceGenerator generator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] { generator });
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(IInstanceGenerator instanceGenerator)
    {
      return SortedDictionary<TKey, TValue>(AllGenerator.Many, instanceGenerator);
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator)
    {
      var dict = new ConcurrentDictionary<TKey, TValue>();
      for (var i = 0; i < length; ++i)
      {
        dict.TryAdd(instanceGenerator.Instance<TKey>(), instanceGenerator.Instance<TValue>());
      }
      return dict;
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(IInstanceGenerator instanceGenerator)
    {
      return ConcurrentDictionary<TKey, TValue>(AllGenerator.Many, instanceGenerator);
    }

    public ConcurrentStack<T> ConcurrentStack<T>(IInstanceGenerator instanceGenerator)
    {
      return ConcurrentStack<T>(AllGenerator.Many, instanceGenerator);
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length, IInstanceGenerator instanceGenerator)
    {
      var coll = new ConcurrentStack<T>();
      for (var i = 0; i < length; ++i)
      {
        coll.Push(instanceGenerator.Instance<T>());
      }
      return coll;
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(IInstanceGenerator instanceGenerator)
    {
      return ConcurrentQueue<T>(AllGenerator.Many, instanceGenerator);
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length, IInstanceGenerator instanceGenerator)
    {
      var coll = new ConcurrentQueue<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Enqueue(instanceGenerator.Instance<T>());
      }
      return coll;

    }

    public ConcurrentBag<T> ConcurrentBag<T>(IInstanceGenerator instanceGenerator)
    {
      return ConcurrentBag<T>(AllGenerator.Many, instanceGenerator);
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length, IInstanceGenerator instanceGenerator)
    {
      var coll = new ConcurrentBag<T>();
      for (int i = 0; i < length; ++i)
      {
        coll.Add(instanceGenerator.Instance<T>());
      }
      return coll;

    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length, IInstanceGenerator instanceGenerator)
    {
      return SortedSet<T>(length, instanceGenerator).ToList();
    }

    public IEnumerable<T> EnumerableSortedDescending<T>(IInstanceGenerator instanceGenerator)
    {
      return EnumerableSortedDescending<T>(AllGenerator.Many, instanceGenerator);
    }

    public IEnumerator<T> Enumerator<T>(IInstanceGenerator instanceGenerator)
    {
      return List<T>(instanceGenerator).GetEnumerator();
    }

    public object List(Type type, IInstanceGenerator instanceGenerator)
    {
      return ResultOfGenericVersionOfMethod(
        type, 
        MethodBase.GetCurrentMethod(), 
        instanceGenerator);
    }

    public object Set(Type type, IInstanceGenerator instanceGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), instanceGenerator);
    }

    public object SortedList(Type keyType, Type valueType, IInstanceGenerator instanceGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] {instanceGenerator});
    }

    public object SortedSet(Type type, IInstanceGenerator instanceGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, MethodBase.GetCurrentMethod().Name); 
    }

    public object ConcurrentDictionary(Type keyType, Type valueType, IInstanceGenerator instanceGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, keyType, valueType, MethodBase.GetCurrentMethod().Name, new object[] {instanceGenerator});
    }

    public object Array(Type type, IInstanceGenerator instanceGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, MethodBase.GetCurrentMethod().Name, new object[] {instanceGenerator}); 
    }

    public ICollection<T> AddManyTo<T>(ICollection<T> collection, int many, IInstanceGenerator instanceGenerator)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(instanceGenerator.Instance<T>());
      }
      return collection;
    }

    public object Enumerator(Type type, IInstanceGenerator instanceGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name, new object[] {instanceGenerator}); 
    }

    public object ConcurrentStack(Type type, IInstanceGenerator instanceGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), instanceGenerator);
    }

    public object ConcurrentQueue(Type type, IInstanceGenerator instanceGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), instanceGenerator);
    }

    public object ConcurrentBag(Type type, IInstanceGenerator instanceGenerator)
    {
      return ResultOfGenericVersionOfMethod(type, MethodBase.GetCurrentMethod(), instanceGenerator);
    }

    private object ResultOfGenericVersionOfMethod(Type type, MethodBase currentMethod, IInstanceGenerator instanceGenerator)
    {
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod(
        this, type, currentMethod.Name, new object[] { instanceGenerator });
    }

    public object KeyValuePair(Type keyType, Type valueType, IInstanceGenerator generator)
    {
      return Activator.CreateInstance(
        typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType), generator.Instance(keyType), generator.Instance(valueType)
      );
    }

  }
}