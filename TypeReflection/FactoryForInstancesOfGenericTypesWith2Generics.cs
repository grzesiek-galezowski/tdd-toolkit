using System;
using System.Reflection;

namespace TddEbook.TypeReflection
{
  public class FactoryForInstancesOfGenericTypesWith2Generics : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<IProxyBasedGenerator, Type, Type, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith2Generics(Func<IProxyBasedGenerator, Type, Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type, IProxyBasedGenerator proxyBasedGenerator)
    {
      var typeInfo = type.GetTypeInfo();
      var type1 = typeInfo.GetGenericArguments()[0];
      var type2 = typeInfo.GetGenericArguments()[1];
      return _factoryMethod.Invoke(proxyBasedGenerator, type1, type2);
    }
  }
}