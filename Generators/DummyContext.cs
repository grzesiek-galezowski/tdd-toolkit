using AutoFixture.Kernel;

namespace TddEbook.TddToolkit.ImplementationDetails
{
  public class DummyContext : ISpecimenContext
  {
    public object Resolve(object request)
    {
      return null;
    }
  }
}

