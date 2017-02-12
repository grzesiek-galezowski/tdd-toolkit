using System.Collections.Generic;
using System.Linq;
using TypeReflection.Interfaces;

namespace TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PublicRecursiveConstructorsRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public PublicRecursiveConstructorsRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var constructorList = constructors.TryToObtainPublicConstructorsWithRecursiveArguments();
      if (constructorList.Any())
      {
        return constructorList.ToList();
      }
      else
      {
        return _next.RetrieveFrom(constructors);
      }
    }
  }
}