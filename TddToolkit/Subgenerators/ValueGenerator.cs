using System;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;

namespace TddEbook.TddToolkit.Subgenerators
{
  public class ValueGenerator
  {
    private readonly Fixture _generator;
    private readonly ProxyBasedGenerator _proxyBasedGenerator;

    public ValueGenerator(Fixture generator, ProxyBasedGenerator proxyBasedGenerator)
    {
      _generator = generator;
      _proxyBasedGenerator = proxyBasedGenerator;
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
      //bug
      return _proxyBasedGenerator.ResultOfGenericVersionOfMethod<Any>(type, MethodBase.GetCurrentMethod().Name);
    }
  }
}