using System;

namespace TddEbook.TypeReflection
{
  public interface FactoryForInstancesOfGenericTypes
  {
    object NewInstanceOf(Type type);
  }

  public class FactoryForInstancesOfGenericTypesWith1Generic : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith1Generic(Func<Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type)
    {
      var type1 = type.GetGenericArguments()[0];
      return _factoryMethod.Invoke(type1);
    }
  }

  public class FactoryForInstancesOfGenericTypesWith2Generics : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, Type, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith2Generics(Func<Type, Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type)
    {
      var type1 = type.GetGenericArguments()[0];
      var type2 = type.GetGenericArguments()[1];
      return _factoryMethod.Invoke(type1, type2);
    }
  }


}
