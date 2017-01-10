using System;
using System.Reflection;

namespace TddEbook.TypeReflection
{
  public class FactoryForInstancesOfGenericTypesWith1Generic : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith1Generic(Func<Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type)
    {
      var type1 = type.GetTypeInfo().GetGenericArguments()[0];
      return _factoryMethod.Invoke(type1);
    }
  }
}