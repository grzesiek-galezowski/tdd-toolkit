using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeChainFactory
  {
    private readonly CachedReturnValueGeneration _cachedReturnValueGeneration;
    private readonly NestingLimit _nestingLimit;
    private readonly ProxyGenerator _proxyGenerator;
    private readonly ValueGenerator _valueGenerator;
    private readonly GenericMethodProxyCalls _methodProxyCalls;
    private readonly ConcurrentDictionary<Type, object> _constrainedFactoryCache = new ConcurrentDictionary<Type, object>();//new MemoryCache("constrained");
    private readonly ConcurrentDictionary<Type, object> _unconstrainedFactoryCache = new ConcurrentDictionary<Type, object>();//new MemoryCache("constrained");

    public FakeChainFactory(
      CachedReturnValueGeneration cachedReturnValueGeneration, 
      NestingLimit nestingLimit, 
      ProxyGenerator proxyGenerator, 
      ValueGenerator valueGenerator, 
      GenericMethodProxyCalls methodProxyCalls)
    {
      _cachedReturnValueGeneration = cachedReturnValueGeneration;
      _nestingLimit = nestingLimit;
      _proxyGenerator = proxyGenerator;
      _valueGenerator = valueGenerator;
      _methodProxyCalls = methodProxyCalls;
    }

    public IFakeChain<T> GetInstance<T>()
    {
      return GetInstanceWithMemoization(() => 
        new GenericFakeChainFactory<T>(
          CreateSpecialCasesOfResolutions<T>()).NewInstance(
            _cachedReturnValueGeneration,
            _nestingLimit,
            _proxyGenerator,
            _valueGenerator
          ), _constrainedFactoryCache);
    }

    private static IFakeChain<T> GetInstanceWithMemoization<T>(Func<IFakeChain<T>> func, ConcurrentDictionary<Type, object> cache)
    {
      var key = typeof(T);
      object outVal;
      if(!cache.TryGetValue(key, out outVal))
      {
        var newInstance = func.Invoke();
        cache[key] = newInstance;
        return newInstance;
      }

      return (IFakeChain<T>) outVal;
    }

    public IFakeChain<T> GetUnconstrainedInstance<T>()
    {
      return GetInstanceWithMemoization(() => 
      new GenericFakeChainFactory<T>(
        CreateSpecialCasesOfResolutions<T>()).UnconstrainedInstance(
        _cachedReturnValueGeneration,
        _proxyGenerator, _valueGenerator), 
        _unconstrainedFactoryCache);
    }

    private SpecialCasesOfResolutions<T> CreateSpecialCasesOfResolutions<T>()
    {
      return new SpecialCasesOfResolutions<T>(_methodProxyCalls);
    }

    public FakeOrdinaryInterface<T> CreateFakeOrdinaryInterfaceGenerator<T>()
    {
      //bug this doesn't fit 100% here.
      return new FakeOrdinaryInterface<T>(
        _cachedReturnValueGeneration, _proxyGenerator);
    }
  }
}