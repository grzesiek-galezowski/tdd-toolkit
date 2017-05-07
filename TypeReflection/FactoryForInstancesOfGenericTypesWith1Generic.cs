using System;
using System.Reflection;

namespace TddEbook.TypeReflection
{
  public class FactoryForInstancesOfGenericTypesWith1Generic : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, IProxyBasedGenerator, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith1Generic(
      Func<Type, IProxyBasedGenerator, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      var type1 = type.GetTypeInfo().GetGenericArguments()[0];
      return _factoryMethod.Invoke(type1, proxyBasedGenerator);
    }
  }
}