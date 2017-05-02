using System;
using System.Reflection;

namespace TddEbook.TypeReflection
{
  public class FactoryForInstancesOfGenericTypesWith1Generic : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<IProxyBasedGenerator, Type, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith1Generic(
      Func<IProxyBasedGenerator, Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      var type1 = type.GetTypeInfo().GetGenericArguments()[0];
      return _factoryMethod.Invoke(proxyBasedGenerator, type1);
    }
  }
}