using System;
using System.Reflection;
using System.Runtime.Serialization;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  class LimitedFakeChain<T> : IFakeChain<T>
  {
    private readonly NestingLimit _perTypeNestingLimit;
    private readonly IFakeChain<T> _fakeChain;

    public LimitedFakeChain(NestingLimit perTypeNestingLimit, IFakeChain<T> fakeChain)
    {
      _perTypeNestingLimit = perTypeNestingLimit;
      _fakeChain = fakeChain;
    }

    public T Resolve(IProxyBasedGenerator proxyBasedGenerator)
    {
      try
      {
        _perTypeNestingLimit.AddNestingFor<T>();
        if (!_perTypeNestingLimit.IsReachedFor<T>())
        {
          return _fakeChain.Resolve(proxyBasedGenerator);
        }
        else 
        {
          try
          {
            //return default(T);
            return Any.Dummy<T>();
          }
          catch (TargetInvocationException e)
          {
            return default(T);
          }
          catch (MemberAccessException e)
          {
            return default(T);
          }
          catch (ArgumentException e)
          {
            return default(T);
          }
        }
        
      }
      finally
      {
        _perTypeNestingLimit.RemoveNestingFor<T>();
      }

    }
  }
}