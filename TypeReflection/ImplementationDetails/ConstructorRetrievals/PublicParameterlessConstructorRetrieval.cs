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

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var constructor = constructors.GetPublicParameterlessConstructor();
      if (constructor.HasValue)
      {
        return new List<IConstructorWrapper> {constructor.Value};
      }
      else
      {
        return _next.RetrieveFrom(constructors);
      }
    }
  }
}