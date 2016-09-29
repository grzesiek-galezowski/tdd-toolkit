using System;
using System.Collections.Generic;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public interface NestingLimit
  {
    void AddNestingFor<T>();
    bool IsReachedFor<T>();
    void RemoveNestingFor<T>();
  }

  internal class PerTypeNestingLimit : NestingLimit
  {
    private readonly Dictionary<Type, int> _nestingCounters = new Dictionary<Type, int>();
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

  internal class GlobalNestingLimit : NestingLimit
  {
    private readonly int _limit;
    private int _nesting;

    private GlobalNestingLimit(int limit)
    {
      _limit = limit;
    }

    public static GlobalNestingLimit Of(int limit)
    {
      return new GlobalNestingLimit(limit);
    }

    public void AddNestingFor<T>()
    {
      _nesting++;
    }

    public bool IsReachedFor<T>()
    {
      return _nesting > _limit;
    }

    public void RemoveNestingFor<T>()
    {
      _nesting--;
    }
  }
}