using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class ValueObjectActivator
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;
    private List<object> _constructorArguments;
    private readonly Type _type;

    public ValueObjectActivator(FallbackTypeGenerator fallbackTypeGenerator, Type type)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
      _type = type;
    }

    private object CreateInstanceWithNewConstructorArguments()
    {
      _constructorArguments = _fallbackTypeGenerator.GenerateConstructorParameters();
      return CreateInstanceWithCurrentConstructorArguments();
    }

    private object CreateInstanceWithCurrentConstructorArguments()
    {
      return CreateInstance(_constructorArguments);
    }

    public static ValueObjectActivator FreshInstance(Type type)
    {
      return new ValueObjectActivator(new FallbackTypeGenerator(type), type);
    }

    public object CreateInstanceAsValueObjectWithFreshParameters()
    {
      var instance = DefaultValue.Of(_type);
      this.Invoking(_ => { instance = _.CreateInstanceWithNewConstructorArguments(); })
        .ShouldNotThrow(_type + " cannot even be created as a value object");
      XAssert.Equal(_type, instance.GetType());
      return instance;
    }

    public object CreateInstanceAsValueObjectWithPreviousParameters()
    {
      var instance = DefaultValue.Of(_type);
      this.Invoking(_ => { instance = _.CreateInstanceWithCurrentConstructorArguments(); })
        .ShouldNotThrow(_type + " cannot even be created as a value object");
      XAssert.Equal(_type, instance.GetType());
      return instance;
    }

    public int GetConstructorParametersCount()
    {
      return _fallbackTypeGenerator.GetConstructorParametersCount();
    }

    public object CreateInstanceAsValueObjectWithModifiedParameter(int i)
    {
      var modifiedArguments = _constructorArguments.ToList();
      modifiedArguments[i] = Any.Instance(modifiedArguments[i].GetType());
      return CreateInstance(modifiedArguments);
    }

    private object CreateInstance(List<object> parameters)
    {
      return _fallbackTypeGenerator.GenerateInstance(parameters.ToArray());
    }

    public Type TargetType
    {
      get
      {
        return _type;
      }
    }
  }

  public class DefaultValue
  {
    public static object Of(Type t)
    {
      if (t.IsValueType)
      {
        return Activator.CreateInstance(t);
      }

      return null;
    }
  }

}