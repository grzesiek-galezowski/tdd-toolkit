using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  internal class FakeConcreteClassWithNonConcreteConstructor<T> : IResolution<T>
  {
    readonly FallbackTypeGenerator<T> _fallbackTypeGenerator = new FallbackTypeGenerator<T>();

    public bool Applies()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }

    public T Apply(IInstanceGenerator instanceGenerator)
    {
      return _fallbackTypeGenerator.GenerateInstance(instanceGenerator);
    }
  }
}