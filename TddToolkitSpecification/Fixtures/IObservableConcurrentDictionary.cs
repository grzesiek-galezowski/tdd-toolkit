using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TddEbook.TddToolkitSpecification.Fixtures
{
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IObservableConcurrentDictionary<TKey, TValue>
    : IObservable<Tuple<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
  {
    void TryAdd(TKey key, TValue value);
    TValue this[TKey key] { get; set; }
    void TryRemove(TKey key);
    bool TryGetValue(TKey key, out TValue value);
  }
}