using System;
using System.Linq;
using System.Reflection;

namespace TddEbook.TypeReflection
{
  public class MethodWrapper : IMethodWrapper
  {
    private readonly MethodInfo _methodInfo;

    public MethodWrapper(MethodInfo methodInfo)
    {
      _methodInfo = methodInfo;
    }

    public object InvokeWithAnyArgsOn(object instance, Func<Type, object> valueFactory)
    {
      var parameters = _methodInfo.GetParameters().Select(constructorParam => valueFactory(constructorParam.ParameterType)).ToArray();
      var returnValue = _methodInfo.Invoke(instance, parameters);
      return returnValue;
    }

    public object GenerateAnyReturnValue(Func<Type, object> valueFactory)
    {
      return valueFactory.Invoke(_methodInfo.ReturnType);
    }
  }
}