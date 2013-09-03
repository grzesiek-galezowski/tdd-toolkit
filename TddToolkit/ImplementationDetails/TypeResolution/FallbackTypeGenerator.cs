using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  public class FallbackTypeGenerator<T>
  {
    private readonly Type _type;

    public FallbackTypeGenerator()
    {
      _type = typeof (T);
    }

    public T GenerateInstance()
    {
      var constructorParameters = GenerateConstructorParameters();
      var instance = Activator.CreateInstance(_type, constructorParameters.ToArray());
      return (T)instance;
    }

    public List<object> GenerateConstructorParameters()
    {
      var constructor = PickConstructorWithLeastNonPointersParametersFrom(_type);
      var constructorParameters = constructor.GenerateAnyParameterValues();
      return constructorParameters;
    }

    public bool ConstructorHasAtLeastOneNonConcreteArgumentType(Type type)
    {
      var constructor = PickConstructorWithLeastNonPointersParametersFrom(type);
      return constructor.HasAbstractOrInterfaceArguments();
    }

    private static ITypeConstructor PickConstructorWithLeastNonPointersParametersFrom(Type type)
    {
      var constructors = TypeConstructor.ExtractAllFrom(type);
      ITypeConstructor leastParamsConstructor = null;
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

    public void FillFieldsAndPropertiesOf(T result)
    {
      FillPropertyValues(result);
      FillFieldValues(result);
    }

    private static void FillFieldValues(T result)
    {
      var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
      foreach (var field in fields)
      {
        try
        {
          field.SetValue(result, AnyReturnValue.Of(field.FieldType).Generate());
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
    }

    private static void FillPropertyValues(T result)
    {
      var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite);

      foreach (var property in properties)
      {
        try
        {
          var propertyType = property.PropertyType;

          if (!property.GetGetMethod().IsVirtual && !property.GetGetMethod().IsAbstract)
          {
            var value = AnyReturnValue.Of(propertyType).Generate();
            property.SetValue(result, value, null);
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
    }
  }
}