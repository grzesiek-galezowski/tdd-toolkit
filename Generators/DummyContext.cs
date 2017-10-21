using AutoFixture.Kernel;

namespace TddEbook.TddToolkit.Generators
{
  public class DummyContext : ISpecimenContext
  {
    public object Resolve(object request)
    {
      return null;
    }
  }
}

