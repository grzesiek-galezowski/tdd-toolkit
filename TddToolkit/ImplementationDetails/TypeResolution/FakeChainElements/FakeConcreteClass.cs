using System;
using System.Reflection;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeConcreteClass<T> : IResolution<T>
  {
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;
    private readonly ValueGenerator _valueGenerator;

    public FakeConcreteClass(
      FallbackTypeGenerator<T> fallbackTypeGenerator, 
      ValueGenerator valueGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
      _valueGenerator = valueGenerator;
    }

    public bool Applies()
    {
      return true; //TODO consider catching exception here instead of in Apply() and returning false, then have a fallback chain element
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      try
      {
        return _valueGenerator.ValueOf<T>();
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