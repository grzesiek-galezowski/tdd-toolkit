using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public interface FactoryForInstancesOfGenericTypes
  {
    object NewInstanceOf(Type type);
  }

  public class FactoryForInstancesOfGenericTypesWith1Type : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith1Type(Func<Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type)
    {
      var type1 = type.GetGenericArguments()[0];
      return _factoryMethod.Invoke(type1);
    }
  }

  public class FactoryForInstancesOfGenericTypesWith2Types : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, Type, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith2Types(Func<Type, Type, object> factoryMethod)
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
