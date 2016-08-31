using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  class LimitedFakeChain<T> : IFakeChain<T>
  {
    private readonly PerTypeNestingLimit _perTypeNestingLimit;
    private readonly IFakeChain<T> _fakeChain;

    public LimitedFakeChain(PerTypeNestingLimit perTypeNestingLimit, IFakeChain<T> fakeChain)
    {
      _perTypeNestingLimit = perTypeNestingLimit;
      _fakeChain = fakeChain;
    }

    public T Resolve()
    {
      try
      {
        _perTypeNestingLimit.AddNestingFor<T>();
        if (!_perTypeNestingLimit.IsReachedFor<T>())
        {
          return _fakeChain.Resolve();
        }
        else 
        {
          try
          {
            try
            {
              return Any.Dummy<T>();
            }
            catch (Exception e)
            {
              Console.WriteLine(e);
              return default(T);
            }
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