using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors;

namespace TddEbook.TddToolkit
{
  public class WrappingInterceptor : IInterceptor
  {
    private InterfaceInterceptor interfaceInterceptor;

    public WrappingInterceptor(InterfaceInterceptor interfaceInterceptor)
    {
      this.interfaceInterceptor = interfaceInterceptor;
    }
    public void Intercept(IInvocation invocation)
    {
      invocation.Proceed();
      if (invocation.ReturnValue == null || invocation.ReturnValue.Equals(DefaultValue.Of(invocation.Method.ReturnType)))
      {
        interfaceInterceptor.Intercept(invocation);
      }
    }

    internal void DoNotOverride(System.Reflection.MethodInfo methodInfo)
    {
      throw new NotImplementedException();
    }
  }
}
