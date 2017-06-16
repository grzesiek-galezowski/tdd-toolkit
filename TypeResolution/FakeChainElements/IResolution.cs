using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public interface IResolution<out T>
  {
    bool Applies();
    T Apply(IInstanceGenerator instanceGenerator);
  }
}