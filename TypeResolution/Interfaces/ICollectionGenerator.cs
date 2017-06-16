using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.Interfaces
{
  public interface ICollectionGenerator
  {
    IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included, IInstanceGenerator instanceGenerator);
    IEnumerable<T> Enumerable<T>(IInstanceGenerator instanceGenerator);
    IEnumerable<T> Enumerable<T>(int length, IInstanceGenerator instanceGenerator);
    IEnumerable<T> EnumerableWithout<T>(T[] excluded, IInstanceGenerator instanceGenerator);
    T[] Array<T>(IInstanceGenerator instanceGenerator);
    T[] Array<T>(int length, IInstanceGenerator instanceGenerator);
    T[] ArrayWithout<T>(T[] excluded, IInstanceGenerator instanceGenerator);
    T[] ArrayWith<T>(T[] included, IInstanceGenerator instanceGenerator);
    T[] ArrayWithout<T>(IEnumerable<T> excluded, IInstanceGenerator instanceGenerator);
    T[] ArrayWith<T>(IInstanceGenerator instanceGenerator, IEnumerable<T> included);
    List<T> List<T>(IInstanceGenerator instanceGenerator);
    List<T> List<T>(int length, IInstanceGenerator instanceGenerator);
    List<T> ListWithout<T>(T[] excluded, IInstanceGenerator instanceGenerator);
    List<T> ListWith<T>(T[] included, IInstanceGenerator instanceGenerator);
    List<T> ListWithout<T>(IEnumerable<T> excluded, IInstanceGenerator instanceGenerator);
    List<T> ListWith<T>(IEnumerable<T> included, IInstanceGenerator instanceGenerator);
    IReadOnlyList<T> ReadOnlyList<T>(IInstanceGenerator instanceGenerator);
    IReadOnlyList<T> ReadOnlyList<T>(int length, IInstanceGenerator instanceGenerator);
    IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items, IInstanceGenerator instanceGenerator);
    IReadOnlyList<T> ReadOnlyListWith<T>(T[] items, IInstanceGenerator instanceGenerator);
    IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items, IInstanceGenerator instanceGenerator);
    IReadOnlyList<T> ReadOnlyListWithout<T>(T[] items, IInstanceGenerator instanceGenerator);
    SortedList<TKey, TValue> SortedList<TKey, TValue>(IInstanceGenerator instanceGenerator);
    SortedList<TKey, TValue> SortedList<TKey, TValue>(int length, IInstanceGenerator instanceGenerator);
    ISet<T> Set<T>(int length, IInstanceGenerator instanceGenerator);
    ISet<T> Set<T>(IInstanceGenerator instanceGenerator);
    ISet<T> SortedSet<T>(int length, IInstanceGenerator instanceGenerator);
    ISet<T> SortedSet<T>(IInstanceGenerator instanceGenerator);
    Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator);
    object Dictionary(Type keyType, Type valueType, IInstanceGenerator generator);
    Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys, IInstanceGenerator instanceGenerator);
    Dictionary<TKey, TValue> Dictionary<TKey, TValue>(IInstanceGenerator instanceGenerator);
    IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator);
    IReadOnlyDictionary<T, U> ReadOnlyDictionaryWithKeys<T, U>(IEnumerable<T> keys, IInstanceGenerator instanceGenerator);
    IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(IInstanceGenerator instanceGenerator);
    SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator);
    object SortedDictionary(Type keyType, Type valueType, IInstanceGenerator generator);
    SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(IInstanceGenerator instanceGenerator);
    ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length, IInstanceGenerator instanceGenerator);
    ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(IInstanceGenerator instanceGenerator);
    ConcurrentStack<T> ConcurrentStack<T>(IInstanceGenerator instanceGenerator);
    ConcurrentStack<T> ConcurrentStack<T>(int length, IInstanceGenerator instanceGenerator);
    ConcurrentQueue<T> ConcurrentQueue<T>(IInstanceGenerator instanceGenerator);
    ConcurrentQueue<T> ConcurrentQueue<T>(int length, IInstanceGenerator instanceGenerator);
    ConcurrentBag<T> ConcurrentBag<T>(IInstanceGenerator instanceGenerator);
    ConcurrentBag<T> ConcurrentBag<T>(int length, IInstanceGenerator instanceGenerator);
    IEnumerable<T> EnumerableSortedDescending<T>(int length, IInstanceGenerator instanceGenerator);
    IEnumerable<T> EnumerableSortedDescending<T>(IInstanceGenerator instanceGenerator);
    IEnumerator<T> Enumerator<T>(IInstanceGenerator instanceGenerator);
    object List(Type type, IInstanceGenerator instanceGenerator);
    object Set(Type type, IInstanceGenerator instanceGenerator);
    object SortedList(Type keyType, Type valueType, IInstanceGenerator instanceGenerator);
    object SortedSet(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentDictionary(Type keyType, Type valueType, IInstanceGenerator instanceGenerator);
    object Array(Type type, IInstanceGenerator instanceGenerator);
    ICollection<T> AddManyTo<T>(ICollection<T> collection, int many, IInstanceGenerator instanceGenerator);
    object Enumerator(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentStack(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentQueue(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentBag(Type type, IInstanceGenerator instanceGenerator);
    object KeyValuePair(Type keyType, Type valueType, IInstanceGenerator generator);
  }
}