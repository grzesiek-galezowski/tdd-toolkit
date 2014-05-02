using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class ValueObjectActivator<T>
  {
    private readonly ValueObjectActivator _activator;

    public ValueObjectActivator(ValueObjectActivator activator)
    {
      _activator = activator;
    }

    public static ValueObjectActivator<T> FreshInstance()
    {
      return new ValueObjectActivator<T>(ValueObjectActivator.FreshInstance(typeof(T)));
    }

    public T CreateInstanceAsValueObjectWithFreshParameters()
    {
      return (T)_activator.CreateInstanceAsValueObjectWithFreshParameters();
    }

    public T CreateInstanceAsValueObjectWithPreviousParameters()
    {
      return (T)_activator.CreateInstanceAsValueObjectWithPreviousParameters();
    }

    public int GetConstructorParametersCount()
    {
      return _activator.GetConstructorParametersCount();
    }

    public T CreateInstanceAsValueObjectWithModifiedParameter(int i)
    {
      return (T)_activator.CreateInstanceAsValueObjectWithModifiedParameter(i);
    }
  }

  public class ValueObjectActivator
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;
    private List<object> _constructorArguments;
    private Type _type;

    public ValueObjectActivator(FallbackTypeGenerator fallbackTypeGenerator, Type type)
    {
      this._fallbackTypeGenerator = fallbackTypeGenerator;
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
      return instance;
    }

    public object CreateInstanceAsValueObjectWithPreviousParameters()
    {
      var instance = DefaultValue.Of(_type);
      this.Invoking(_ => { instance = _.CreateInstanceWithCurrentConstructorArguments(); })
        .ShouldNotThrow(_type + " cannot even be created as a value object");

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
      if (parameters.Count > 0)
      {
        return Activator.CreateInstance(_type, parameters.ToArray());
      }
      else
      {
        return Activator.CreateInstance(_type);
      }
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