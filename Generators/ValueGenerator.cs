using System.Linq;
using Ploeh.AutoFixture;
using TddEbook.TddToolkit.TypeResolution.Interfaces;

namespace TddEbook.TddToolkit.Generators
{
  public class ValueGenerator : IValueGenerator
  {
    private readonly Fixture _generator;

    public ValueGenerator(Fixture generator)
    {
      _generator = generator;
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

    public T ValueOf<T>(T seed)
    {
      return _generator.Create(seed);
    }
  }
}