using System.Collections.Generic;
using System.Reflection;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class NonPublicParameterlessConstructorRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public NonPublicParameterlessConstructorRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var constructor = constructors.GetNonPublicParameterlessConstructorInfo();
      if (constructor.HasValue)
      {
        return new List<IConstructorWrapper> { constructor.Value };
      }
      else
      {
        return _next.RetrieveFrom(constructors);
      }
    }
  }
}