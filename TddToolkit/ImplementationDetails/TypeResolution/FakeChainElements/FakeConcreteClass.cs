using System;
using System.Reflection;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeConcreteClass<T> : IResolution<T>
  {
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;

    public FakeConcreteClass(FallbackTypeGenerator<T> fallbackTypeGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public bool Applies()
    {
      return true; //TODO consider catching exception here instead of in Apply() and returning false, then have a fallback chain element
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      try
      {
        return Any.ValueOf<T>(); //bug
      }
      catch (Ploeh.AutoFixture.ObjectCreationException)
      {
        return _fallbackTypeGenerator.GenerateInstance(proxyBasedGenerator);
      }
      catch (TargetInvocationException)
      {
        return _fallbackTypeGenerator.GenerateInstance(proxyBasedGenerator);
      }
    }
  }
}