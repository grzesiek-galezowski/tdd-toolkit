using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails
{
	public class ExplodingInterceptor : IInterceptor
	{
      public void Intercept(IInvocation invocation)
      {
        throw new BoooooomException(invocation.Method.Name);
      }
	}
}

