using TddEbook.TypeReflection;

namespace TypeResolution.TypeResolution.FakeChainElements
{
  public interface IResolution<out T>
  {
    bool Applies();
    T Apply(IInstanceGenerator instanceGenerator);
  }
}