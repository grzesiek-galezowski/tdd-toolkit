using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class ValueObjectActivator
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;
    private List<object> _constructorArguments;
    private readonly Type _type;
    private readonly IInstanceGenerator _generator;

    public ValueObjectActivator(FallbackTypeGenerator fallbackTypeGenerator, Type type, IInstanceGenerator generator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
      _type = type;
      _generator = generator;
    }

    private object CreateInstanceWithNewConstructorArguments()
    {
      _constructorArguments = _fallbackTypeGenerator.GenerateConstructorParameters(_generator.Instance);
      return CreateInstanceWithCurrentConstructorArguments();
    }

    private object CreateInstanceWithCurrentConstructorArguments()
    {
      return _fallbackTypeGenerator.GenerateInstance(_constructorArguments.ToArray());
    }

    public static ValueObjectActivator FreshInstance(Type type, IInstanceGenerator generator)
    {
      return new ValueObjectActivator(new FallbackTypeGenerator(type), type, generator);
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
      modifiedArguments[i] = _generator.Instance(modifiedArguments[i].GetType());
      return _fallbackTypeGenerator.GenerateInstance(modifiedArguments.ToArray());
    }

    public Type TargetType
    {
      get
      {
        return _type;
      }
    }
  }
}