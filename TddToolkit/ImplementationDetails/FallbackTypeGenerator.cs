using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection
{
  public class FallbackTypeGenerator<T>
  {
    private readonly Type _type;
    private FallbackTypeGenerator _fallbackTypeGenerator;

    public FallbackTypeGenerator()
    {
      _type = typeof (T);
      _fallbackTypeGenerator = new FallbackTypeGenerator(_type);
    }

    public int GetConstructorParametersCount()
    {
      return _fallbackTypeGenerator.GetConstructorParametersCount();
    }

    public T GenerateInstance()
    {
      return (T)_fallbackTypeGenerator.GenerateInstance();
    }

    public T GenerateInstance(IEnumerable<object> constructorParameters)
    {
      return (T)_fallbackTypeGenerator.GenerateInstance(constructorParameters);
    }

    public List<object> GenerateConstructorParameters()
    {
      return _fallbackTypeGenerator.GenerateConstructorParameters();
    }

    public bool ConstructorHasAtLeastOneNonConcreteArgumentType()
    {
      return _fallbackTypeGenerator.ConstructorHasAtLeastOneNonConcreteArgumentType();
    }


    public void FillFieldsAndPropertiesOf(T result)
    {
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(result);
    }
  }

  public class FallbackTypeGenerator
  {
    private TypeWrapper _typeWrapper;
    private Type _type;

    public FallbackTypeGenerator(Type type)
    {
      _typeWrapper = new TypeWrapper(type);
      _type = type;
    }

    public int GetConstructorParametersCount()
    {
      var constructor = _typeWrapper.PickConstructorWithLeastNonPointersParameters();
      return constructor.GetParametersCount();
    }

    public object GenerateInstance()
    {
      var constructorParameters = GenerateConstructorParameters();
      return GenerateInstance(constructorParameters);
    }

    public object GenerateInstance(IEnumerable<object> constructorParameters)
    {
      var instance = Activator.CreateInstance(_type, constructorParameters.ToArray());
      return instance;
    }

    public List<object> GenerateConstructorParameters()
    {
      var constructor = _typeWrapper.PickConstructorWithLeastNonPointersParameters();
      var constructorParameters = constructor.GenerateAnyParameterValues(
        t => Any.Instance(t)
        );
      return constructorParameters;
    }

    public bool ConstructorHasAtLeastOneNonConcreteArgumentType()
    {
      var constructor = _typeWrapper.PickConstructorWithLeastNonPointersParameters();
      return constructor.HasAbstractOrInterfaceArguments();
    }


    public void FillFieldsAndPropertiesOf(object result)
    {
      FillPropertyValues(result);
      FillFieldValues(result);
    }

    private void FillFieldValues(object result)
    {
      var fields = _type.GetFields(BindingFlags.Public | BindingFlags.Instance);
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

    private void FillPropertyValues(object result)
    {
      var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite);

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