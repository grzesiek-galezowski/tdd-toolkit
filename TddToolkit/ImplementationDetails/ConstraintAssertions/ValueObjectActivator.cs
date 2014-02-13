using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution;

namespace TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions
{
  public class ValueObjectActivator<T>
  {
    readonly FallbackTypeGenerator<T> _fallbackTypeGenerator = new FallbackTypeGenerator<T>();
    private List<object> _constructorArguments;

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
      return new ValueObjectActivator<T>();
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