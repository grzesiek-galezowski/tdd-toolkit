using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.Common.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection
{
  public class FallbackTypeGenerator<T>
  {
    private readonly Type _type;

    public FallbackTypeGenerator()
    {
      _type = typeof (T);
    }

    public int GetConstructorParametersCount()
    {
      var constructor = TypeOf<T>.PickConstructorWithLeastNonPointersParameters();
      return constructor.GetParametersCount();
    }

    public T GenerateInstance()
    {
      var constructorParameters = GenerateConstructorParameters();
      return GenerateInstance(constructorParameters);
    }

    public T GenerateInstance(IEnumerable<object> constructorParameters)
    {
      var instance = Activator.CreateInstance(_type, constructorParameters.ToArray());
      return (T)instance;
    }

    public List<object> GenerateConstructorParameters()
    {
      var constructor = TypeOf<T>.PickConstructorWithLeastNonPointersParameters();
      var constructorParameters = constructor.GenerateAnyParameterValues(
        t => Any.Instance(t)
        );
      return constructorParameters;
    }

    public bool ConstructorHasAtLeastOneNonConcreteArgumentType()
    {
      var constructor = TypeOf<T>.PickConstructorWithLeastNonPointersParameters();
      return constructor.HasAbstractOrInterfaceArguments();
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
          field.SetValue(result, Any.Instance(field.FieldType));
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

          if (!property.GetGetMethod().IsAbstract)
          {
            var value = Any.Instance(propertyType);
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