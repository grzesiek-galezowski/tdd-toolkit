namespace TddEbook.TddToolkit.TypeResolution.Interfaces
{
  public interface IValueGenerator
  {
    T ValueOtherThan<T>(params T[] omittedValues);
    T ValueOf<T>();
    T ValueOf<T>(T seed);
  }
}