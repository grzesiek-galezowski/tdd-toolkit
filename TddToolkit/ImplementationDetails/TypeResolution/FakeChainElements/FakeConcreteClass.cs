using System;
using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeConcreteClass<T> : IResolution<T>
  {
    public bool Applies()
    {
      return true; //TODO consider catching exception here instead of in Apply() and returning false, then have a fallback chain element
    }

    public T Apply()
    {
      try
      {
        return Any.ValueOf<T>();
      }
      catch (Ploeh.AutoFixture.ObjectCreationException)
      {
        return new FallbackTypeGenerator<T>().GenerateInstance();
      }
      catch (TargetInvocationException)
      {
        return new FallbackTypeGenerator<T>().GenerateInstance();
      }
    }
  }
}