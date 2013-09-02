using System;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public class FallbackTypeGenerator<T>
  {
    private readonly Type _type;

    public FallbackTypeGenerator()
    {
      _type = typeof (T);
    }

    public object GenerateInstance()
    {
      var constructor = PickConstructorWithLeastNonPointersParametersFrom(_type);
      var constructorValues = constructor.GenerateAnyParameterValues();
      var instance = Activator.CreateInstance(_type, constructorValues.ToArray());
      return instance;
    }

    public bool ConstructorHasAtLeastOneNonConcreteArgumentType(Type type)
    {
      var constructor = PickConstructorWithLeastNonPointersParametersFrom(type);
      return constructor.HasAbstractOrInterfaceArguments();
    }

    private TypeConstructor PickConstructorWithLeastNonPointersParametersFrom(Type type)
    {
      var constructors = TypeConstructor.ExtractAllFrom(type);
      TypeConstructor leastParamsConstructor = null;
      var numberOfParams = int.MaxValue;

      foreach (var typeConstructor in constructors)
      {
        if (typeConstructor.HasNonPointerArgumentsOnly() && typeConstructor.HasLessParametersThan(numberOfParams))
        {
          leastParamsConstructor = typeConstructor;
          numberOfParams = typeConstructor.GetParametersCount();
        }
      }
      return leastParamsConstructor;
    }
  }
}