using System.Collections.Generic;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PublicParameterlessConstructorRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public PublicParameterlessConstructorRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorRetrieval constructors)
    {
      if (constructors.HasPublicParameterlessConstructor())
      {
        return new List<IConstructorWrapper> {new DefaultParameterlessConstructor(constructors.GetPublicParameterlessConstructorInfo())};
      }
      else
      {
        return _next.RetrieveFrom(constructors);
      }
    }
  }
}