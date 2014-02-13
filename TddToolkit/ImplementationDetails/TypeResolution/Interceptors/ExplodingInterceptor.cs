using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  public class ExplodingInterceptor : IInterceptor
  {
      public void Intercept(IInvocation invocation)
      {
        throw new BoooooomException(invocation.Method.Name);
      }
  }
}

