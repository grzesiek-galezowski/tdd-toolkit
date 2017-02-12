using System;
using System.Collections.Generic;
using TddEbook.TypeReflection;
using TypeReflection.Interfaces;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class FallbackTypeGenerator<T>
  {
    private readonly Type _type;
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;

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
      var generateInstance = (T)_fallbackTypeGenerator.GenerateInstance();
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(generateInstance);
      return generateInstance;
    }

    public List<object> GenerateConstructorParameters()
    {
      return _fallbackTypeGenerator.GenerateConstructorParameters();
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }


    public void FillFieldsAndPropertiesOf(T result)
    {
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(result);
    }
  }

  public class FallbackTypeGenerator
  {
    private readonly IType _smartType;
    private readonly Type _type;

    public FallbackTypeGenerator(Type type)
    {
      _smartType = SmartType.For(type);
      _type = type;
    }

    public int GetConstructorParametersCount()
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      return constructor.Value.GetParametersCount(); //bug backward compatibility (for now)
    }

    public object GenerateInstance()
    {
      var instance = _smartType.PickConstructorWithLeastNonPointersParameters()
        .Value.InvokeWithParametersCreatedBy(Any.Instance);  //bug backward compatibility (for now)
      XAssert.Equal(_type, instance.GetType());
      return instance;
    }

    public object GenerateInstance(IEnumerable<object> constructorParameters)
    {
      var instance = _smartType.PickConstructorWithLeastNonPointersParameters().Value  //bug backward compatibility (for now)
        .InvokeWith(constructorParameters);
      XAssert.Equal(_type, instance.GetType());
      return instance;
    }

    public List<object> GenerateConstructorParameters()
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      var constructorParameters = constructor.Value  //bug backward compatibility (for now)
        .GenerateAnyParameterValues(Any.Instance);
      return constructorParameters;
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      return constructor.Value //bug backward compatibility (for now)
        .HasAbstractOrInterfaceArguments()
      || constructor.Value.IsInternal();
    }


    public void FillFieldsAndPropertiesOf(object result)
    {
      FillPropertyValues(result);
      FillFieldValues(result);
    }

    private void FillFieldValues(object result)
    {
      var fields = _smartType.GetAllPublicInstanceFields();
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
      var properties = _smartType.GetPublicInstanceWritableProperties();

      foreach (var property in properties)
      {
        try
        {
          var propertyType = property.PropertyType;

          if (!property.HasAbstractGetter())
          {
            var value = Any.Instance(propertyType);
            property.SetValue(result, value);
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