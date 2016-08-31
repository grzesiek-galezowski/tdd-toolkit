using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class PerTypeNestingLimit
  {
    private readonly Dictionary<Type, int> _nestingCounters = new Dictionary<Type,int>();
    private readonly int _limit;

    private PerTypeNestingLimit(int limit)
    {
      _limit = limit;
    }

    public static PerTypeNestingLimit Of(int limit)
    {
      return new PerTypeNestingLimit(limit);
    }

    public void AddNestingFor<T>()
    {
      var type = typeof(T);
      if (!_nestingCounters.ContainsKey(type))
      {
        _nestingCounters[type] = 0;
      }
      _nestingCounters[type]++;
      _nesting++;
    }

    public bool IsReachedFor<T>()
    {
      return _nestingCounters[typeof(T)] > _limit;
    }

    public void RemoveNestingFor<T>()
    {
      _nestingCounters[typeof(T)]--;
    }
  }
}