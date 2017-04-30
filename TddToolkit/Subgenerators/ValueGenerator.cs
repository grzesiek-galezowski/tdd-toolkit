using System;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class ValueGenerator
  {
    private readonly Fixture _generator;
    private readonly GenericMethodProxyCalls _genericMethodProxyCalls;

    public ValueGenerator(Fixture generator, GenericMethodProxyCalls genericMethodProxyCalls)
    {
      _generator = generator;
      _genericMethodProxyCalls = genericMethodProxyCalls;
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      T currentValue;
      do
      {
        currentValue = ValueOf<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public T ValueOf<T>()
    {
      //bug: add support for creating generic structs with interfaces
      return _generator.Create<T>();
    }

    public object ValueOf(Type type)
    {
      //bug put under test and change Any to current
      return _genericMethodProxyCalls.ResultOfGenericVersionOfMethod<Any>(type, MethodBase.GetCurrentMethod().Name);
    }

    public T ValueOf<T>(T seed)
    {
      return _generator.Create(seed);
    }
  }
}