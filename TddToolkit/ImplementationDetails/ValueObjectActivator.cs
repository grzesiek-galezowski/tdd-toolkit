using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Reflection;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection
{
  public class ValueObjectActivator<T>
  {
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;
    private List<object> _constructorArguments;

    public ValueObjectActivator(FallbackTypeGenerator<T> fallbackTypeGenerator)
    {
      this._fallbackTypeGenerator = fallbackTypeGenerator;
    }

    private T CreateInstanceWithNewConstructorArguments()
    {
      _constructorArguments = _fallbackTypeGenerator.GenerateConstructorParameters();
      return CreateInstanceWithCurrentConstructorArguments();
    }

    private T CreateInstanceWithCurrentConstructorArguments()
    {
      return CreateInstance(_constructorArguments);
    }

    public static ValueObjectActivator<T> FreshInstance()
    {
      return new ValueObjectActivator<T>(new FallbackTypeGenerator<T>());
    }

    public T CreateInstanceAsValueObjectWithFreshParameters()
    {
      var instance = default(T);
      this.Invoking(_ => { instance = _.CreateInstanceWithNewConstructorArguments(); })
        .ShouldNotThrow(typeof(T) + " cannot even be created as a value object");
      return instance;
    }

    public T CreateInstanceAsValueObjectWithPreviousParameters()
    {
      var instance = default(T);
      this.Invoking(_ => { instance = _.CreateInstanceWithCurrentConstructorArguments(); })
        .ShouldNotThrow(typeof(T) + " cannot even be created as a value object");
      return instance;
    }

    public int GetConstructorParametersCount()
    {
      return _fallbackTypeGenerator.GetConstructorParametersCount();
    }

    public T CreateInstanceAsValueObjectWithModifiedParameter(int i)
    {
      var modifiedArguments = _constructorArguments.ToList();
      modifiedArguments[i] = Any.Instance(modifiedArguments[i].GetType());
      return CreateInstance(modifiedArguments);

    }

    private T CreateInstance(List<object> parameters)
    {
      if (parameters.Count > 0)
      {
        return (T) Activator.CreateInstance(typeof (T), parameters.ToArray());
      }
      else
      {
        return Activator.CreateInstance<T>();
      }
    }
  }
}