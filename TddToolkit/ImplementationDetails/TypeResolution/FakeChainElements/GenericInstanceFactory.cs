using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public interface GenericInstanceFactory
  {
    object Create(Type type);
  }

  public class GenericInstanceFactory1 : GenericInstanceFactory
  {
    private readonly Func<Type, object> _factoryMethod;

    public GenericInstanceFactory1(Func<Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object Create(Type type)
    {
      var type1 = type.GetGenericArguments()[0];
      return _factoryMethod.Invoke(type1);
    }
  }

  public class GenericInstanceFactory2 : GenericInstanceFactory
  {
    private readonly Func<Type, Type, object> _factoryMethod;

    public GenericInstanceFactory2(Func<Type, Type, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object Create(Type type)
    {
      var type1 = type.GetGenericArguments()[0];
      var type2 = type.GetGenericArguments()[1];
      return _factoryMethod.Invoke(type1, type2);
    }
  }


}
