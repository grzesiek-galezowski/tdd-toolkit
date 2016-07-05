using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  class LimitedFakeChain<T> : IFakeChain<T>
  {
    private readonly NestingLimit _nestingLimit;
    private readonly IFakeChain<T> _fakeChain;

    public LimitedFakeChain(NestingLimit nestingLimit, IFakeChain<T> fakeChain)
    {
      _nestingLimit = nestingLimit;
      _fakeChain = fakeChain;
    }

    public T Resolve()
    {
      try
      {
        _nestingLimit.AddNesting();
        if (!_nestingLimit.IsReached())
        {
          return _fakeChain.Resolve();
        }
        else 
        {
          try
          {
            try
            {
              return (T) FormatterServices.GetUninitializedObject(typeof(T));
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
        _nestingLimit.RemoveNesting();
      }

    }
  }
}