using Castle.DynamicProxy;

namespace TypeResolution.TypeResolution.Interceptors
{
  public class ExplodingInterceptor : IInterceptor
  {
      public void Intercept(IInvocation invocation)
      {
        throw new BoooooomException(invocation.Method.Name);
      }
  }
}

