using System;
using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class FakeAbstractClass<T> : IResolution<T> where T : class
  {
    private readonly CachedGeneration _generation;

    public FakeAbstractClass(CachedGeneration generation)
    {
      _generation = generation;
    }

    public bool Applies()
    {
      return typeof (T).IsAbstract;
    }

    public T Apply()
    {
      var fallbackTypeGenerator = new FallbackTypeGenerator<T>();
      var result = (T)(new ProxyGenerator().CreateClassProxy(
        typeof(T),
        fallbackTypeGenerator.GenerateConstructorParameters().ToArray(),
        new IInterceptor[] {new AbstractClassInterceptor(_generation)}));
      fallbackTypeGenerator.FillFieldsAndPropertiesOf(result);
      return result;
    }
  }

  internal class AbstractClassInterceptor : IInterceptor
  {
    private readonly CachedGeneration _cachedGeneration;

    public AbstractClassInterceptor(CachedGeneration cachedGeneration)
    {
      _cachedGeneration = cachedGeneration;
    }

    public void Intercept(IInvocation invocation)
    {
      if (invocation.Method.IsAbstract)
      {
        var returnType = invocation.Method.ReturnType;
        if (_cachedGeneration.AppliesTo(returnType))
        {
          invocation.ReturnValue = _cachedGeneration.GetReturnTypeFor(invocation);
        }
      }
    }
  }
}